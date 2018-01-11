using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FirePos : MonoBehaviour
{
    float Grap_F;

    public GameObject Gun;//총의 랜더러


    private void Update()
    {
        Grap_F =
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        Debug.Log(Grap_F);
        
    }
    private void OnCollisionEnter(Collision collision)//충돌처리
    {
        if (collision.gameObject.tag == "Player"&&Grap)
        {
            Destroy(Gun);
        }
    }
}
