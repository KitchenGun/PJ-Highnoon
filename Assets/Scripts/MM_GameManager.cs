using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_GameManager : MonoBehaviour
{


    public void StartScene()//시작화면씬 시작
    {
        SceneManager.LoadScene(0);
    }

    public void LevelScene()//레벨씬 시작
    {
        SceneManager.LoadScene(1);
    }

    public void EasyScene()//쉬움씬 시작
    {
        SceneManager.LoadScene(2);
    }

    public void NormalScene()//쉬움씬 시작
    {
        SceneManager.LoadScene(3);
    }

    public void HardScene()//어려움씬 시작
    {
        SceneManager.LoadScene(4);
    }

    public void HowToPlayScene()//튜토리얼씬 시작
    {
        SceneManager.LoadScene(5);
    }


    
}
