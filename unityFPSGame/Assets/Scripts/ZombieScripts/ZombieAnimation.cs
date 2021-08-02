using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimation : MonoBehaviour
{
    private Animator myAnimator;
    private ZombieHealth myHealth;
    private ZombieStatus myStatus;
    [SerializeField]
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

        myHealth = GetComponent<ZombieHealth>();
        myStatus = GetComponent<ZombieStatus>();
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

    private void AttackStart()
    {
        attackParts.GetComponent<BoxCollider>().enabled = true;
    }

    private void AttackEnd()
    {
        myStatus.isAttack = false;
        attackParts.GetComponent<BoxCollider>().enabled = false;
    }

    private void GetHitAnim()
    {
        if (myHealth.IsHit)
        {
            attackParts.GetComponent<BoxCollider>().enabled = false;
            myAnimator.SetTrigger("GetHit");
            myHealth.canHeal = false;
            myHealth.IsHit = false;
        }
    }

    private void HitEnd()
    {
        myHealth.canHeal = true;
    }

    private void IsDeadAnim()
    {
        if(myHealth.IsDead)
        {
            attackParts.GetComponent<BoxCollider>().enabled = false;
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
