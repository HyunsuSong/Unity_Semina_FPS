using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyunSu;

public class PlayerHealth : Health
{
    private HUD HUD;
    private GameObject respawnPosition;

    private bool endRespawn = true;
    public bool EndRespawn { get { return endRespawn; } set { endRespawn = value; } }


    private new void Start()
    {
        base.Start();
        HUD = GameObject.Find("PlayerCanvas").GetComponent<HUD>();
        respawnPosition = GameObject.Find("RespawnPoint");
    }

    private new void Update()
    {
        base.Update();

        if (canUpdate)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                base.OnHit(60);
            }
        }
        else 
        {
            if (base.IsDead)
            {
                Respawn();
            }
        }
    }

    // 살아난 이후 무적 시간을 주는 것이 좋을 듯.
    private void Respawn()
    {
        HUD.PlayerDead = true;
        HUD.startFadeInOut = true;

        if (HUD.endFadeIn)
        {
            transform.position = new Vector3(respawnPosition.transform.position.x, transform.position.y, respawnPosition.transform.position.z);
            //부활 로직 생성
            base.currentHealth = maxHealth;
            base.IsDead = false;
            HUD.PlayerDead = false;
        }
    }
}
