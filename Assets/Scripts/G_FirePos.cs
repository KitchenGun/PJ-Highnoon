using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FirePos : MonoBehaviour
{

    public GameObject Gun;//총의 랜더러
    
    private void OnCollisionEnter(Collision collision)//충돌처리
    {
        if (collision.gameObject.tag == "PH")
        {
            Destroy(Gun);
        }
    }
}
