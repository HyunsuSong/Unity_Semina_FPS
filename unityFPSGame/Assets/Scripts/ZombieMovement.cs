using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private GameObject targetObject;
    private Vector3 vectorToTarget;
    private float distanceBetweenTarget;

    [SerializeField]
    private float findRange = 10.0f;
    [SerializeField]
    private float attackRange = 1.0f;
    [SerializeField]
    private float rotationSpeed = 1.0f;
    [SerializeField]
    private float moveSpeed = 5.0f;

    private Animator myAnimator;

    void Start()
    {
        targetObject = GameObject.FindGameObjectWithTag("Player");

        if(targetObject != null)
        {
            Debug.Log(targetObject.name);
        }
        else
        {
            Debug.LogError("타겟을 찾을 수 없음, 태그 확인");
            Debug.Break();
        }

        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        FindTarget();
    }

    void FindTarget()
    {
        vectorToTarget = targetObject.transform.position - transform.position;
        vectorToTarget.y = 0.0f;
        distanceBetweenTarget = vectorToTarget.magnitude;

        if(distanceBetweenTarget <= findRange && distanceBetweenTarget >= attackRange)
        {
            myAnimator.SetBool("FindTarget", true);

            Vector3 toLook = Vector3.Slerp(transform.forward, vectorToTarget.normalized, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.LookRotation(toLook, Vector3.up);

            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            myAnimator.SetBool("FindTarget", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, findRange);
    }
}
