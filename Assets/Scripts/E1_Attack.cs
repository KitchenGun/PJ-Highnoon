using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_Attack : MonoBehaviour {

    //적캐릭터 공격
    public GameObject E1Attack;
    //적캐릭터 기본 
    public GameObject e1;
    //애니메이터
    private Animator animator;
    //총사운드
    public AudioSource GunSfxR;
    public AudioClip[] FireSfx;
    //총알수
    private int G_Bullet = 6;
    //발사위치
    public Transform G_FirePosition;
    //발사가능여부
    private bool E1_FireReady = true;
    //발사속도
    float E1_BulletRpm = 60.0f;
    //사망함수
    private bool e1die = false;


    void Start ()
    {
        StartCoroutine(CoReady());
    }
	

	void Update ()
    {
        //레이그리기
        Debug.DrawRay(G_FirePosition.position, G_FirePosition.forward * 100, Color.green);
    }

    void E_OnAttack()
    {
        StopAllCoroutines();
        Debug.Log("hit");
        //적공격 오브젝트 비활성
        E1Attack.SetActive(false);
        //적기본 오브젝트 활성
        e1.SetActive(true);
        //피격판정
        e1.SendMessage("E_OnAttack", SendMessageOptions.DontRequireReceiver);

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
                //적 공격 오브젝트 비활성
                E1Attack.SetActive(false);
                //적 기본 오브젝트 활성
                e1.SetActive(true);
                //플레이어에 대미지 입히는 함수
                hit.collider.gameObject.SendMessage("P_Die", SendMessageOptions.DontRequireReceiver);
                //적캐릭터에게 플레이어 사망 전달
                e1.SendMessage("P_OnAttack", SendMessageOptions.DontRequireReceiver);
            }
        }
        this.E1_FireReady = false;
    }
    void FireSfxR()//발사음 랜덤재생
    {
        G_Bullet--;
        GunSfxR.clip = FireSfx[Random.Range(0, 3)];
        GunSfxR.Play();
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
            else if (0 < G_Bullet && G_Bullet <=3)
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
