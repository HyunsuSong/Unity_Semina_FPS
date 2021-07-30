using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    [SerializeField]
    private float damage = 1.0f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("isTriggerHit");
        
        if(other.GetComponent<Health>() != null)
        {
            Debug.Log(other.name);

            other.GetComponent<Health>().IsHit(damage);
        }

        Destroy(gameObject);
    }
}
