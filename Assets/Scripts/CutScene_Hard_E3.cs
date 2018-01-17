using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene_Hard_E3 : MonoBehaviour {

    public GameObject Player;

    public AudioSource E3Sfx;

    public Animator E3Ani;

    public AudioClip E1;
    public AudioClip E2;
    public AudioClip E3;
    public AudioClip E4;
    public AudioClip E5;
    public AudioClip E6;

    private void Start()
    {
        E3set1();
    }

    void E3set1()
    {
        E3Ani.SetTrigger("e3ani1");
        E3Sfx.PlayOneShot(E1);
        if (!E3Sfx.isPlaying)
        {
            Player.SendMessage("Pset1");
        }
    }

    void E3set2()
    {
        E3Ani.SetTrigger("e3ani2");
        E3Sfx.PlayOneShot(E2);
        if (!E3Sfx.isPlaying)
        {
            Player.SendMessage("Pset2");
        }
    }
    void E3set3()
    {
        E3Ani.SetTrigger("e3ani3");
        E3Sfx.PlayOneShot(E3);
        if (!E3Sfx.isPlaying)
        {
            E3set4();
        }
    }
    void E3set4()
    {
        E3Ani.SetTrigger("e3ani4");
        E3Sfx.PlayOneShot(E4);
        if (!E3Sfx.isPlaying)
        {
            E3set5();
        }
    }
    void E3set5()
    {
        E3Ani.SetTrigger("e3ani5");
        E3Sfx.PlayOneShot(E5);
        if (!E3Sfx.isPlaying)
        {
            Player.SendMessage("Pset3");
        }
    }

    void E3set6()
    {
        E3Ani.SetTrigger("e3ani6");
        E3Sfx.PlayOneShot(E6);
        if (!E3Sfx.isPlaying)
        {
            SceneManager.LoadScene(3);
        }
    }
}
