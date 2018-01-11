using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Collider1 : MonoBehaviour {

    float Grap_F;//그랩 입력값
    public GameObject GunR;//오른쪽 홀스터 총
    private void Update()
    {
        Grap_F =
            OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger, OVRInput.Controller.LTouch);
        Debug.Log(Grap_F);
    }
    private void OnCollisionStay(Collision c)//충돌판정
    {
        Debug.Log(c.transform.name);
        if (c.gameObject.tag == "Player" && Grap_F >= 0.8)//손이 플레이어충돌하고 그랩인풋값들어왔을때
        {
            Debug.Log("ddd");
            GunR.SendMessage("H_changeR");
            Destroy(this.gameObject);
        }
    }
}
