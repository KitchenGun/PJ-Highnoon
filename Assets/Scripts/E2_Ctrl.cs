using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E2_State { Idle, Attack, Die, Hit, PDie };
public class E2_Ctrl : MonoBehaviour
{

    //애니메이터
    private Animator animator;
    //몬스터 사망여부
    private bool isdie = false;
    //플레이어 사망여부ㅡ
    private bool ispdie = false;
    //공격시작판정
    private bool isattack = false;
    //콜리더 충돌
    // bool isStop = false;
    //시작신호
    public float StartSigh = 3.0f;
    //현재 상태 저장
    public E2_State e2_state = E2_State.Idle;
    //캐릭터 이동 속도
    float speed =3.0f;
    //적캐릭터지정
    public GameObject e2;

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
        if(isattack)
        {
            StartSigh -= Time.deltaTime;
        }
        if (ispdie)
        {
            PDie();
        }
    }

    //일정 간격으로 몬스터행동 체크 및 스테이츠 변경
    IEnumerator CheckE1State()
    {
        while (!isdie)
        {
            yield return new WaitForSeconds(0.2f);

            if (StartSigh > 0)
            {
                e2_state = E2_State.Idle;
            }
            else if (StartSigh <= 0)
            {
                e2_state = E2_State.Attack;
            }
            else if (ispdie == true)
            {
                e2_state = E2_State.PDie;
            }
        }
    }

    IEnumerator E1Action()
    {
        while (!isdie)
        {
            switch (e2_state)
            {
               //idle 상태
               case E2_State.Idle:
               //this.transform.position += this.transform.position + new Vector3(0, 0, speed);
               this.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
               break;

               //attack 상태
               case E2_State.Attack:
                    animator.SetTrigger("isattack");
                    break;

               //pdie 상태
               case E2_State.PDie:
                   break;
            }
           yield return null;
        }
    }

    void E_OnAttack()
    {
        StopAllCoroutines();
        Debug.Log("Die");
        animator.SetBool("isdie", true);
        GetComponent<AudioSource>().Play();
    }

    void PDie()
    {
        animator.SetTrigger("ispdie");
    }

    void P_OnAttack()
    {
        ispdie = true;
        animator.SetTrigger("ispdie");
    }

    void IsStop()
    {
        speed = 0;
        transform.Rotate(new Vector3(0, 90.0f, 0));
    }

    void Attack()
    {
        e2.SendMessage("StartAttack");
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "StopE1")
        {
            Debug.Log("Stoped");
            IsStop();
            isattack = true;
            animator.SetTrigger("isturn");
            Invoke("Attack", 3.0f);
        }
    }
 }
