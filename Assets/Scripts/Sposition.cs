using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sposition : MonoBehaviour {

    public GameObject e1;
    private void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag =="Enemy")
        {
           e1.SendMessage("isStop", SendMessageOptions.DontRequireReceiver);
        }
    }
}
