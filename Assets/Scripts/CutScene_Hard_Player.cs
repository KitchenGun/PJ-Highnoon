using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene_Hard_Player : MonoBehaviour
{
    public GameObject E3;
    public AudioSource PlayerSfx;

    public AudioClip P31;
    public AudioClip P32;
    public AudioClip P33;



    void Pset1()
    {
        PlayerSfx.PlayOneShot(P31);
        Invoke("SendESet1", 3.0f);
    }
    void Pset2()
    {

        PlayerSfx.PlayOneShot(P32);
        Invoke("SendESet2", 3.0f);
        
    }
    void Pset3()
    {
        PlayerSfx.PlayOneShot(P33);
        Invoke("SendESet3", 3.0f);
        
    }
    
    void SendESet1()
    {
        E3.SendMessage("E3set2");
    }

    void SendESet2()
    {
        E3.SendMessage("E3set3");
    }

    void SendESet3()
    {
        SceneManager.LoadScene(5);
    }
}
