using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E2_Attack : MonoBehaviour
{


    public GameObject P_Go;//플레이어 게임오브젝트
    //적캐릭터 기본 
    public GameObject e2;
    //애니메이터
    private Animator animator;
    //총사운드
    public AudioSource E1GunSfx;
    public AudioSource WhizSfx;
    public AudioClip[] WhizBSfx;
    public AudioClip[] FireSfx;
    //총격 이펙트
    public GameObject G_MF;
    //총알수
    private int G_Bullet = 6;
    //발사위치
    public Transform G_FirePosition;
    //발사가능여부
    //private bool E1_FireReady = true;
    //발사속도
    float E1_BulletRpm = 60.0f;
    //사망함수
    private bool e1die = false;
    //스탑포지션 콜리더
    //private bool isStop = false;


    void Start()
    {
    }


    void Update()
    {
        //레이그리기
        Debug.DrawRay(G_FirePosition.position, G_FirePosition.forward * 100, Color.green);
    }

    void E_OnAttack()
    {
        StopAllCoroutines();
        Debug.Log("hit");
        //피격판정
        e2.SendMessage("E_OnAttack");

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
                e2.SendMessage("P_OnAttack", SendMessageOptions.DontRequireReceiver);
            }
        }
        if (P_Go == null)
        {
            //적캐릭터에게 플레이어 사망 전달
            e2.SendMessage("P_OnAttack", SendMessageOptions.DontRequireReceiver);
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
    void StartAttack()
    {
        StartCoroutine(CoReady());
    }


    IEnumerator CoReady()
    {
        while (!e1die)
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