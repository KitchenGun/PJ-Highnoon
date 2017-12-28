using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Ctrl : MonoBehaviour {

    private int P_HP = 100;
   
    
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
            //animator.SetBool("IsPDie", true);

    }
}
