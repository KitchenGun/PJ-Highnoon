using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene_Easy_Player : MonoBehaviour
{

    private Animator animator;
    public GameObject E1;//적캐릭터
    public GameObject Clerk;//플레이어 
    //대사
    public AudioSource PlayerSfx;

    public AudioClip P1;
    public AudioClip P2;
    public AudioClip P3;

    void Pset1()
    {
        PlayerSfx.PlayOneShot(p1);
        if(!PlayerSfx.isPlaying)
        {
            Pset2();
        }
    }

    void Pset2()
    {
        PlayerSfx.PlayOneShot(p2);
        if (!PlayerSfx.isPlaying)
        {
            Pset3();
        }
    }
    void Pset3()
    {
        PlayerSfx.PlayOneShot(p3);
        if (!PlayerSfx.isPlaying)
        {
            E1.SendMessage("E1set4");
        }
    }


}
