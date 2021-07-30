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

        if(distanceBetweenTarget <= myStatus.findRange && distanceBetweenTarget >= myStatus.attackRange)
        {
            isClose = false;
            // �ִϸ��̼� �κ��� zombieAni~~ ��ũ��Ʈ�� �ű� ��.
            myAnimator.SetBool("FindTarget", true);

            // ���� ���� �̳��� ���� ��� ĳ���� �������� ȸ���Ͽ� �¼��� �� �� ����
            // �� ���� ��Ÿ� �̳�(���� ��Ÿ� ��)�� ���� ��� �ش� �ڵ尡 �����Ƿ� ��� ������ ������ ��Ȳ�̸�
            // ���� �ٸ� ������ ������ �ϴ� ���� �� �� ���� > ������ �ʿ��� ��Ȳ
            // >> ������ ������ ���� ���� �̳����� �ƴ��� �˻�, ���� ���� �̳��� ���
            // >> ���� �����̳��̸鼭 ���� ���� �̳��������� �Ǻ��ϸ� �� ��

            // ĳ���Ͱ� ������ ���ݿ� ������ ĳ���Ͱ� �и��� �κ��� ����
            // �Ǵ� ĳ���͸� ������Ű�� ���Ͱ� ������ ���� ��� �ڽ��� �и��� �κ��� ����
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
