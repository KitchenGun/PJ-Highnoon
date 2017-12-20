using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E1_Ctrl : MonoBehaviour {

    public enum E1_State
    {
        Idle,//대기 
        Attack,//공격
        Die,//사망
        PDie//플레이어사망

    };
    //적캐릭터의 현재 상태 정보를 저장할 Enum 변수
    public E1_State e1_state = E1_State.Idle;

    private Animator animator;
    //적캐릭터 사망여부
    private bool isdie = false;
    //적 캐릭터 생명 변수
    private int hp = 100;

    private void Start()
    {
        //Animator 컴포넌트 할당
        animator = this.gameObject.GetComponent<Animator>();
        //일정한 간격으로 몬스터의 행동 상태를 체크하는 코루틴 함수 실행
        StartCoroutine(this.CheckEnemy1());

        //몬스터의 상태에 따라 동작하는 루틴을 실행하는 코루틴 함수 실행
        StartCoroutine(this.E1_Action());

    }

    IEnumerator CheckEnemy1()
    {
    }

    IEnumerator E1_Action()
    {
            while (!isdie)
            {
                switch (E1_State)
                {
                //대기상태
                case E1_State.Idle:
                    break;

                case E1_State.Attack:
                    animator.SetBool("isattack",true);
                    break;
                }

            }
    }
}
