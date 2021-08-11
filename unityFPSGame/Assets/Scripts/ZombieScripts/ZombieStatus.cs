using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기획자가 수정할 수 있는 데이터들의 모음.
public class ZombieStatus : MonoBehaviour
{
    public float findRange = 10.0f;
    public float attackRange = 1.0f;
    public float attackDamage = 1.0f;
    public float rotationSpeed = 1.0f;
    public float moveSpeed = 5.0f;

    public float attackDelay = 2.633f;
    public bool isAttack = false;
}
