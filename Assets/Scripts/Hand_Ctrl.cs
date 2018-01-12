using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand_Ctrl : MonoBehaviour
{

    //public OVRInput.Controller Controller;

    public GameObject P_Go;//플레이어 오브젝트

    public Transform G_FirePositionL;//발사위치
    public GameObject HandnGunL;//총없는손
    public GameObject G_gunhandL;//총있는손
    private bool G_isGrapL = false;//총 잡았는가?
    private bool G_isReadyL = false;//총을 쏠수 있는가?
    private bool G_TriggerBackL;//노리쇠후퇴
    public GameObject G_MFL;//머즐플래쉬

    public Animator HandRAniL;//애니메이터

    public AudioSource GunSfxL;
    public AudioClip Reload;//사운드
    public AudioClip[] FireSfx;
    public AudioClip FireFSfx;

    private int G_BulletL = 6;//6발


    private void Start()
    {
        G_isGrapL = false;
        G_isReadyL = true;
        G_TriggerBackL = true;
        G_gunhandL.SetActive(false);
        HandnGunL.SetActive(true);
    }

    void Update()
    {
        float Firetrigger_resultf =
            OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);//방아쇠컨트롤러버튼

        float G_Reloadf =
            OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y;//스틱컨트롤러y축 버튼
        
        //Debug.Log(Firetrigger_resultf);
        //Debug.Log(G_Reloadf);
        Debug.DrawRay(G_FirePositionL.position, G_FirePositionL.forward * 100, Color.green);
        if (Input.GetKeyDown(KeyCode.A))
        {
            H_changeL();
        }

        if (G_isGrapL == true)
        {
            if (G_isReadyL == true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || Firetrigger_resultf >= 0.9f)//마우스버튼 클릭시 발포성공
                {
                    if (G_Reloadf > -0.3f)
                    {

                        G_FireL();
                        Debug.Log("fire");
                    }
                }
            }
            else
            {
                HandRAniL.SetBool("GisReady", false);
                if (Input.GetKeyDown(KeyCode.Mouse0) || Firetrigger_resultf >= 0.9f)//마우스버튼 클릭시 발포 실패
                {
                    if (G_TriggerBackL != false)
                    {
                       
                        G_FireFL();
                        HandRAniL.SetTrigger("FireFalse");
                    }

                }
                if (Input.GetKeyDown(KeyCode.R) || G_Reloadf < -0.8f)//재장전
                {
                    G_ReloadL();
                }
            }
        }

    }
    void G_FireL()//발사
    {
        //이펙트 사운드
        HandRAniL.SetTrigger("Fire");
        G_TriggerBackL = false;
        FireSfxL();
        G_MFL.SendMessage("Play");

        RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
        if (Physics.Raycast(G_FirePositionL.position, G_FirePositionL.forward, out hit, 100.0f))//레이 탐색 
        {
            if (hit.collider.tag == "Enemy")//적 탐지시
            {
                object[] _params = new object[2];//레이피격시 내부 정보추출
                _params[0] = hit.point;
                _params[1] = 50;
                //몬스터에 대미지 입히는 함수
                hit.collider.gameObject.SendMessage("E_OnAttack", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "Player")//적 탐지시
            {
                object[] _params = new object[2];//레이피격시 내부 정보추출
                _params[0] = hit.point;
                _params[1] = 100;
                //몬스터에 대미지 입히는 함수
                hit.collider.gameObject.SendMessage("OnAttack", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "SB")//시작 버튼 탐지시
            {
                Debug.Log("hit");
                G_BulletL++;
                //씬호출
                hit.collider.gameObject.SendMessage("LevelScene");
            }
            if (hit.collider.tag == "HTPB")//튜토리얼버튼 탐지시
            {
                G_BulletL++;
                //씬호출
                hit.collider.gameObject.SendMessage("HowToPlayScene", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "EB")//나가기 버튼 탐지시
            {
                G_BulletL++;
                //씬호출
                hit.collider.gameObject.SendMessage(" ExitScene", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "EZTB")// 쉬움상대 버튼 탐지시
            {
                G_BulletL++;
                P_Go.SendMessage("Set");
                //씬호출
                hit.collider.gameObject.SendMessage("EasyScene", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "NTB")// 중간상대 버튼 탐지시
            {
                G_BulletL++;
                //씬호출
                hit.collider.gameObject.SendMessage("EasyScene", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "BTB")// 어려운상대 버튼 탐지시
            {
                G_BulletL++;
                //씬호출
                hit.collider.gameObject.SendMessage(" ExitScene", SendMessageOptions.DontRequireReceiver);
            }

        }
        G_BulletL--;

        G_isReadyL = false;
    }
    void G_FireFL()//총발사 실패
    {
        HandRAniL.SetTrigger("FireFalse");
        G_TriggerBackL = false;
        if (!GunSfxL.isPlaying)
        {
            GunSfxL.PlayOneShot(FireFSfx);
            //OVRHaptics.Channels[0].Preempt(new OVRHapticsClip(FireFSfx));이것 좃같은 물건이다.
        }
        Debug.Log("Icantfire");
        //오디오
    }

    void G_ReloadL()//재장전
    {

        if (G_isReadyL== false && G_TriggerBackL == false)
        {
            if (G_BulletL <= 0)
            {

                HandRAniL.SetBool("GisReady", false);
                HandRAniL.SetTrigger("Reload");
                if (!GunSfxL.isPlaying)
                {
                    GunSfxL.PlayOneShot(Reload);
                    //OVRHaptics.Channels[0].Preempt(new OVRHapticsClip(Reload));이것 좃같은 물건이다.
                }
                G_isReadyL = false;
                G_TriggerBackL = true;
            }
            else
            {
                GunSfxL.PlayOneShot(Reload);
                if (!GunSfxL.isPlaying)
                {
                    GunSfxL.PlayOneShot(Reload);
                    //OVRHaptics.Channels[0].Preempt(new OVRHapticsClip(Reload));이것 좃같은 물건이다.
                }
                HandRAniL.SetBool("GisReady", true);
                HandRAniL.SetTrigger("Reload");

                if (!GunSfxL.isPlaying)
                {
                    GunSfxL.PlayOneShot(Reload);
                    //OVRHaptics.Channels[0].Preempt(new OVRHapticsClip(Reload));이것 좃같은 물건이다.
                }
                Debug.Log("reload");
                G_isReadyL = true;
                G_TriggerBackL = true;
            }
        }
    }
    public void H_changeL()//손모양 교체
    {
        G_isGrapL = true;
        G_gunhandL.SetActive(true);
        HandnGunL.SetActive(false);
    }

    void G_allReload()//총의 총알 초기화
    {
        G_BulletL = 6;
    }
    

    void FireSfxL()//발사음 랜덤재생
    {
        GunSfxL.clip = FireSfx[Random.Range(0, 2)];
        //OVRHaptics.Channels[0].Preempt(new OVRHapticsClip(GunSfxL.clip));이것 좃같은 물건이다.
        GunSfxL.Play();
    }
}
