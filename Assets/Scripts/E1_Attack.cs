using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_Attack : MonoBehaviour {

    public GameObject E1Attack;
    public GameObject e1;


	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void E_OnAttack()
    {
        Debug.Log("hit");
        E1Attack.SetActiveRecursively(false);
        e1.SetActiveRecursively(true);

    }
}
