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
            Debug.LogError("Ÿ���� ã�� �� ����, �±� Ȯ��");
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

        //���� ���� �̳��� �ִ°�? > ���� ������ ���� ���� �ȿ� ����
        if (distanceBetweenTarget <= myStatus.findRange)
        {
            transform.rotation = Quaternion.LookRotation(toLook, Vector3.up);

            // ���� ���� �̳��� �ִ°�?
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

            // �ִϸ��̼� �κ��� zombieAni~~ ��ũ��Ʈ�� �ű� ��.

            //��Ʈ ��� ����� ���� ������ �ڵ带 ���� ������� ����
            //if (!myHealth.IsHit)
            //{
            //    transform.position += transform.forward * moveSpeed * Time.deltaTime;
            //}
        }
        // ���� ���� �ۿ� �ִ°�?
        else
        {
            isClose = false;
            // �ִϸ��̼� �κ��� zombieAni~~ ��ũ��Ʈ�� �ű� ��.
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

        // NullReferenceExpection �߻� > start���� myStatus�� �Ҵ��ϰ� �ֱ� ������
        // �ش� �Լ� Ư���� �÷��� ���� �ƴ� ��쿡�� ���ư��� ����
        //Gizmos.DrawWireSphere(transform.position, myStatus.findRange);
    }
}
