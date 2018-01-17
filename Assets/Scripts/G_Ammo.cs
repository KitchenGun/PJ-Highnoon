using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_Ammo : MonoBehaviour {

    public GameObject L;
    public GameObject R;
    //오른손 왼손

	// Use this for initialization
	void Start ()
    {
        L.SendMessage("G_isInf");
        R.SendMessage("G_isInf");

	}
	
}
