using System;
using UnityEngine;

namespace FarmWar.ShieldFunction
{
    public class Shield : MonoBehaviour, IDamageable
    {
        public int RemainHit { get; private set; }

        public event Action OnShieldBreak;

        public void SetMaxHit(int maxHit)
        {
            RemainHit = maxHit;
        }

        public void TakeDamage(float damage)
        {
            RemainHit--;

            if (RemainHit == 0)
            {
                OnShieldBreak?.Invoke();
            }
        }
    }
}