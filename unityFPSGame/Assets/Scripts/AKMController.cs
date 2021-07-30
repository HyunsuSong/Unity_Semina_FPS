using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKMController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;
    [SerializeField]
    private GameObject myMuzzle = null;
    [SerializeField]
    private GameObject mySpark = null;
    [SerializeField]
    private GameObject myBulletPrefab;

    public int bulletMaxCount = 30;
    public int bulletCurrentCount = 30;
    public float AKMTimer = 0.0f;
    public float AttackSpeed = 0.1f;

    void Update()
    {
        if(Input.GetMouseButton(0) && bulletCurrentCount > 0)
        {
            AKMTimer += Time.deltaTime;

            mySpark.gameObject.SetActive(true);

            if (AKMTimer >= AttackSpeed)
            {
                AKMTimer = 0.0f;

                Instantiate(myBulletPrefab, myMuzzle.transform.position, myMuzzle.transform.rotation);
                GetComponent<Animator>().SetTrigger("Fire");
                GetComponent<AudioSource>().Play();
                bulletCurrentCount--;
            }
        }
        else
        {
            mySpark.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (bulletCurrentCount < bulletMaxCount)
            {
                bulletCurrentCount = bulletMaxCount;
            }
        }
    }
}
