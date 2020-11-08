using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LogoLight : MonoBehaviour
{
    public Light LightLogo;//로고를 비추는 스팟 라이트
    public GameObject a;
    public GameObject b;
    public GameObject c;
    private int g;
    public AudioSource G_Sfx;
    public AudioClip ASfx;
    public AudioClip BSfx;
    public AudioClip CSfx;
    public AudioClip DSfx;

    // Use this for initialization
    void Start()
    {
        a.SetActive(false);
        b.SetActive(false);
        c.SetActive(false);
        LightLogo.spotAngle = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
        LightLogo.spotAngle += Time.deltaTime * 30;
        if(LightLogo.spotAngle>160)
        {
            if (g == 0)
            {
                Invoke("A1", 0.7f);
                Invoke("B1", 1.4f);
                Invoke("C1", 2.1f);
                Invoke("D1", 2.8f);

                Invoke("Scene", 4f);
                g++;
            }
        }
    }


    void A1()
    {
        Debug.Log("ddd");
        a.SetActive(true);
        G_Sfx.PlayOneShot(ASfx);
    }
    void B1()
    {
        b.SetActive(true);
        G_Sfx.PlayOneShot(BSfx);
    }
    void C1()
    {
        c.SetActive(true);
        G_Sfx.PlayOneShot(CSfx);
    }
    void D1()
    {
        G_Sfx.PlayOneShot(DSfx);
    }
    void Scene()
    {
        SceneManager.LoadScene(1);
    }

}
