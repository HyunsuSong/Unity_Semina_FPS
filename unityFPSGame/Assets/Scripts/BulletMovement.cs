using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    private CameraManager cameraScript;

    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraManager>();

        if (cameraScript.hit.point != Vector3.zero)
            transform.LookAt(cameraScript.hit.point);
        else
            transform.LookAt(Camera.main.transform.position + Camera.main.transform.forward * 100.0f);

        transform.localEulerAngles += new Vector3(-90.0f, 0.0f, 0.0f);
        Debug.Log(cameraScript.hit.point);

        StartCoroutine(DestroySelf(55.0f));
    }

    void Update()
    {
        transform.position += -transform.up * moveSpeed * Time.deltaTime;
    }

    IEnumerator DestroySelf(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(gameObject);
    }
}
