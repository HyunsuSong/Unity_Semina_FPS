using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HyunSu;

public class ZombieHealthUIController : MonoBehaviour
{
    public GameObject player;
    public GameObject slider;

    // Update is called once per frame
    void Update()
    {
        //임시, 벡터 계산으로 진행할 것, 플레이어도 괜찮고 카메라 기준으로도 괜찮음 > 1인칭이기 때문
        transform.LookAt(transform.position + player.transform.rotation * Vector3.forward);

        slider.GetComponent<Slider>().value = transform.root.GetComponent<Health>().CurrentHealth / transform.root.GetComponent<Health>().MaxHealth; 
    }
}
