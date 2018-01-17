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
        E2Sfx.PlayOneShot(E1);
        Invoke("SendPset1", 3.0f);
    }

    void E2set2()
    {
        E2Ani.SetTrigger("e2ani1");
        E2Sfx.PlayOneShot(E2);
        Invoke("SendPset2", 6.0f);
    }

    void E2set3()
    {
        E2Ani.SetTrigger("e2ani2");
        E2Sfx.PlayOneShot(E3);
        Invoke("SendPset3", 3.0f);

    }
    void E2set4()
    {
        E2Ani.SetTrigger("e2ani3");
        E2Sfx.PlayOneShot(E4);
        Invoke("nextScene", 2.0f);
    }

    void SendPset1()
    {
      Debug.Log("ddd");
      Player.SendMessage("Pset1");
    }

    void SendPset2()
    {
        Debug.Log("aaa");
      Player.SendMessage("Pset2");
    }

    void SendPset3()
    {
        Player.SendMessage("Pset3");
    }
    void NextScene()
    {
        SceneManager.LoadScene(3);
    }

}
