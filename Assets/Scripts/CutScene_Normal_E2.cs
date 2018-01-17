using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene_Normal_E2 : MonoBehaviour {


    public GameObject Player;

    public AudioSource E2Sfx;
    public AudioClip E1;
    public AudioClip E2;
    public AudioClip E3;
    public AudioClip E4;

    public Animator E2Ani;


    private void Start()
    {
        E2set1();
    }

    void E2set1()
    {
        E2Ani.SetTrigger("e2ani1");
        E2Sfx.PlayOneShot(E1);
        if (!E2Sfx.isPlaying)
        {
            Player.SendMessage("Pset1");
        }
    }

    void E2set2()
    {
        E2Ani.SetTrigger("e2ani2");
        E2Sfx.PlayOneShot(E2);
        if (!E2Sfx.isPlaying)
        {
            Player.SendMessage("Pset2");
        }
    }

    void E2set3()
    {
        E2Ani.SetTrigger("e2ani3");
        E2Sfx.PlayOneShot(E3);
        if (!E2Sfx.isPlaying)
        {
            Player.SendMessage("Pset3");
        }

    }
    void E2set4()
    {
        E2Ani.SetTrigger("e2ani3");
        E2Sfx.PlayOneShot(E3);
        if (!E2Sfx.isPlaying)
        {
            SceneManager.LoadScene(3);
        }
    }

}
