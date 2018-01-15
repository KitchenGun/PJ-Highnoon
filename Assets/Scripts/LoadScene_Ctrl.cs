using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene_Ctrl : MonoBehaviour
{
    private float LoadTf=0f;//로딩시간
    private bool LoadDone=false;//로드상태false는 실패
    AsyncOperation async_operation;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(StartLoad(2));//씬로드
    }

	
	// Update is called once per frame
	void Update () {
		
	}
    public IEnumerator StartLoad(int SceneNum)//로드씬대기화면 
    {
        async_operation = SceneManager.LoadSceneAsync(SceneNum);
        async_operation.allowSceneActivation = false;

        if (LoadDone == false)
        {
            LoadDone = true;

            while (async_operation.progress < 0.9f)
            {
                yield return true;
            }
        }
    }

}
