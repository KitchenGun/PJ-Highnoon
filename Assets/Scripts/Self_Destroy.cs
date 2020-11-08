using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Self_Destroy : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Destroy(this.gameObject,4.0f);	
	}
	
}
