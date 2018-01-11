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
    private bool IsDie = false;
    //적 캐릭터 생명 변수
    public int E1_hp = 100;
    //공격 사거리
    public float AttackDist = 2.0f;
    //발사위치
    public Transform G_FirePosition;
    //6발
    //private int G_Bullet = 6;
    //총발사 가능 여부
    private bool G_isReady = false;
    //시작 카운트
    public float StartCount = 20.0f;
    RaycastHit hit;
    //적캐릭터 이동속도
    private int Speed = -3;
    //엔딩 사운드
    public AudioClip Ending;
    //AudioSource 컴포넌트를 저장할 변수
    private AudioSource source = null;
    private bool isPDie = false;
    //혈흔 효과 프리팹
    public GameObject bloodEffect;
    //혈흔 데칼 효과 프리팹
    public GameObject bloodDecal;

    private Transform E1_Tr;
    private Transform PlayerTr;





    void Start()
    {
        //Animator 컴포넌트 할당
        animator = this.gameObject.GetComponent<Animator>();
        //일정한 간격으로 몬스터의 행동 상태를 체크하는 코루틴 함수 실행
        StartCoroutine(this.CheckEnemy1());

        //몬스터의 상태에 따라 동작하는 루틴을 실행하는 코루틴 함수 실행
        StartCoroutine(this.E1_Action());
        //AudioSource 컴포넌트를 추출한 후 변수에 할당
        source = GetComponent<AudioSource>();

    }
    private void Update()
    {
        StartCount -= Time.deltaTime;
        if (isPDie == true)
        {
            transform.Translate(new Vector3(0.0f, 0.0f, Speed) * Time.deltaTime);
        }

    }
    IEnumerator CheckEnemy1()
    {
        while (IsDie == false)
        {
            yield return new WaitForSeconds(0.2f);
            if (StartCount <= 0.0f)
            {
                e1_state = E1_State.Attack;
            }
            //lse if(hit.collider.tag== "Player")
            //{
                //e1_state = E1_State.PDie;
            //}
            else
            {
                e1_state = E1_State.Idle;
            }
        }
        while (IsDie == true)
        {
            yield return new WaitForSeconds(0.2f);
            e1_state = E1_State.Die;
        }
    }

    IEnumerator E1_Action()
    {
        while (E1_hp >0)
        {
            switch (e1_state)
            {
                //대기상태
                case E1_State.Idle:
                    break;

                //공격상태
                case E1_State.Attack:
                    E1_Attack();
                    animator.SetTrigger("IsAttack");
                    break;

                //플레이어사망 애니메이션
                case E1_State.PDie:
                    //Player_Die();
                    animator.SetTrigger("IsPDie");
                    break;     
            }
            yield return null;
        }
        while (E1_hp <=0)
        {
            E1_Die();
        }
    }
   
    //적 캐릭터 피격
    void E_OnAttack(object[] _params)
    {
        Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));

        //혈흔효과 함수 호출
        CreateBloodEffect((Vector3) _params[0]);
        
        //맞은 총알의 데미지를 추출해 적 캐릭터 체력 차감
        E1_hp -= (int) _params[1];
        if (E1_hp <=0)
        {
            IsDie = true;
            GetComponent<AudioSource>().Play();
        }

        //IsHit트리거 발생
        animator.SetTrigger("isHit");
    }

    //적 캐릭터 사망
    void E1_Die()
    {
        //모든 코루틴 정지
        StopAllCoroutines();
        //적캐릭터 상태 변환
        e1_state = E1_State.Die;
        //다음 스테이지로 넘김
        SceneManager.LoadScene("normal");
        Debug.Log("히다희");
    }

    //적캐릭터 공격
    void E1_Attack()
    {
        RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
        if (Physics.Raycast(G_FirePosition.position, transform.forward, out hit, 100.0f))//레이 탐색 
        {
            if (hit.collider.tag == "Player")//적 탐지시
            {
                object[] _params = new object[2];//레이피격시 내부 정보추출
                _params[0] = hit.point;
                //플레이어에 대미지 입히는 함수
                hit.collider.gameObject.SendMessage("P_OnAttack", SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ("StopE1"))
        {
            Speed = 0;
            Debug.Log("Stoped");
            Player_Die();
        }
    }

    //술 뿌리기
    void Player_Die()
    {
        Debug.Log("유다희");
        animator.SetTrigger("Spread");
    }

    void CreateBloodEffect(Vector3 pos)
    {
        //혈흔 효과 생성
        GameObject blood1 = (GameObject)Instantiate(bloodEffect, pos, Quaternion.identity);
        Destroy(blood1, 2.0f);

        //데칼 생성 위치 - 바닥에서 조금 올린 위치 산출
        Vector3 decalpos = E1_Tr.position + (Vector3.up * 0.05f);
        //데칼의 회전값을 무작위로 설정
        Quaternion decalrot = Quaternion.Euler(90, 0, Random.Range(0, 360));

        //데칼 프리팹 생성
        GameObject blood2 = (GameObject)Instantiate(bloodDecal, decalpos, decalrot);
        //데칼의 크기도 불규칙 적으로 나타나게끔 스케일 조정
        float scale = Random.Range(1.5f, 3.5f);
        blood2.transform.localScale = Vector3.one * scale;

        //.2초 후에 혈흔효과 제거
        Destroy(blood2, 5.0f);
    }
}
