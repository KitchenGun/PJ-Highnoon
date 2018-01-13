using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player_Ctrl : MonoBehaviour
{
    public static Player_Ctrl Playsc;//플레이어 스크립트

    
    public GameObject RHandG;
    public GameObject LHandG;


    private Animator animator;
    public bool IsPDie = false;


    //엔딩 사운드
    public AudioClip Ending;
    //AudioSource 컴포넌트를 저장할 변수
    private AudioSource source = null;



    private void Start()
    {
        //AudioSource 컴포넌트를 추출한 후 변수에 할당
        source = GetComponent<AudioSource>();

        RHandG.SendMessage("G_allReload");
        LHandG.SendMessage("G_allReload");
    }

    private void Awake()
    {
        //if (Playsc == null)//싱글톤 
        //{
        //    Playsc = gameObject.GetComponent<Player_Ctrl>();
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //    RHandG.SendMessage("G_allReload");
        //    LHandG.SendMessage("G_allReload");
        //}
        
    }

    void Set()
    {
        Invoke("SetDelay",2.0f);
    }
    void SetDelay()
    {
        this.transform.position = new Vector3(-5, -14, 1.77f);
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

    void P_Die()
    {
        GetComponent<AudioSource>().Play();
    }
}
