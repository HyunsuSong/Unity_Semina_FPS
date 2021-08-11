using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyunSu;

public class ZombieMovement : MonoBehaviour
{
    private GameObject targetObject;
    private Vector3 vectorToTarget;
    private float distanceBetweenTarget;
    private float attackTimer = 0.0f;
    private bool isClose = false;

    private Animator myAnimator;
    private Health myHealth;
    private ZombieStatus myStatus;

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");

        if (targetObject != null)
        {
            Debug.Log(targetObject.name);
        }
        else
        {
            Debug.LogError("타겟을 찾을 수 없음, 태그 확인");
            Debug.Break();
        }

        myAnimator = GetComponent<Animator>();
        myHealth = GetComponent<Health>();
        myStatus = GetComponent<ZombieStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();

        if(isClose)
        {
            AttackTarget();
        }
    }

    void FindTarget()
    {
        vectorToTarget = targetObject.transform.position - transform.position;
        vectorToTarget.y = 0.0f;
        distanceBetweenTarget = vectorToTarget.magnitude;
        Vector3 toLook = Vector3.Slerp(transform.forward, vectorToTarget.normalized, myStatus.rotationSpeed * Time.deltaTime);

        //인지 범위 이내에 있는가? > 공격 범위는 인지 범위 안에 속함
        if (distanceBetweenTarget <= myStatus.findRange)
        {
            transform.rotation = Quaternion.LookRotation(toLook, Vector3.up);

            // 공격 범위 이내에 있는가?
            if (distanceBetweenTarget <= myStatus.attackRange)
            {
                isClose = true;
                myAnimator.SetBool("FindTarget", false);
            }
            else
            {
                isClose = false;
                myAnimator.SetBool("FindTarget", true);
            }

            // 애니메이션 부분을 zombieAni~~ 스크립트에 옮길 것.

            //루트 모션 사용을 위해 움직임 코드를 따로 사용하지 않음
            //if (!myHealth.IsHit)
            //{
            //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
            //}
        }
        // 인지 범위 밖에 있는가?
        else
        {
            isClose = false;
            // 애니메이션 부분을 zombieAni~~ 스크립트에 옮길 것.
            myAnimator.SetBool("FindTarget", false);
        }
    }

    void AttackTarget()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= myStatus.attackDelay)
        {
            myStatus.isAttack = true;
            attackTimer = 0.0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // NullReferenceExpection 발생 > start에서 myStatus를 할당하고 있기 때문에
        // 해당 함수 특성상 플레이 중이 아닌 경우에도 돌아가기 때문
        //Gizmos.DrawWireSphere(transform.position, myStatus.findRange);
    }
}
