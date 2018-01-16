using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_GunReady : MonoBehaviour {
    public GameObject Lhand;
    public GameObject Rhand;
    // Use this for initialization
    void Start () {
        Lhand.SendMessage("Gun_isReady");
        Rhand.SendMessage("Gun_isReady");
    }
	
}
