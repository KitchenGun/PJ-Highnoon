using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E1_State { Idle, Attack, Die, Hit, PDie };
public class E1_Ctrl : MonoBehaviour
{

    //애니메이터
    private Animator animator;
    //몬스터 사망여부
    private bool isdie = false;
    //플레이어 사망여부ㅡ
    private bool ispdie = false;
    //시작신호
    public float StartSigh = 3.0f;
    //현재 상태 저장
    public E1_State e1_state = E1_State.Idle;
    //캐릭터 이동 속도
    public float speed = 3.0f;
    //적캐릭터 숨기기위한 변수
    public GameObject E1;
    //공격시 나올 적 캐릭터
    public GameObject E1attack;

    void Start()
    {
        //적캐릭터 행동 상태 체크
        StartCoroutine(CheckE1State());
        //적캐릭터의 상태에따라 동작
        StartCoroutine(E1Action());
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        StartSigh -= Time.deltaTime;
    }

    //일정 간격으로 몬스터행동 체크 및 스테이츠 변경
    IEnumerator CheckE1State()
    {
        while (!isdie)
        {
            yield return new WaitForSeconds(0.2f);

            if (StartSigh > 0)
            {
                e1_state = E1_State.Idle;
            }
            else if (StartSigh <= 0)
            {
                e1_state = E1_State.Attack;
            }
            else
            {
                e1_state = E1_State.PDie;
            }
        }
    }

    IEnumerator E1Action()
    {
        while (!isdie)
        {
            switch (e1_state)
            {
                //idle 상태
                case E1_State.Idle:
                    break;

                //attack 상태
                case E1_State.Attack:
                    E1.SetActiveRecursively(false);
                    E1attack.SetActiveRecursively(true);
                    break;

                //pdie 상태
                case E1_State.PDie:
                    animator.SetBool("ispdie", true);
                    transform.Translate(new Vector3(0.0f, 0.0f, -speed) * Time.deltaTime);
                    break;
            }
            yield return null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "StopE1")
        {
            speed = 0.0f;
            animator.SetTrigger("isspread");
        }
    }

    void E_OnAttack()
    {
        Debug.Log("Die");
        animator.SetBool("isdie", true);
    }
}
