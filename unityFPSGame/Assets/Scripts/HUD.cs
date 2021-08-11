using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HyunSu;
public class HUD : MonoBehaviour
{
    public AKMController AKMInfo;
    public Text[] text_Bullet;
    public GameObject panel;
    public float speed;
    public GameObject fadeInPanel;
    private Image fadeInPanelImage;
    [SerializeField]
    private Color fadeInPanelColor;
    public bool endFadeIn = false;
    public bool startFadeInOut = false;
    private bool playerDead = false;
    public bool PlayerDead { set { playerDead = value; } }

    private GameObject player;
    [SerializeField]
    private Slider playerHP;

    private void Start()
    {
        if(fadeInPanel != null)
        {
            fadeInPanelImage = fadeInPanel.GetComponent<Image>();
            fadeInPanelColor = fadeInPanelImage.color;
        }
        else
        {
            Debug.LogError("패널 컴포넌트 오류");
            Debug.Break();
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        CheckHP();
        CheckBullet();

        if (startFadeInOut)
        {
            if (playerDead)
            {
                FadeIn();
            }
            else
            {
                FadeOut();
            }
        }
    }

    private void CheckHP()
    {
        playerHP.value = player.GetComponent<Health>().CurrentHealth / player.GetComponent<Health>().MaxHealth;
    }

    private void CheckBullet()
    {
        text_Bullet[0].text = AKMInfo.BulletCurrentCount.ToString();
        text_Bullet[1].text = " / ";
        text_Bullet[2].text = AKMInfo.BulletMaxCount.ToString();
    }

    private void FadeIn()
    {
        fadeInPanelColor.a += speed * Time.deltaTime;
        fadeInPanelImage.color = fadeInPanelColor;

        if (fadeInPanelImage.color.a >= 1.0f)
        {
            endFadeIn = true;
        }
    }

    private void FadeOut()
    {
        fadeInPanelColor.a -= speed * Time.deltaTime;
        fadeInPanelImage.color = fadeInPanelColor;

        if (fadeInPanelImage.color.a <= 0.0f)
        {
            startFadeInOut = false;
            endFadeIn = false;
        }
    }
}
