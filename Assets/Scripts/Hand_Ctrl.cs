using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand_Ctrl : MonoBehaviour
{

    //public OVRInput.Controller Controller;

    public Transform G_FirePositionL;//발사위치
    public GameObject HandnGunL;//총없는손
    public GameObject G_gunhandL;//총있는손
    private bool G_isGrapL = false;//총 잡았는가?
    private bool G_isReadyL = false;//총을 쏠수 있는가?
    public GameObject G_MF;//머즐플래쉬

    public Animator HandRAniL;//애니메이터

    public AudioSource GunSfxL;
    public AudioClip Reload;//사운드
    public AudioClip FireSfx;
    public AudioClip FireFSfx;

    private int G_BulletL = 6;//6발


    private void Start()
    {
        G_isGrapL = false;
        G_isReadyL = true;
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

                HandRAniL.SetBool("GisReady", true);
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
                    G_FireFL();
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
        GunSfxL.PlayOneShot(FireSfx);
        G_MF.SendMessage("Play");
        HandRAniL.SetTrigger("Fire");//총사격 애니메이션

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

            if (hit.collider.name == "Player")//적 탐지시
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
        G_BulletL--;
       
        G_isReadyL = false;
    }
    void G_FireFL()//총발사 실패
    {
        if (!GunSfxL.isPlaying)
        {
            GunSfxL.PlayOneShot(FireFSfx);
        }
        HandRAniL.SetTrigger("FireFalse");
        Debug.Log("Icantfire");
        //오디오
    }

    void G_ReloadL()//재장전
    {
        GunSfxL.PlayOneShot(Reload);
        HandRAniL.SetTrigger("Reload");
        if (G_isReadyL == false)
        {
            if (G_BulletL <= 0)
            {
                G_isReadyL = false;
            }
            else
            {
                Debug.Log("reload");
                G_isReadyL = true;
            }
        }
    }


    void H_changeL()//손모양 교체
    {
        G_isGrapL = true;
        G_gunhandL.SetActive(true);
        HandnGunL.SetActive(false);
    }

    void G_allReload()//총의 총알 초기화
    {
        G_BulletL = 6;
    }
    private void OnCollisionEnter(Collision collision)//손에 충돌시
    {
        float HandRG =
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        Debug.Log(HandRG);
        if (collision.gameObject.tag == "Gun" && HandRG >= 0.8f)
        {
            Debug.Log("충돌");
            H_changeL();//손모양 교체
        }
    }

}
