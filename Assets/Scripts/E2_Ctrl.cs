using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E2_State { Walk, Idle, Attack, Die, Hit, PDie };
public class E2_Ctrl : MonoBehaviour
{

    private Quaternion turn = Quaternion.identity;
    //애니메이터
    private Animator animator;
    //몬스터 사망여부
    private bool isdie = false;
    //플레이어 사망여부
    private bool ispdie = false;
    //적캐릭터 대기시작
    private bool isIdle = false;
    //캐릭터 회전
    private bool isturn = false;
    private Vector3 vector3;
    //콜리더 충돌
    // bool isStop = false;
    //시작신호
    public float StartSigh = 3.0f;
    //현재 상태 저장
    public E2_State e2_state = E2_State.Idle;
    //캐릭터 이동 속도
    float speed = 2.0f;
    //적 피격판정 콜라이더
    Collider E2Collider;
    //적 피격횟수
    public int E_HitCount = 0;
    //발사위치
    public Transform G_FirePosition;
    //총알수
    private int G_Bullet = 6;
    //플레이어 게임오브젝트
    public GameObject P_Go;
    //총사운드
    public AudioSource E1GunSfx;
    public AudioSource WhizSfx;
    public AudioClip[] WhizBSfx;
    public AudioClip[] FireSfx;
    //총격 이펙트
    public GameObject G_MF;
    public GameObject E2;
    //발사속도
    float E1_BulletRpm = 60.0f;

    void Start()
    {
        E2Collider = GetComponent<Collider>();
        //적캐릭터 행동 상태 체크
        StartCoroutine(CheckE1State());
        //적캐릭터의 상태에따라 동작
        StartCoroutine(E1Action());
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        this.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
        //레이그리기
        Debug.DrawRay(G_FirePosition.position, G_FirePosition.forward * 100, Color.green);
        if (E_HitCount <= 1)
        {
            Debug.Log("reattack");
            animator.SetBool("isattack", true);
            animator.SetBool("ishit", false);
        }
        if (isIdle)
        {
            //시작카운트 차감 시작
            StartSigh -= Time.deltaTime;
            
        }
        if (ispdie)
        {
            PDie();
        }
        if(isturn)
        {
            Debug.Log("StopTurn");
            //transform.Rotate(new Vector3(0, -180.0f, 0) * Time.deltaTime);
            //E2.transform.rotation = Quaternion.Euler(0, 90.0f, 0);
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
            else if (StartSigh <= 0 && E_HitCount<2)
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
                    break;

                //attack 상태
                case E2_State.Attack:
                    StartTurn();
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
        E2.transform.rotation = Quaternion.Euler(0, 90.0f, 0);
        Debug.Log("Die");
        if (E_HitCount > 1)//피격횟수가 초과시 죽음
        {
            StopAllCoroutines();
            //Debug.Log("Die");
            animator.SetBool("isdie", true);
            GetComponent<AudioSource>().Play();
            E2Collider.enabled = !E2Collider.enabled;//콜라이더 제거
        }

        if (E_HitCount <= 1)//피격획수 미달시 다시 공격
        {
            Debug.Log("reattack");
            animator.SetTrigger("reattack");
            animator.SetBool("ishit",true);
        }
        //피격 애니메이션 사운드 필요
        E_HitCount++;//적피격
    }

    void PDie()
    {
        E2.transform.rotation = Quaternion.Euler(0, 90.0f, 0);
        Debug.Log("Kiss");
        animator.SetTrigger("ispdie");
        //this.transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
    }

    void P_OnAttack()
    {
        ispdie = true;
    }

    void IsStop()
    {
        speed = 0;
        animator.SetTrigger("isidle");
        isIdle = true;
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "StopE1")
        {
            Debug.Log("Stoped");
            IsStop();
            Destroy(col.gameObject);
        }
    }

    void E_Startattack()
    {
        isturn = true;
        //transform.Rotate(new Vector3(0,90.0f, 0)*Time.deltaTime);
        E2.transform.rotation = Quaternion.Euler(0, 90.0f, 0);
        animator.SetTrigger("isattack");
        StartCoroutine(CoReady());
    }
    void G_Fire()//발사
    {
        //이펙트 사운드
        FireSfxR();
        G_MF.SendMessage("Play");
        //레이케스트라인 안에 들어온 물체 변수
        RaycastHit hit;
        if (Physics.Raycast(G_FirePosition.position, G_FirePosition.forward, out hit, 100.0f))//레이 탐색 
        {
            Debug.Log(hit.transform.name);
            if (hit.collider.tag == "Player")//플레이어 탐지시
            {
                Debug.Log("Hit");
                //플레이어에 대미지 입히는 함수
                //hit.collider.gameObject.SendMessage("P_Die", SendMessageOptions.DontRequireReceiver);
                //적캐릭터에게 플레이어 사망 전달
                P_OnAttack();
            }
        }
        if (P_Go == null)
        {
            //적캐릭터에게 플레이어 사망 전달
            P_OnAttack();
        }
        else
        {
            this.transform.LookAt(P_Go.transform);//플레이어 따라 시선 이동
        }

        //this.E1_FireReady = false;
    }
    void FireSfxR()//발사음 랜덤재생
    {

        E1GunSfx.clip = FireSfx[Random.Range(0, 4)];
        E1GunSfx.Play();
        WhizSfxR();
    }
    void WhizSfxR()//탄 지나가는 사운드
    {
        Debug.Log("DD");
        WhizSfx.clip = WhizBSfx[Random.Range(0, 6)];
        WhizSfx.Play();
        G_Bullet--;
    }
    void StartTurn()
    {
        animator.SetTrigger("isturn");
        E_Startattack();
    }

    IEnumerator CoReady()
    {
        while (!isdie)
        {
            Debug.Log(G_Bullet);
            if (G_Bullet > 3)
            {
                FireSfxR();
                yield return new WaitForSeconds(60.0f / this.E1_BulletRpm);
            }
            else if (0 < G_Bullet && G_Bullet <= 3)
            {
                G_Fire();
                yield return new WaitForSeconds(60.0f / this.E1_BulletRpm);
            }
            if (G_Bullet <= 0)
            {
                break;
            }
            yield return null;
        }
    }
}
