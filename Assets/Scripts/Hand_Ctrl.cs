using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand_Ctrl : MonoBehaviour
{
    //public OVRInput.Controller Controller;

    public Transform G_FirePosition;//발사위치
    public MeshRenderer HandnGun;//총없는손
    public MeshRenderer HandGun1;//총없는손
    public MeshRenderer HandGun;//총있는손
    private bool G_isGrap=false;//총 잡았는가?
    private bool G_isReady = false;//총을 쏠수 있는가?

    private int G_Bullet = 6;//6발
    

    private void Start()
    {
        G_isGrap = false;
        G_isReady = true;
        HandGun.enabled = false;
        HandnGun.enabled = true;
        HandGun1.enabled = false;
        
    }

    void Update()
    {
		this.transform.localPosition = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch)+new Vector3(0,2f,0);//Vector3(-0.125f,2,0)
		this.transform.localRotation=OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch)*new Quaternion(10f,10f,10f,10f);
        Debug.DrawRay(G_FirePosition.position, Vector3.up * -100, Color.green);
        if (Input.GetKeyDown(KeyCode.A))
        {
            H_change();
        }
       
        if (G_isGrap == true)
        {
            if (G_isReady == true)
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || OVRInput.Get(OVRInput.RawButton.LIndexTrigger))//마우스버튼 클릭시 발포성공
                {
                    G_Fire();
                    Debug.Log("fire");
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Mouse0) || OVRInput.Get(OVRInput.RawButton.LIndexTrigger))//마우스버튼 클릭시 발포 실패
                {
                    G_FireF();
                }
                if (Input.GetKeyDown(KeyCode.R)||OVRInput.GetDown(OVRInput.Button.PrimaryThumbstickDown))//재장전
                {
                    G_Reload();
                }
            }
        }

    }
    void G_Fire()//발사
    {
        RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
        if (Physics.Raycast(G_FirePosition.position, Vector3.forward, out hit, 100.0f))//레이 탐색 
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

            if (hit.collider.name == "SB")//시작 버튼 탐지시
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
        G_Bullet--;
        G_isReady = false;
    }
    void G_FireF()//총발사 실패
    {
        Debug.Log("Icantfire");
        //오디오
    }

    void G_Reload()//재장전
    {
        if(G_Bullet<=0)
        {
            G_isReady = false;
        }
        else
        {
            G_isReady = true;
        }
    }

    void H_change()//손모양 교체
    {
        G_isGrap = true;
        HandGun.enabled=true;
        HandnGun.enabled=false;
        HandGun1.enabled = true;
    }

    void G_allReload()//총의 총알 초기화
    {
        G_Bullet = 6;
    }
    private void OnCollisionEnter(Collision collision)//손에 충돌시
    {
        if (collision.gameObject.tag == "Gun")
        {
            H_change();//손모양 교체
        }
    }

}
