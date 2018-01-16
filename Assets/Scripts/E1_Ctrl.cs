using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum E1_State { Idle, Attack, Die, Hit, PDie };
public class E1_Ctrl : MonoBehaviour
{

    //애니메이터
    private Animator animator;
    //몬스터 사망여부
    private bool isdie = false;
    //플레이어 사망여부ㅡ
    private bool ispdie = false;
    //콜리더 충돌
    // bool isStop = false;
    //시작신호
    public float StartSigh = 3.0f;
    //현재 상태 저장
    public E1_State e1_state = E1_State.Idle;
    //캐릭터 이동 속도
    float speed = 2.0f;
    //적캐릭터 숨기기위한 변수
    public GameObject E1;
    //공격시 나올 적 캐릭터
    public GameObject E1attack;
    //적 피격판정 콜라이더
    Collider E1Collider;
    //적 피격횟수
    private int E_HitCount=0;
    //게임관리 오디오 소스
    public AudioSource GameplaySfx;
    public AudioClip GameStartSfx;
    public AudioClip GameEndSfx;
    //적 캐릭터 음성 소스
    public AudioSource E1Sfx;


    void Start()
    {
        E1Collider=GetComponent<Collider>();
        //적캐릭터 행동 상태 체크
        StartCoroutine(CheckE1State());
        //적캐릭터의 상태에따라 동작
        StartCoroutine(E1Action());
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        StartSigh -= Time.deltaTime;
        if(ispdie)
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
                e1_state = E1_State.Idle;
            }
            else if (StartSigh <= 0)
            {
                e1_state = E1_State.Attack;
            }
            else if (ispdie == true)
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
                    E1attack.SetActive(true);
                    E1attack.SendMessage("Gun_isReady");
                    E1.SetActive(false);
                    break;

                //pdie 상태
                case E1_State.PDie:
                    this.transform.position += this.transform.position + new Vector3(0, 0, speed)*Time.deltaTime;
                    break;
            }
            yield return null;
        }
    }

    void E_OnAttack()
    {
        Debug.Log("Die");
        
        StopAllCoroutines();
        //Debug.Log("Die");
        animator.SetTrigger("isdie");
        Destroy(E1attack);
        E1Collider.enabled=!E1Collider.enabled;//콜라이더 제거
        GameEnd();//게임끝 
        Invoke("GameEndCall", 2.0f);
        //피격 애니메이션 사운드 필요
        E_HitCount++;//적피격
    }

    void PDie()//플레이어가 죽음
    {
        Debug.Log("Walk");
        this.transform.position +=  new Vector3(0, 0, -speed)*Time.deltaTime;
    }

    void P_OnAttack()//플레이어가 공격을 당함(걸어가는 애니메이션 실행)
    {
        ispdie = true;
        animator.SetTrigger("ispdie");
        GameEnd();
    }

    void IsStop()//정지 명령
    {
        speed = 0;
        animator.SetTrigger("isspread");
        Invoke("GameEndCall", 7.0f);
    }

    void GameStart()//게임 시작 관리
    {
        GameplaySfx.PlayOneShot(GameStartSfx);
    }
    void GameEnd()//게임 끝 관리
    {
        GameplaySfx.PlayOneShot(GameEndSfx);
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "StopE1")//정지 콜라이더 충돌시 정지
        {
            Debug.Log("Stoped");
            IsStop();
        }
    }
    void GameEndCall()
    {
        SceneManager.LoadScene(1);
    } 
}
