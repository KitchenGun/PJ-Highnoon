using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_BodyCtrl : MonoBehaviour
{
    public Transform Head;
    public Transform Body;
    private void Update()
    {
        Body.transform.position = Head.transform.position+new Vector3(0,-1f,0);
        //바라보는 방향을 기준으로 이동하기 위한 몸통 회전
        Vector3 headEuler = Head.localEulerAngles;
        Body.localEulerAngles = new Vector3(0, headEuler.y, 0);
    }
}
