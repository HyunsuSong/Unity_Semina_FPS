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
        //�ӽ�, ���� ������� ������ ��, �÷��̾ ������ ī�޶� �������ε� ������ > 1��Ī�̱� ����
        transform.LookAt(transform.position + player.transform.rotation * Vector3.forward);

        slider.GetComponent<Slider>().value = transform.root.GetComponent<Health>().CurrentHealth / transform.root.GetComponent<Health>().MaxHealth; 
    }
}
