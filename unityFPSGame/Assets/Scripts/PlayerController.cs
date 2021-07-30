using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float sensitivity = 1.5f;
    public float cameraRotationX = 0.0f;
    public float cameraRotationLimit = 45.0f;
    public Vector3 characterRotationY = Vector3.zero;

    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        Cursor.visible = false;
    }


    private void Update()
    {
        velocity = (transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal")).normalized;

        transform.position += velocity * moveSpeed * Time.deltaTime;

        cameraRotationX -= Input.GetAxisRaw("Mouse Y") * sensitivity;

        cameraRotationX = Mathf.Clamp(cameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        Camera.main.transform.localEulerAngles = new Vector3(cameraRotationX, 0.0f, 0.0f);

        characterRotationY = new Vector3(0.0f, Input.GetAxisRaw("Mouse X"), 0.0f) * sensitivity;

        transform.rotation *= Quaternion.Euler(characterRotationY);
    }
}
