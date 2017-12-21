using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FirePos : MonoBehaviour
{

    public Transform G_FirePosition;//발사위치
    private int G_Bullet = 6;//6발
    private bool G_isReady = false;//총을 쏠수 있는가?
    

    // Use this for initialization
    void Start ()
    {
        G_isReady = false;//발사 불가능
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Mouse0)&&G_isReady==true)//마우스버튼 클릭시
        {
            G_Fire();
        }
        else
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
        }
        G_Bullet--;
        if(G_Bullet<=0)
        {
            G_isReady = false;
        }
    }
    void G_FireF()//총발사 실패
    {

        //오디오
    }
}
