using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand_CtrlR : MonoBehaviour {

		//public OVRInput.Controller Controller;

		public Transform G_FirePosition;//발사위치
		public GameObject HandnGun;//총없는손
        public GameObject G_gunhand;//총있는손
		private bool G_isGrap=false;//총 잡았는가?
		private bool G_isReady = false;//총을 쏠수 있는가?
        public GameObject G_MF;//머즐플래쉬

        public Animator HandRAni;//애니메이터

        public AudioSource GunSfxR;
        public AudioClip Reload;//사운드
        public AudioClip[] FireSfx;
        public AudioClip FireFSfx;

		private int G_Bullet = 6;//6발


		private void Start()
		{
			G_isGrap = false;
			G_isReady = true;
            G_gunhand.SetActive(false);
            HandnGun.SetActive(true);
			

		}

		void Update()
		{

        float Firetrigger_resultf = 
            OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);//방아쇠컨트롤러버튼

        float G_Reloadf = 
            OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y;//스틱컨트롤러y축 버튼
        //Debug.Log(Firetrigger_resultf);
        //Debug.Log(G_Reloadf);
        Debug.DrawRay(G_FirePosition.position, G_FirePosition.forward * 100, Color.green);
        if (Input.GetKeyDown(KeyCode.A))
			{
				H_change();
			}

			if (G_isGrap == true)
			{
				if (G_isReady == true)
				{
                    
                    HandRAni.SetBool("GisReady", true);
                    if (Input.GetKeyDown(KeyCode.Mouse0) || Firetrigger_resultf >= 0.9f)//마우스버튼 클릭시 발포성공
					{
                        if(G_Reloadf>-0.3f)
                        {
                            G_Fire();
                            Debug.Log("fire");
                        }
					}
				}
				else
				{
                    HandRAni.SetBool("GisReady", false);
				    if (Input.GetKeyDown(KeyCode.Mouse0) || Firetrigger_resultf >=0.9f)//마우스버튼 클릭시 발포 실패
					{
                        if (G_Reloadf > -0.3f&&!GunSfxR.isPlaying)
                        {
                            G_FireF();
                            Debug.Log("firef");
                        }
                }
					if (Input.GetKeyDown(KeyCode.R)|| G_Reloadf <-0.8f)//재장전
					{
						G_Reload();
					}
				}
			}

		}
		void G_Fire()//발사
		{
            //이펙트 사운드
            FireSfxR();
            G_MF.SendMessage("Play");
            HandRAni.SetTrigger("Fire");//총사격 애니메이션
            
			RaycastHit hit;//레이케스트라인 안에 들어온 물체 변수
		    if (Physics.Raycast(G_FirePosition.position, G_FirePosition.forward, out hit, 100.0f))//레이 탐색 
			{
				if (hit.collider.tag == "Enemy")//적 탐지시
				{
					object[] _params = new object[2];//레이피격시 내부 정보추출
					_params[0] = hit.point;
					_params[1] = 50;
					//몬스터에 대미지 입히는 함수
					hit.collider.gameObject.SendMessage("E_OnAttack", SendMessageOptions.DontRequireReceiver);
				}

				if (hit.collider.name == "Player")//적 탐지시
				{
					object[] _params = new object[2];//레이피격시 내부 정보추출
					_params[0] = hit.point;
					_params[1] = 100;
					//몬스터에 대미지 입히는 함수
					hit.collider.gameObject.SendMessage("OnAttack", SendMessageOptions.DontRequireReceiver);
				}

				if (hit.collider.tag == "SB")//시작 버튼 탐지시
				{
					Debug.Log("hit");
					//씬호출
					hit.collider.gameObject.SendMessage("LevelScene");
				}
				if (hit.collider.tag == "HTPB")//튜토리얼버튼 탐지시
				{
					//씬호출
					hit.collider.gameObject.SendMessage("HowToPlayScene", SendMessageOptions.DontRequireReceiver);
				}

				if (hit.collider.tag == "EB")//나가기 버튼 탐지시
				{
					//씬호출
					hit.collider.gameObject.SendMessage(" ExitScene", SendMessageOptions.DontRequireReceiver);
				}

				if (hit.collider.tag == "EZTB")// 쉬움상대 버튼 탐지시
				{
					//씬호출
					hit.collider.gameObject.SendMessage("EasyScene", SendMessageOptions.DontRequireReceiver);
				}

				if (hit.collider.tag == "NTB")// 중간상대 버튼 탐지시
				{
					//씬호출
					hit.collider.gameObject.SendMessage("EasyScene", SendMessageOptions.DontRequireReceiver);
				}

				if (hit.collider.tag == "BTB")// 어려운상대 버튼 탐지시
				{
					//씬호출
					hit.collider.gameObject.SendMessage(" ExitScene", SendMessageOptions.DontRequireReceiver);
				}

			}
			G_Bullet--;
			G_isReady = false;
		}
		void G_FireF()//총발사 실패
		{
            if(!GunSfxR.isPlaying)
            {
                GunSfxR.PlayOneShot(FireFSfx);
            }
            HandRAni.SetTrigger("FireFalse");
			Debug.Log("Icantfire");
		}

		void G_Reload()//재장전
		{
            
            
            HandRAni.SetTrigger("Reload");
			if(G_isReady == false)
			{
				if(G_Bullet<=0)
				{
                if (!GunSfxR.isPlaying)
                {
                    GunSfxR.PlayOneShot(Reload);
                }
                G_isReady = false;
				}
				else
				{
                    GunSfxR.PlayOneShot(Reload);
                    Debug.Log("reload");
					G_isReady = true;
				}
		}
	}


		void H_change()//손모양 교체
		{
			G_isGrap = true;
            G_gunhand.SetActive(true);
			HandnGun.SetActive(false);
		}

		void G_allReload()//총의 총알 초기화
		{
			G_Bullet = 6;
		}
		private void OnCollisionEnter(Collision collision)//손에 충돌시
		{
        float HandRG =
            OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        Debug.Log(HandRG);
        if (collision.gameObject.tag == "Gun"&&HandRG>=0.8f)
			{
                Debug.Log("충돌");
				H_change();//손모양 교체
			}
		}

    void FireSfxR()
    {
        GunSfxR.clip = FireSfx[Random.Range(0, 2)];
        GunSfxR.Play();
    }
}
