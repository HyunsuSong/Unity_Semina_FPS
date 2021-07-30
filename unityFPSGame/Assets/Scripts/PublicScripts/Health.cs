using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float maxHealth=10.0f;
    [SerializeField]
    private float currentHealth=0.0f;
    [SerializeField]
    private float healTimer = 0.0f;
    [SerializeField]
    private float healValue = 0.0f;



    private bool canUpdate = true;
    [SerializeField]
    private bool useAutoHeal = false;
    private bool isHit = false;
    public bool IsHit
    {
        get { return isHit; }
        set { isHit = value; }
    }

    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (canUpdate)
        {
            if (!GetDead())
            {
                if (useAutoHeal)
                {
                    AutoHeal();
                }
            }
        }
    }

    public void OnHit(float hitDamage)
    {
        isHit = true;
        currentHealth = Mathf.Max(currentHealth - hitDamage, 0.0f);

        // isHit = false;
        // AutoHeal을 위한 isHIt false 로직 제작
    }

    private bool GetDead()
    {
        if(currentHealth == 0.0f)
        {
            isDead = true;
            canUpdate = false;
        }
        else
        {
            isDead = false;
        }

        return isDead;
    }

    private void AutoHeal()
    {
        if (!isHit)
            healTimer += Time.deltaTime;
        else
            healTimer = 0.0f;

        if (currentHealth < maxHealth && healTimer >= 3.0f)
        {
            currentHealth += healValue * Time.deltaTime;
        }
    }
}
