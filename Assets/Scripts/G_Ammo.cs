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
        Time.timeScale = 1f;//시간 느려지는거 복구
        L.SendMessage("G_isInf");
        R.SendMessage("G_isInf");

	}
	
}
