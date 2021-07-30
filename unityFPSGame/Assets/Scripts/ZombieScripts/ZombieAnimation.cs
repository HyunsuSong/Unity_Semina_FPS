using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private Animator myAnimator;
    private Health myHealth;
    private ZombieStatus myStatus;
    private GameObject attackParts;

    // Start is called before the first frame update
    void Start()
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

        myHealth = GetComponent<Health>();
        myStatus = GetComponent<ZombieStatus>();
        attackParts = GameObject.Find("AttackCollision");
    }

    void Update()
    {
        IsDeadAnim();
        GetHitAnim();
        AttackAnim();
    }
    
    private void AttackAnim()
    {
        Debug.Log("어택애님 진입");

        if(myStatus.isAttack)
        {
            Debug.Log("공격 진입");
            myStatus.isAttack = false;
            myAnimator.SetTrigger("isAttack");
        }
    }

    private void AttackEnd()
    {
        myStatus.isAttack = false;
        attackParts.SetActive(false);
    }

    private void GetHitAnim()
    {
        if (myHealth.IsHit)
        {
            attackParts.SetActive(false);
            myAnimator.SetTrigger("GetHit");
            myHealth.IsHit = false;
        }
    }

    private void IsDeadAnim()
    {
        if(myHealth.IsDead)
        {
            attackParts.SetActive(false);
            int deadDivide = Random.Range(0, 2);

            if (deadDivide == 0)
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
    }
    IEnumerator DestroySelf(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
