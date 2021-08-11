using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyunSu;

public class HealthItem : Item
{
    public float healValue = 5.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<ItemInventory>().HealthItemCount++;
            Destroy(gameObject);
        }
    }

    public bool UseHealthItem(Health _playerHealth)
    {
        if (_playerHealth.CurrentHealth < _playerHealth.MaxHealth)
        {
            _playerHealth.CurrentHealth = Mathf.Min(_playerHealth.CurrentHealth + healValue, _playerHealth.MaxHealth);

            return true;
        }

        return false;
    }
}
