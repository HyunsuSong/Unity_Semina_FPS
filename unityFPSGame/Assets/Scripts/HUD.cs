using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public AKMController AKMInfo;
    public Text[] text_Bullet;
    public GameObject panel;

    void Update()
    {
        CheckBullet();
    }

    private void CheckBullet()
    {
        text_Bullet[0].text = AKMInfo.bulletCurrentCount.ToString();
        text_Bullet[1].text = " / ";
        text_Bullet[2].text = AKMInfo.bulletMaxCount.ToString();
    }
}
