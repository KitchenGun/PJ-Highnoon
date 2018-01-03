using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player_Ctrl : MonoBehaviour
{

    public Transform P_Head;
    public Transform P_Body;
    public Transform P_HandR;
    public Transform P_HandL;

    private Vector3 P_originPos;

    public int P_HP = 100;
    private Animator animator;

    private void Awake()
    {
        Init();
        P_originPos = this.transform.position;
    }

    void Init()//vr초기화
    {
        //VR 해상도
        XRSettings.eyeTextureResolutionScale = 1.0f;

        //VR 시스템을 활성화할거임?
        XRSettings.enabled = true;

        //포지션 트랙킹 사용안할거임?
        InputTracking.disablePositionalTracking = false;

        //카메라 오토트랙킹을 사용안할거임?
        XRDevice.DisableAutoXRCameraTracking(Camera.main, true);
    }

    private void Update()
    {
        //현재 VR헤드셋의 위치, 방향을 원점, 정면으로
        if (Input.GetButtonDown("Fire1"))
        {
            InputTracking.Recenter();
        }

        //VR 사용유무
        if (Input.GetKeyDown(KeyCode.Space))
        {
            XRSettings.enabled = !XRSettings.enabled;
        }
    }

   

    //수동 트랙킹
    private void ManualTracking()
    {
        Vector3 headPosition = InputTracking.GetLocalPosition(XRNode.Head);
        Vector3 headEularAngle = InputTracking.GetLocalRotation(XRNode.Head).eulerAngles;
        Vector3 leftHandControllerPos =  InputTracking.GetLocalPosition (XRNode.LeftHand);
        Vector3 rightHandControllerPos = InputTracking.GetLocalPosition(XRNode.RightHand);
        transform.position = P_originPos + (headPosition * 3);
        //		transform.eulerAngles = new Vector3 (0, headEularAngle.y, 0);
    }

    //플레이어 피격
    void P_OnAttack(object[] _params)
    {
        Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));

        //맞은 총알의 데미지를 추출해 플레이어 체력 차감
        P_HP -= (int)_params[1];
        if (P_HP <= 0)
        {
            P_Die();
        }

        //IsHit트리거 발생
        //animator.SetTrigger("isHit");
    }

    void P_Die()
    {
            StopAllCoroutines();
        //플레이어 사망시 적 캐릭터 애니메이션 실행
        GameObject.Find("E1").GetComponent<E1_Ctrl>().Player_Die();

    }
}
