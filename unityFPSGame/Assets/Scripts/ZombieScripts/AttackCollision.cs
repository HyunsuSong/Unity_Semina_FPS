using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private ZombieStatus myStatus;

    private void Start()
    {
        myStatus = transform.root.GetComponent<ZombieStatus>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != null && other.tag == "Player")
        {
            Debug.Log(other.name);
            other.GetComponent<Health>().OnHit(myStatus.attackDamage);
            //Debug.Break();
        }
    }
}
