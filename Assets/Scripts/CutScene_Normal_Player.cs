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
        Invoke("SendE2set2", 3.0f);
    }

    void Pset2()
    {
        PlayerSfx.PlayOneShot(P2);
        Invoke("SendE2set3", 2.0f);
    }

    void Pset3()
    {
        PlayerSfx.PlayOneShot(P3);
        Invoke("SendE2set4", 2.0f);
    }

    void SendE2set2()
    {
      E2.SendMessage("E2set2");
    }
    void SendE2set3()
    {
        E2.SendMessage("E2set3");
    }
    void SendE2set4()
    {
        E2.SendMessage("E2set4");
    }

}
