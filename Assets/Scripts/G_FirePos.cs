using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_FirePos : MonoBehaviour
{

    public Transform G_FirePosition;//발사위치
    

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
		if(Input.GetKeyDown(KeyCode.Mouse0))//마우스버튼 클릭시
        {
            Physics.Raycast(G_FirePosition.position, transform.forward, out hit, 100.0f);
        }
	}
}
