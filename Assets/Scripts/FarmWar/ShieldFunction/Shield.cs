using System;
using UnityEngine;

namespace FarmWar.ShieldFunction
{
    public class Shield : MonoBehaviour, IDamageable
    {
        public int RemainHit { get; private set; }

        public bool IsActivate { get; private set; } = false;


        public event Action<Shield> OnShieldActivate;
        public event Action<Shield> OnShieldBreak;

        public void SetMaxHit(int maxHit)
        {
            RemainHit = maxHit;
        }

        public void TakeDamage(float damage)
        {
            RemainHit--;

            if (RemainHit == 0)
            {
                IsActivate = false;
                OnShieldBreak?.Invoke(this);
            }
        }

        public void ActivateShield(int maxHit)
        {
            IsActivate = true;
            SetMaxHit(maxHit);
            OnShieldActivate?.Invoke(this);
        }
    }
}