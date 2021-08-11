using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyunSu;
public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private float damage = 1.0f;

    void Start()
    {
        transform.localEulerAngles += new Vector3(-90.0f, 0.0f, 0.0f);

        StartCoroutine(DestroySelf(55.0f));
    }

    void Update()
    {
        transform.position += -transform.up * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("isTriggerHit");

        if (other.GetComponent<Health>() != null)
        {
            Debug.Log(other.name);

            other.GetComponent<Health>().OnHit(damage);
        }

        Destroy(gameObject);
    }

    IEnumerator DestroySelf(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
