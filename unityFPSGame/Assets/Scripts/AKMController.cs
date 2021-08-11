using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyunSu;

public class AKMController : MonoBehaviour
{
    [SerializeField] private GameObject mySparkObject = null;
    [SerializeField] private GameObject myBulletSparkPrefab = null;

    [SerializeField] private int bulletMaxCount = 30;
                     private int bulletCurrentCount = 0;
    public int BulletMaxCount { get { return bulletMaxCount; } }
    public int BulletCurrentCount { get { return bulletCurrentCount; } }

    [SerializeField] private float AttackSpeed = 0.1f;
    [SerializeField] private float AKMAccuracy = 0.0f;

    private Animator AKMAnimator;
    private CrossHairUIController crossHairUI;

    private float AKMTimer = 0.0f;

    private RaycastHit hit;
    private int layerMask;

    private Vector3 totalAccuracyPos;

    public bool isReload = false;

    private void Awake()
    {
        AKMAnimator = GetComponent<Animator>();
        crossHairUI = FindObjectOfType<CrossHairUIController>();
    }

    private void Start()
    {
        bulletCurrentCount = bulletMaxCount;
        layerMask = 1 << LayerMask.NameToLayer("BulletCheck");
    }

    void Update()
    {
        if (!isReload && !transform.root.GetComponent<PlayerController>().isRun && Input.GetMouseButton(0) && bulletCurrentCount > 0)
        {
            if(!mySparkObject.activeSelf)
            {
                mySparkObject.SetActive(!mySparkObject.activeSelf);
            }

            AKMTimer += Time.deltaTime;

            if (AKMTimer >= AttackSpeed)
            {
                totalAccuracyPos = Camera.main.transform.forward + new Vector3(0.0f,
                                                                               Random.Range(-crossHairUI.poseAccuracy - AKMAccuracy, crossHairUI.poseAccuracy + AKMAccuracy),
                                                                               Random.Range(-crossHairUI.poseAccuracy - AKMAccuracy, crossHairUI.poseAccuracy + AKMAccuracy));

                if (Physics.Raycast(Camera.main.transform.position, totalAccuracyPos, out hit, 100.0f, layerMask))
                {
                    StartCoroutine(DestroySelf(Instantiate(myBulletSparkPrefab, hit.point, Quaternion.LookRotation(hit.normal)), 100.0f));
                    
                    if (hit.transform.GetComponent<Health>() != null)
                    {
                        //총 공격력 넣기
                        hit.transform.GetComponent<Health>().OnHit(1.0f);
                    }
                    Debug.Log(hit.transform.name);
                }

                AKMTimer = 0.0f;
                crossHairUI.FireAnim();
                AKMAnimator.SetTrigger("Fire");
                GetComponent<AudioSource>().Play();
                bulletCurrentCount--;
            }
        }
        else
        {
            if (mySparkObject.activeSelf)
            {
                mySparkObject.SetActive(!mySparkObject.activeSelf);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isReload && bulletCurrentCount < bulletMaxCount)
            {
                isReload = true;
                AKMAnimator.SetTrigger("Reload");
            }
        }
    }

    private void StartReload()
    {
        bulletCurrentCount = bulletMaxCount;
    }

    private void EndReload()
    {
        isReload = false;
    }

    public void WalkingAnim(bool _isWalk)
    {
        AKMAnimator.SetBool("Walk", _isWalk);
    }

    public void RunningAnim(bool _isRun)
    {
        AKMAnimator.SetBool("Run", _isRun);
    }

    IEnumerator DestroySelf(GameObject _object, float _timer)
    {
        yield return new WaitForSeconds(_timer);

        Destroy(_object);
    }
}
