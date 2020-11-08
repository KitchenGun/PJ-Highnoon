using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class Map_Ctrl : MonoBehaviour {

    //시작 종소리 사운드
    public AudioClip Startsign;
    //AudioSource 컴포넌트를 저장할 변수
    private AudioSource source = null;

	void Start ()
    {
        //AudioSource 컴포넌트를 추출한 후 변수에 할당
        source = GetComponent<AudioSource>();
	}
    //레벨 시작
    void LevelStart()
    {
        //시작사운드 처리
        if(! source.playOnAwake)
        {
            GetComponent<AudioSource>().Play();

        }
    }
}
