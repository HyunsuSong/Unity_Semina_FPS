using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameObject attackParts;

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");
        attackParts = GameObject.Find("AttackCollision");
        attackParts.SetActive(false);

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

        if(distanceBetweenTarget <= myStatus.findRange && distanceBetweenTarget >= myStatus.attackRange)
        {
            isClose = false;
            // 애니메이션 부분을 zombieAni~~ 스크립트에 옮길 것.
            myAnimator.SetBool("FindTarget", true);

            // 인지 범위 이내에 있을 경우 캐릭터 방향으로 회전하여 맞서서 볼 수 있음
            // 단 일정 사거리 이내(공격 사거리 등)에 있을 경우 해당 코드가 없으므로 계속 공격이 가능한 상황이면
            // 전혀 다른 쪽으로 공격을 하는 것을 알 수 있음 > 수정이 필요한 상황
            // >> 구간을 몬스터의 인지 범위 이내인지 아닌지 검사, 인지 범위 이내일 경우
            // >> 인지 범위이내이면서 공격 범위 이내인지까지 판별하면 될 것

            // 캐릭터가 몬스터의 공격에 맞으면 캐릭터가 밀리는 부분이 있음
            // 또는 캐릭터를 고정시키면 몬스터가 공격을 했을 경우 자신이 밀리는 부분이 있음
            // 

            Vector3 toLook = Vector3.Slerp(transform.forward, vectorToTarget.normalized, myStatus.rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(toLook, Vector3.up);
            
            //if (!myHealth.IsHit)
            //{
            //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
            //}
        }
        else
        {
            isClose = true;
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
            attackParts.SetActive(true);
            attackTimer = 0.0f;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, myStatus.findRange);
    }
}
