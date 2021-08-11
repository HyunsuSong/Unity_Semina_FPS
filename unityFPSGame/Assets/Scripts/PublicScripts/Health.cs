using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HyunSu
{
    public class Health : MonoBehaviour
    {
        [SerializeField]
        protected float maxHealth = 10.0f;
        public float MaxHealth { get { return maxHealth; } }
        [SerializeField]
        protected float currentHealth = 0.0f;
        public float CurrentHealth { get { return currentHealth; } set { currentHealth = value; } }

        [SerializeField]
        protected float healTimer = 0.0f;
        [SerializeField]
        protected float healValue = 0.0f;

        protected bool canUpdate = true;

        private bool isHit = false;
        public bool IsHit
        {
            get { return isHit; }
            set { isHit = value; }
        }

        private bool isDead = false;
        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        protected void Start()
        {
            currentHealth = maxHealth;
        }

        protected void Update()
        {
            GetDead();
        }

        public void OnHit(float hitDamage)
        {
            isHit = true;
            currentHealth = Mathf.Max(currentHealth - hitDamage, 0.0f);
        }

        private void GetDead()
        {
            if (currentHealth == 0.0f)
            {
                isDead = true;
                canUpdate = false;
            }
            else
            {
                canUpdate = true;
                isDead = false;
            }
        }
    }
}
