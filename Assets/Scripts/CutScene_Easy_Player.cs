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

    public AudioClip P11;
    public AudioClip P12;
    public AudioClip P13;

    void Pset1()
    {
        PlayerSfx.PlayOneShot(P11);
        if(!PlayerSfx.isPlaying)
        {
            Pset2();
        }
    }

    void Pset2()
    {
        PlayerSfx.PlayOneShot(P12);
        if (!PlayerSfx.isPlaying)
        {
            Pset3();
        }
    }
    void Pset3()
    {
        PlayerSfx.PlayOneShot(P13);
        if (!PlayerSfx.isPlaying)
        {
            E1.SendMessage("E1set4");
        }
    }


}
