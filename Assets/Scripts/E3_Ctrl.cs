using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum E3_State { Idle, Attack, Die, Hit, PDie };
public class E3_Ctrl : MonoBehaviour
{

    private Quaternion turn = Quaternion.identity;
    //애니메이터
    private Animator animator;
    //몬스터 사망여부
    private bool isdie = false;
    //플레이어 사망여부
    private bool ispdie = false;
    int a = 0;
    //적캐릭터 대기시작
    private bool islough = false;
    //캐릭터 회전
    private bool isturn = false;
    //걷기 시작
    private bool iswalk = false;
    private Vector3 vector3;
    //콜리더 충돌
    // bool isStop = false;
    //시작신호
    public float StartSigh = 3.0f;
    //현재 상태 저장
    public E3_State e3_state = E3_State.Idle;
    //캐릭터 이동 속도
    float speed = 2.0f;
    //적 피격판정 콜라이더
    Collider E3Collider;
    //적 피격횟수
    public int E_HitCount = 0;
    int b = 0;
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
    public GameObject E3;
    //발사속도
    float E1_BulletRpm = 120.0f;

    void Start()
    {
        E3Collider = GetComponent<Collider>();
        //적캐릭터 행동 상태 체크
        StartCoroutine(CheckE3State());
        //적캐릭터의 상태에따라 동작
        StartCoroutine(E3Action());
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if(iswalk)
        {
            this.transform.position += new Vector3(0, 0, -speed) * Time.deltaTime;
        }
        StartSigh -= Time.deltaTime;
        //레이그리기
        Debug.DrawRay(G_FirePosition.position, G_FirePosition.forward * 100, Color.green);
        //if (E_HitCount <= 1)
        //{
        //    Debug.Log("reattack");
        //    animator.SetBool("isattack", true);
        //    animator.SetBool("ishit", false);
        //}
        if (isdie)
        {
            PDie();
        }
    }

    //일정 간격으로 몬스터행동 체크 및 스테이츠 변경
    IEnumerator CheckE3State()
    {
        while (!isdie)
        {
            yield return new WaitForSeconds(0.2f);

            if (StartSigh > 0)
            {
                e3_state = E3_State.Idle;
            }
            else if (StartSigh <= 0 && E_HitCount < 2)
            {
                e3_state = E3_State.Attack;
            }
            else if (ispdie == true)
            {
                yield return new WaitForSeconds(0.2f);
                e3_state = E3_State.PDie;
            }
        }
    }

    IEnumerator E3Action()
    {
        while (!isdie)
        {
            switch (e3_state)
            {
                //idle 상태
                case E3_State.Idle:
                    break;

                //attack 상태
                case E3_State.Attack:
                    {
                        E_Startattack();
                        yield return new WaitForSeconds(1.2f);
                        if (G_Bullet > 4)
                        {
                            FireSfxR();
                            Invoke("WhizSfxR", 0.1f);
                            yield return new WaitForSeconds(60.0f / this.E1_BulletRpm);
                        }
                        else if (0 < G_Bullet && G_Bullet <= 4)
                        {

                            if (!ispdie)
                            {
                                yield return new WaitForSeconds(60.0f / this.E1_BulletRpm);
                                G_Fire();
                            }
                            else if (ispdie)
                            {
                                PDie();
                                break;
                            }
                        }
                        if (G_Bullet <= 0)
                        {
                            break;
                        }
                        break;

                    }
                //pdie 상태
                case E3_State.PDie:
                    {
                        PDie();
                    }
                    break;
            }
            yield return null;
        }
    }

    void E_OnAttack()//적 공격받음
    {
        Debug.Log("Die");
        if (E_HitCount == 1)//피격횟수가 초과시 죽음
        {
            StopAllCoroutines();
            animator.SetTrigger("isdie");
            GetComponent<AudioSource>().Play();
            E3Collider.enabled = !E3Collider.enabled;//콜라이더 제거
        }

        if (E_HitCount == 0)//피격획수 미달시 다시 공격
        {
            animator.SetTrigger("ishit");
            if (b < 1)
            {
                Debug.Log("reattack");
                animator.SetTrigger("reattack");
            }
            b++;

        }
        //피격 애니메이션 사운드 필요
        E_HitCount++;//적피격
    }

    void PDie()
    {
        if (a < 1)
        {
            iswalk = true;
            Debug.Log("Walk");
            animator.SetTrigger("ispdie");
        }
        a++;
    }

    void P_OnAttack()
    {
        ispdie = true;
    }

    void IsStop()//종료부분 정지
    {
        speed = 0;
        animator.SetTrigger("islough");
        islough = true;
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
        animator.SetTrigger("isattack");
    }
    void G_Fire()//발사
    {
        //이펙트 사운드
        FireSfxR();

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
        G_MF.SendMessage("Play");
        E1GunSfx.clip = FireSfx[Random.Range(0, 4)];
        E1GunSfx.Play();
        G_Bullet--;
    }
    void WhizSfxR()//탄 지나가는 사운드
    {
        WhizSfx.clip = WhizBSfx[Random.Range(0, 6)];
        WhizSfx.Play();

    }

}
