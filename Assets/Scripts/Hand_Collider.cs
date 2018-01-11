using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Collider : MonoBehaviour
{
    private void OnCollisionEnter(Collision c)
    {
        if(c.gameObject.tag=="PH")
        {
            c.gameObject.SendMessage("H_ChangeL");
        }
    }
}
