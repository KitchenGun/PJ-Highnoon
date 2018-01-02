using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FirePos : MonoBehaviour
{

    public MeshRenderer Gun;//총의 랜더러
    public MeshRenderer Gun1;//총 노리쇠의 랜더러
    private void OnCollisionEnter(Collision collision)//충돌처리
    {
        if (collision.gameObject.tag == "P_hand")
        {
            Gun.enabled=false;
            Gun1.enabled = false;
        }
    }
}
