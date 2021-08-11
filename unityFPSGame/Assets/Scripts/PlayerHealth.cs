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

    // ��Ƴ� ���� ���� �ð��� �ִ� ���� ���� ��.
    private void Respawn()
    {
        HUD.PlayerDead = true;
        HUD.startFadeInOut = true;

        if (HUD.endFadeIn)
        {
            transform.position = new Vector3(respawnPosition.transform.position.x, transform.position.y, respawnPosition.transform.position.z);
            //��Ȱ ���� ����
            base.currentHealth = maxHealth;
            base.IsDead = false;
            HUD.PlayerDead = false;
        }
    }
}
