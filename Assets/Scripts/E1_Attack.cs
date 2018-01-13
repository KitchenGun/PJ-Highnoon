using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_Attack : MonoBehaviour {

    public GameObject E1Attack;
    public GameObject e1;
    private Animator animator;
    public AudioSource GunSfxR;
    public AudioClip[] FireSfx;
    private int G_BulletR = 6;//6발
    public Transform G_FirePosition;//발사위치

    // Use this for initialization
    void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        Debug.DrawRay(G_FirePosition.position, G_FirePosition.forward * 100, Color.green);
    }

    void E_OnAttack()
    {
        Debug.Log("hit");
        E1Attack.SetActiveRecursively(false);
        e1.SetActiveRecursively(true);
        e1.SendMessage("E_OnAttack", SendMessageOptions.DontRequireReceiver);

    }
    void G_FireR()//발사
    {
        //이펙트 사운드
        FireSfxR();

        RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
        if (Physics.Raycast(G_FirePosition.position, G_FirePosition.forward, out hit, 100.0f))//레이 탐색 
        {
            Debug.Log("PlayerDie");
            if (hit.collider.tag == "Player")//플레이어 탐지시
            {
                
                E1Attack.SetActiveRecursively(false);
                e1.SetActiveRecursively(true);
                //플레이어에 대미지 입히는 함수
                hit.collider.gameObject.SendMessage("P_Die", SendMessageOptions.DontRequireReceiver);
                //적캐릭터에게 플레이어 사망 전달
                e1.SendMessage("P_OnAttack", SendMessageOptions.DontRequireReceiver);
            }
        }
        G_BulletR--;
    }
    void FireSfxR()//발사음 랜덤재생
    {
        GunSfxR.clip = FireSfx[Random.Range(0, 2)];
        GunSfxR.Play();
    }
}
