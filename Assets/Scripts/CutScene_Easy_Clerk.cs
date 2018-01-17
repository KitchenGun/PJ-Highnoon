using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene_Easy_Clerk : MonoBehaviour {
    
    public GameObject E1;//적캐릭터
    public GameObject Player;//플레이어 
    //대사
    public AudioSource ClerkSfx;

    public AudioClip C1;
    public AudioClip C2;
    public AudioClip C3;
    public AudioClip C4;

    public Animator CAni;
    

    void Cset1()
    {
        Debug.Log("dd");
        CAni.SetTrigger("Cani1");
        ClerkSfx.PlayOneShot(C1);
        if(!ClerkSfx.isPlaying)
        {
            E1.SendMessage("E1set3");
        }
    }

    void Cset2()
    {
        CAni.SetTrigger("Cani2");
        ClerkSfx.PlayOneShot(C2);
        if (!ClerkSfx.isPlaying)
        {
            Cset3();
        }
    }

    void Cset3()
    {
        CAni.SetTrigger("Cani3");
        ClerkSfx.PlayOneShot(C3);
        if (!ClerkSfx.isPlaying)
        {
            Cset4();
        }
    }

    void Cset4()
    {
        CAni.SetTrigger("Cani4");
        ClerkSfx.PlayOneShot(C4);
        if (!ClerkSfx.isPlaying)
        {
            Cset5();
        }
    }

    void Cset5()
    {
        CAni.SetTrigger("Cani4");
        ClerkSfx.PlayOneShot(C4);
        if (!ClerkSfx.isPlaying)
        {
            Player.SendMessage("Pset1");
        }

    }




}
