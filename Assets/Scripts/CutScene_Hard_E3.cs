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
    //public AudioClip E6;

    private void Start()
    {
        E3set1();
    }

    void E3set1()
    {
        //E3Ani.SetTrigger("e3ani1");
        E3Sfx.PlayOneShot(E1);
        Invoke("StartPSet1", 3.0f);
    }

    void E3set2()
    {
        E3Ani.SetTrigger("e3ani1");
        E3Sfx.PlayOneShot(E2);
        Invoke("StartPSet2",3.0f);
    }
    void E3set3()
    {
        E3Ani.SetTrigger("e3ani2");
        E3Sfx.PlayOneShot(E3);
        Invoke("E3set4", 3.0f);
    }
    void E3set4()
    {
        E3Ani.SetTrigger("e3ani3");
        E3Sfx.PlayOneShot(E4);
        Invoke("E3set5", 2.0f);
    }
    void E3set5()
    {
        E3Ani.SetTrigger("e3ani4");
        E3Sfx.PlayOneShot(E5);
        Invoke("StartPSet3", 5.0f);
    }

    //void E3set6()
    //{
       // E3Ani.SetTrigger("e3ani6");
        //E3Sfx.PlayOneShot(E6);
        //if (!E3Sfx.isPlaying)
        //{
          //  SceneManager.LoadScene(3);
        //}
    //}

    void StartPSet1()
    {
        Player.SendMessage("Pset1");
    }
    void StartPSet2()
    {
        Player.SendMessage("Pset2");
    }
    void StartPSet3()
    {
        Player.SendMessage("Pset3");
    }
}
