using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player_Ctrl : MonoBehaviour
{
    public static Player_Ctrl Playsc;//플레이어 스크립트

    //public Transform P_Head;
    //public Transform P_Body;
    //public Transform P_HandR;
    //public Transform P_HandL;
    //
    //private Vector3 P_originPos;
    //
    public GameObject RHandG;
    public GameObject LHandG;


    private Animator animator;
    public bool IsPDie = false;

    //발사위치
    public Transform G_FirePosition;
    //시작 종소리 사운드
    public AudioClip AplusSound;
    //AudioSource 컴포넌트를 저장할 변수
    private AudioSource source = null;



    private void Start()
    {
        //AudioSource 컴포넌트를 추출한 후 변수에 할당
        source = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        if (Playsc == null)//싱글톤 
        {
            Playsc = gameObject.GetComponent<Player_Ctrl>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            RHandG.SendMessage("G_allReload");
        }
        //Init();
        //P_originPos = this.transform.position;
    }

    //void Init()//vr초기화
    //{
    //    //VR 해상도
    //    XRSettings.eyeTextureResolutionScale = 1.0f;
    //
    //    //VR 시스템을 활성화할거임?
    //    XRSettings.enabled = true;
    //
    //    //포지션 트랙킹 사용안할거임?
    //    InputTracking.disablePositionalTracking = true;
    //
    //    //카메라 오토트랙킹을 사용안할거임?
    //    XRDevice.DisableAutoXRCameraTracking(Camera.main, false);
    //}
    //
    //private void Update()
    //{
    //    //현재 VR헤드셋의 위치, 방향을 원점, 정면으로
    //    if (Input.GetButtonDown("Fire1"))
    //    {
    //        InputTracking.Recenter();
    //    }
    //
    //    //VR 사용유무
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        XRSettings.enabled = !XRSettings.enabled;
    //    }
    //    ManualTracking();
    //}
    //
    //
    //
    ////수동 트랙킹
    //private void ManualTracking()
    //{
    //    Vector3 headPosition = InputTracking.GetLocalPosition(XRNode.Head);
    //    Vector3 headEularAngle = InputTracking.GetLocalRotation(XRNode.Head).eulerAngles;
    //    transform.position = P_originPos + (headPosition);
    //    //		transform.eulerAngles = new Vector3 (0, headEularAngle.y, 0);
    //}

    //플레이어 피격
    void P_OnAttack(object[] _params)
    {
        Debug.Log(string.Format("Hit ray {0} : {1}", _params[0], _params[1]));

        //총에맞을시 플레이어 사망
        P_Die();
        GameObject.Find("E1").SendMessage("Player_Die");

        //IsHit트리거 발생
        //animator.SetTrigger("isHit");
    }

    void P_Die()
    {
        animator.SetBool("IsPDie", true);
    }

    void A_Plus()
    {
        RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
        if (Physics.Raycast(G_FirePosition.position, transform.forward, out hit, 100.0f))//레이 탐색 
        {
            if (hit.collider.tag == "APlus")//적 탐지시
            {
                object[] _params = new object[2];//레이피격시 내부 정보추출
                _params[0] = hit.point;
                //에이플러스 사운드 처리
                GetComponent<AudioSource>().Play();
            }
        }
    }
}
