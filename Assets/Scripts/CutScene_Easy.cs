using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene_Easy : MonoBehaviour
{
    private Animator animator;
    //대사
    public AudioSource MentS;
    public AudioClip Ment1;
    public AudioClip Ment2;
    public AudioClip Ment3;
    private void Start()
    {
        StartM1();
    }
    void StartM1()
    {
        animator.SetTrigger("start2");
        StartM2();
    }

    void StartM2()
    {
        
        animator.SetTrigger("Start3");
    }




}
