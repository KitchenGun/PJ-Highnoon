using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_GameManager : MonoBehaviour
{
    public MeshRenderer a; //버튼이 되는병
    public Collider b;//콜라이더
    public GameObject B_Piece;//병 조각
    public AudioClip[] BottleBrokenSfx;//병깨지는 소리
    public AudioSource BottleSfx;

    private void Start()
    {
        a.enabled = true;
       
    }
    void BottleSfxP()//발사음 랜덤재생
    {
        BottleSfx.clip = BottleBrokenSfx[Random.Range(0, 5)];
        //OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(GunSfxR.clip));이것 좃같은 물건이다.
        BottleSfx.Play();
    }
    public void StartScene()
    { 
        a.enabled = false;
        Destroy(b);
        BottleSfxP();
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("StartScene1", 2.0f);
    }

    public void LevelScene()
    {
        a.enabled = false;
        Destroy(b);
        BottleSfxP();
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("LevelScene1", 2.0f);
    }

    public void EasyScene()
    {
        a.enabled = false;
        Destroy(b);
        BottleSfxP();
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("EasyScene1", 2.0f);
    }

    public void NormalScene()
    {
        a.enabled = false;
        Destroy(b);
        BottleSfxP();
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("CutScennormal", 2.0f);
    }

    public void HardScene()
    {
        a.enabled = false;
        Destroy(b);
        BottleSfxP();
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("CutSceneHard", 2.0f);
    }

    public void HowToPlayScene()
    {
        a.enabled = false;
        Destroy(b);
        BottleSfxP();
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("HowToPlayScene1", 2.0f);
    }
    
    public void ExitScene()
    {
        a.enabled = false;
        Destroy(b);
        BottleSfxP();
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("ExitScene1", 2.0f);
    }
    public void StartScene1()//시작화면씬 시작
    {
        SceneManager.LoadScene(1);
    }

    public void LevelScene1()//레벨씬 시작
    {
        SceneManager.LoadScene(2);
    }

    public void EasyScene1()//쉬움씬 시작
    {
        SceneManager.LoadScene(3);
    }

    public void NormalScene1()//보통씬 시작
    {
        SceneManager.LoadScene(4);
    }

    public void HardScene1()//어려움씬 시작
    {
        SceneManager.LoadScene(5);
    }

    public void HowToPlayScene1()//튜토리얼씬 시작
    {
        SceneManager.LoadScene(6);
    }

    public void CutScennormal()//노말컷신
    {
        SceneManager.LoadScene(7);
    }

    public void CutSceneHard()//어려움컷신
    {
        SceneManager.LoadScene(8);
    }

    public void ExitScene1()//프로그램 종료
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    
}
