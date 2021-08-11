using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HyunSu;

public class ItemInventory : MonoBehaviour
{
    public int HealthItemCount = 0;
    public HealthItem healthItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && HealthItemCount > 0)
        {
            if (healthItem.UseHealthItem(GetComponent<Health>())) HealthItemCount--;
        }
    }
}
