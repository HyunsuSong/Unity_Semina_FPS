using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private int layerMask;

    public RaycastHit hit;

    void Start()
    {
        layerMask = 1 << LayerMask.NameToLayer("BulletCheck");
    }

    private void Update()
    {
        Debug.DrawLine(transform.position, transform.forward * 100.0f, Color.blue);

        if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f, layerMask))
        {
            Debug.Log(hit.transform.name);
        }
    }
}
