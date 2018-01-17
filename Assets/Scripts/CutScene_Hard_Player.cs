using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (!PlayerSfx.isPlaying)
        {
            E3.SendMessage("E3set2");
        }
    }
    void Pset2()
    {

        PlayerSfx.PlayOneShot(P32);
        if (!PlayerSfx.isPlaying)
         {
             E3.SendMessage("E3set3");
         }
        
    }
    void Pset3()
    {
        PlayerSfx.PlayOneShot(P33);
        if (!PlayerSfx.isPlaying)
        {
            E3.SendMessage("E3set6");
        }
        
    }       
}
