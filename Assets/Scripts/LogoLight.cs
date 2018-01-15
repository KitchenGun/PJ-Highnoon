using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LogoLight : MonoBehaviour
{
    public Light LightLogo;//로고를 비추는 스팟 라이트

    // Use this for initialization
    void Start()
    {
        LightLogo.spotAngle = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
            
        LightLogo.spotAngle += Time.deltaTime * 30;
        if(LightLogo.spotAngle>160)
        {
            Debug.Log("dd");
            SceneManager.LoadScene(0);
        }
    }
}
