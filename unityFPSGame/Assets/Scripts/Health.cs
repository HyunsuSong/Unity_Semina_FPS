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

    private Animator myAnimator;

    private bool isHit = false;
    private bool isDead = false;

    private void Start()
    {
        if (GetComponent<Animator>() != null)
        {
            myAnimator = GetComponent<Animator>();
        }
        else
        {
            Debug.LogWarning("애니메이터 컴포넌트 부재");
            Debug.Break();
        }

        currentHealth = maxHealth;
    }

    private void Update()
    {
        if (!IsDead())
        {
            AutoHeal();
        }
    }

    public void IsHit(float hitDamage)
    {
        currentHealth = Mathf.Max(currentHealth - hitDamage, 0.0f);
        myAnimator.SetTrigger("GetHit");

        if(!isHit)
            StartCoroutine("HitCoroutine");
    }

    private bool IsDead()
    {
        if(currentHealth == 0.0f)
        {
            isDead = true;

            int deadDivide = Random.Range(0, 2);

            if(deadDivide == 0)
            {
                myAnimator.SetBool("IsDead", true);
                StartCoroutine(DestroySelf(3.333f));
            }
            else
            {
                myAnimator.SetBool("IsDead2", true);
                StartCoroutine(DestroySelf(2.967f));
            }
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

    IEnumerator HitCoroutine()
    {
        isHit = true;

        yield return new WaitForSeconds(0.1f);
        isHit = false;
    }
    IEnumerator DestroySelf(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
