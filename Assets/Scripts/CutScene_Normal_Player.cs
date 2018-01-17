using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene_Normal_Player : MonoBehaviour {

    public GameObject E2;//적

    public AudioSource PlayerSfx;
    public AudioClip P1;
    public AudioClip P2;
    public AudioClip P3;
   
    
    void Pset1()
    {
        PlayerSfx.PlayOneShot(P1);
        if(!PlayerSfx.isPlaying)
        {
            E2.SendMessage("E2set2");
        }
    }

    void Pset2()
    {
        PlayerSfx.PlayOneShot(P2);
        {
            if(!PlayerSfx.isPlaying)
            {
                E2.SendMessage("E2set3");
            }
        }
    }

    void Pset3()
    {
        PlayerSfx.PlayOneShot(P3);
        {
            if (!PlayerSfx.isPlaying)
            {
                E2.SendMessage("E2set4");
            }
        }
    }

}
