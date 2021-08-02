using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : Health
{
    [SerializeField]
    private bool useAutoHeal = false;
    public bool canHeal = false;

    // Start is called before the first frame update
    private new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    private new void Update()
    {
        base.Update();
        
        if(useAutoHeal)
        {
            AutoHeal();
        }
    }

    private void AutoHeal()
    {
        if (canHeal)
            healTimer += Time.deltaTime;
        else
            healTimer = 0.0f;

        if (currentHealth < maxHealth && healTimer >= 3.0f)
        {
            currentHealth += healValue * Time.deltaTime;
        }
    }
}
