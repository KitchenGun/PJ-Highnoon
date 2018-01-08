using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_GameManager : MonoBehaviour
{
    public MeshRenderer a; //버튼이 되는병
    public GameObject B_Piece;//병 조각


    private void Start()
    {
        a.enabled = true;
    }

    public void StartScene()
    { 
        a.enabled = false;
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("StartScene1", 2.0f);
    }

    public void LevelScene()
    {
        a.enabled = false;
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("LevelScene1", 2.0f);
    }

    public void EasyScene()
    {
        a.enabled = false;
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("EasyScene1", 2.0f);
    }

    public void NormalScene()
    {
        a.enabled = false;
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("NormalScene1", 2.0f);
    }

    public void HardScene()
    {
        a.enabled = false;
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("HardScene1", 2.0f);
    }

    public void HowToPlayScene()
    {
        a.enabled = false;
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("HowToPlayScene1", 2.0f);
    }
    
    public void ExitScene()
    {
        a.enabled = false;
        Instantiate(B_Piece, this.transform.position, this.transform.rotation);
        Invoke("ExitScene1", 2.0f);
    }
    public void StartScene1()//시작화면씬 시작
    {
        SceneManager.LoadScene(0);
    }

    public void LevelScene1()//레벨씬 시작
    {
        SceneManager.LoadScene(1);
    }

    public void EasyScene1()//쉬움씬 시작
    {
        SceneManager.LoadScene(2);
    }

    public void NormalScene1()//쉬움씬 시작
    {
        SceneManager.LoadScene(3);
    }

    public void HardScene1()//어려움씬 시작
    {
        SceneManager.LoadScene(4);
    }

    public void HowToPlayScene1()//튜토리얼씬 시작
    {
        SceneManager.LoadScene(5);
    }

    public void ExitScene1()//프로그램 종료
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
