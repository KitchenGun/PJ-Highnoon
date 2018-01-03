using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class E1_Ctrl : MonoBehaviour
{
    //대기,공격,사망,플레이어사망
    public enum E1_State
    { Idle, Attack, Die, PDie };
    //적캐릭터의 현재 상태 정보를 저장할 Enum 변수
    public E1_State e1_state = E1_State.Idle;

    public Animator animator;
    //적캐릭터 사망여부
    private bool isdie = false;
    //적 캐릭터 생명 변수
    public int E1_hp = 100;
    //공격 사거리
    public float AttackDist = 2.0f;
    //발사위치
    public Transform G_FirePosition;
    //6발
    private int G_Bullet = 6;
    //총발사 가능 여부
    private bool G_isReady = false;
    //시작 카운트
    public float StartCount = 3.0f;


    void Start()
    {
        //Animator 컴포넌트 할당
        animator = this.gameObject.GetComponent<Animator>();
        //일정한 간격으로 몬스터의 행동 상태를 체크하는 코루틴 함수 실행
        StartCoroutine(this.CheckEnemy1());

        //몬스터의 상태에 따라 동작하는 루틴을 실행하는 코루틴 함수 실행
        StartCoroutine(this.E1_Action());

    }
    private void Update()
    {
        StartCount -= Time.deltaTime;
    }
    IEnumerator CheckEnemy1()
    {
        while (!isdie)
        {
            yield return new WaitForSeconds(0.2f);
            if(StartCount<=0)
            {
                e1_state = E1_State.Attack;
            }
            else
            {
                e1_state = E1_State.Idle;
            }

        }
    }

    IEnumerator E1_Action()
    {
        while (!isdie)
        {
            switch (e1_state)
            {
                //대기상태
                case E1_State.Idle:

                    animator.SetBool("IsAttack", false);
                    break;

                //공격상태
                case E1_State.Attack:
                    E1_Attack();
                    animator.SetBool("Isattack", true);
                    break;
            }
            yield return null;
        }
    }
   
    //적 캐릭터 피격
    void E_OnAttack(object[] _params)
    {
        Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));

        //맞은 총알의 데미지를 추출해 적 캐릭터 체력 차감
        E1_hp -= (int)_params[1];
        if (E1_hp <=0)
        {
            E1_Die();
        }

        //IsHit트리거 발생
        animator.SetTrigger("isHit");
    }
    //적 캐릭터 사망
    void E1_Die()
    {
        //모든 코루틴 정지
        StopAllCoroutines();
        //사망판정
        isdie = true;
        //적캐릭터 상태 변환
        e1_state = E1_State.Die;
        //적캐릭터 사망 애니메이션 실행
        animator.SetTrigger("IsDie");
        //다음 스테이지로 넘김
        SceneManager.LoadScene("normal");

    }

    void E1_Attack()
    {
        RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
        if (Physics.Raycast(G_FirePosition.position, transform.forward, out hit, 100.0f))//레이 탐색 
        {
            if (hit.collider.tag == "Player")//적 탐지시
            {
                object[] _params = new object[2];//레이피격시 내부 정보추출
                _params[0] = hit.point;
                _params[1] = 50;
                //플레이어에 대미지 입히는 함수
                hit.collider.gameObject.SendMessage("P_OnAttack", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void Player_Die()
    {
        StopAllCoroutines();
        animator.SetBool("IsPDie",true);
    }
}
