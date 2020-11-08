using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G_ReloadUI : MonoBehaviour
{
    public SpriteRenderer ReloadUIHMD;//탄환수 ui
    public Sprite[] Reloadsprite;
    private int G_Bullet=6;


    // Use this for initialization
    void Start ()
    {
        ReloadUIHMD = GetComponent<SpriteRenderer>();
        ReloadUIHMD.sprite = Reloadsprite[6];
    }
	
    public void GunUI(int a)
    {
        G_Bullet = a;
        ReloadUIHMD.sprite = Reloadsprite[G_Bullet];
    }
    
}
