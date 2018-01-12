using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E1_Attack : MonoBehaviour {

    public GameObject E1Attack;


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
        E1Attack.SetActiveRecursively(false);
    }
}
