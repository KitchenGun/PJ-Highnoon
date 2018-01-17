using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene_Easy_E1 : MonoBehaviour
{
    public GameObject Clerk;//점원
    public GameObject Player;//플레이어 
    //대사
    public AudioSource E1Sfx;

    public AudioClip E1;
    public AudioClip E2;
    public AudioClip E3;
    public AudioClip E4;
    public AudioClip E5;

    public Animator E1Ani;


    private void Start()
    {
        E1set1();   
    }

    void E1set1()//술마시는 애니메이션 만
    {
        E1Ani.SetTrigger("e1ani1");
        Invoke("E1set2", 1.0f);
    }
    void E1set2()//점원호출
    {
        E1Ani.SetTrigger("e1ani2");
        E1Sfx.PlayOneShot(E1);
        if(!E1Sfx.isPlaying)
        {
            Clerk.SendMessage("Cset1");
        }
    }
    void E1Set3()//
    {
        E1Ani.SetTrigger("e1ani3");
        E1Sfx.PlayOneShot(E2);
        if (!E1Sfx.isPlaying)
        {
            Clerk.SendMessage("Cset2");
        }
    }

    void E1set4()
    {
        E1Ani.SetTrigger("e1ani4");
        E1Sfx.PlayOneShot(E3);
        if (!E1Sfx.isPlaying)
        {
            E1set5();
        }
    }
    void E1set5()
    {
        E1Ani.SetTrigger("e1ani5");
        E1Sfx.PlayOneShot(E4);
        if (!E1Sfx.isPlaying)
        {

            SceneManager.LoadScene(3);//씬이동
        }
    }

}
