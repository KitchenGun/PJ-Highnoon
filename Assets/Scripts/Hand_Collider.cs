using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Collider : MonoBehaviour
{
    float Grap_F;
    public GameObject GunL;
    private bool GunReady = false;//총꺼내기 가능

    private void Update()
    {
        Grap_F =
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger);
        //Debug.Log(Grap_F);
    }

    public void Gun_isReady()//총을 잡을수있습니다.
    {
        GunReady = true;
    }

    private void OnCollisionStay(Collision c)
    {
        //Debug.Log(c.transform.name);
        if (c.gameObject.tag == "Player"&&Grap_F>=0.8)
        {
            Debug.Log("ddd");
            GunL.SendMessage("H_changeL");
           // Destroy(this.gameObject);
        }
    }
}

