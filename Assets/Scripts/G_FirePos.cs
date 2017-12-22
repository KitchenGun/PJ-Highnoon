using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FirePos : MonoBehaviour
{

    public Transform G_FirePosition;//발사위치
    private int G_Bullet = 6;//6발
    private bool G_isReady = false;//총을 쏠수 있는가?

    //애니메이션

    

    // Use this for initialization
    void Start ()
    {
        G_isReady = true;//발사 가능
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Mouse0)&&G_isReady==true)//마우스버튼 클릭시 발포성공
        {
            G_Fire();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && G_isReady != true)//마우스버튼 클릭시 발포 실패
        {
            G_FireF();
        }
    }

    void G_Fire()//발사
    {
        RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
        if (Physics.Raycast(G_FirePosition.position, transform.forward, out hit, 100.0f))//레이 탐색 
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
                //씬호출
                hit.collider.gameObject.SendMessage("StartScene", SendMessageOptions.DontRequireReceiver);
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

        //오디오
    }

    void G_Reload()//재장전
    {

        G_isReady = true;
    }
}
