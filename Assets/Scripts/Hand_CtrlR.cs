using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand_CtrlR : MonoBehaviour
{
    public GameObject P_Go;//플레이어
    //public OVRInput.Controller Controller;

    public Transform G_FirePositionR;//발사위치
    public GameObject HandnGunR;//총없는손
    public GameObject G_gunhandR;//총있는손
    private bool G_isGrapR = false;//총 잡았는가?
    private bool G_isReadyR = false;//총을 쏠수 있는가?
    private bool G_TriggerBack;//노리쇠후퇴
    public GameObject G_MFR;//머즐플래쉬

    public Animator HandRAniR;//애니메이터

    public AudioSource GunSfxR;
    public AudioClip Reload;//사운드&진동
    public AudioClip[] FireSfx;
    public AudioClip FireFSfx;
    public AudioClip Grap;
    private int GrapCR=0;//잡는 함수 호출 횟수

    private int G_BulletR = 6;//6발


    private void Start()
    {
        G_isGrapR = false;
        G_isReadyR = true;
        G_TriggerBack = true;
        G_gunhandR.SetActive(false);
        HandnGunR.SetActive(true);
    }

    void Update()
    {
        float Firetrigger_resultf =
            OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);//방아쇠컨트롤러버튼

        float G_Reloadf =
            OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;//스틱컨트롤러y축 버튼
        //Debug.Log(Firetrigger_resultf);
        //Debug.Log(G_Reloadf);
        Debug.DrawRay(G_FirePositionR.position, G_FirePositionR.forward * 100, Color.green);
        if (Input.GetKeyDown(KeyCode.A))
        {
            H_changeR();
        }

        if (G_isGrapR == true)
        {
            if (G_isReadyR == true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || Firetrigger_resultf >= 0.9f)//마우스버튼 클릭시 발포성공
                {
                    if (G_Reloadf > -0.3f)
                    {
                        G_FireR();
                        Debug.Log("fire");
                    }
                }
            }
            else
            {
                HandRAniR.SetBool("GisReady", false);
                if (Input.GetKeyDown(KeyCode.Mouse0) || Firetrigger_resultf >= 0.9f)//마우스버튼 클릭시 발포 실패
                {
                    if(G_TriggerBack!=false)
                    {
                        G_FireFR();
                        HandRAniR.SetTrigger("FireFalse");
                    }
                    
                }
                if (Input.GetKeyDown(KeyCode.R) || G_Reloadf < -0.8f)//재장전
                {
                    G_ReloadR();
                }
            }
        }

    }
    void G_FireR()//발사
    {
        G_TriggerBack = false;
        //이펙트 사운드
        FireSfxR();
        HandRAniR.SetTrigger("Fire");
        G_MFR.SendMessage("Play");

        RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
        if (Physics.Raycast(G_FirePositionR.position, G_FirePositionR.forward, out hit, 100.0f))//레이 탐색 
        {
            if (hit.collider.tag == "Enemy")//적 탐지시
            {
                
                //몬스터에 대미지 입히는 함수
                hit.collider.gameObject.SendMessage("E_OnAttack", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "E1Attack")//적 탐지시
            {

                //몬스터에 대미지 입히는 함수
                hit.collider.gameObject.SendMessage("E_OnAttack", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "Player")//적 탐지시
            {
               
                //몬스터에 대미지 입히는 함수
                hit.collider.gameObject.SendMessage("OnAttack", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "SB")//시작 버튼 탐지시
            {
                Debug.Log("hit");
                //씬호출
                hit.collider.gameObject.SendMessage("LevelScene");
            }
            if (hit.collider.tag == "HTPB")//튜토리얼버튼 탐지시
            {
                //씬호출
                hit.collider.gameObject.SendMessage("HowToPlayScene", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "EB")//나가기 버튼 탐지시
            {
                //씬호출
                hit.collider.gameObject.SendMessage(" ExitScene", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "EZTB")// 쉬움상대 버튼 탐지시
            {
                P_Go.SendMessage("Set");//위치 변경
                //씬호출
                hit.collider.gameObject.SendMessage("EasyScene", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "NTB")// 중간상대 버튼 탐지시
            {
                //씬호출
                hit.collider.gameObject.SendMessage("EasyScene", SendMessageOptions.DontRequireReceiver);
            }

            if (hit.collider.tag == "BTB")// 어려운상대 버튼 탐지시
            {
                //씬호출
                hit.collider.gameObject.SendMessage(" ExitScene", SendMessageOptions.DontRequireReceiver);
            }

        }
        G_BulletR--;

        G_isReadyR = false;
    }
    void G_FireFR()//총발사 실패
    {

        G_TriggerBack = false;
        if (!GunSfxR.isPlaying)
        {
            //OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(FireFSfx));이것 좃같은 물건이다.
            GunSfxR.PlayOneShot(FireFSfx);
        }
        Debug.Log("Icantfire");
        //오디오
    }

    void G_ReloadR()//재장전
    {
        
        if (G_isReadyR == false && G_TriggerBack == false)
        {
            if (G_BulletR <= 0)
            {
                
                HandRAniR.SetBool("GisReady", false);
                HandRAniR.SetTrigger("Reload");
                if (!GunSfxR.isPlaying)
                {
                    //OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(Reload));이것 좃같은 물건이다.
                    GunSfxR.PlayOneShot(Reload);
                }
                G_isReadyR = false;
                G_TriggerBack = true;
            }
            else
            {
                GunSfxR.PlayOneShot(Reload);
                if (!GunSfxR.isPlaying)
                {
                    //OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(Reload));이것 좃같은 물건이다.
                    GunSfxR.PlayOneShot(Reload);
                }
                HandRAniR.SetBool("GisReady", true);
                HandRAniR.SetTrigger("Reload");
               
                if (!GunSfxR.isPlaying)
                {
                    //OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(Reload));이것 좃같은 물건이다.
                    GunSfxR.PlayOneShot(Reload);
                }
                Debug.Log("reload");
                G_isReadyR = true;
                G_TriggerBack = true;
            }
        }
    }


    public void H_changeR()//손모양 교체
    {
        if(GrapCR<1)//사운드
        {
            GunSfxR.PlayOneShot(Grap);
            //OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(Grap));이것 좃같은 물건이다.
        }

        G_isGrapR = true;
        G_gunhandR.SetActive(true);
        HandnGunR.SetActive(false);
        GrapCR++;//잡았다.
    }

    void G_allReload()//총의 총알 초기화
    {
        G_BulletR = 6;
    }

    void FireSfxR()//발사음 랜덤재생
    {
        GunSfxR.clip = FireSfx[Random.Range(0, 2)];
        //OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(GunSfxR.clip));이것 좃같은 물건이다.
        GunSfxR.Play();
    }

}