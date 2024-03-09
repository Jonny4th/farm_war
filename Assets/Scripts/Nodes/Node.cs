using FarmWar.ShieldFunction;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace FarmWar.Core
{
    public class Node : MonoBehaviour, IDamageable
    {
        [SerializeField] private Raidable raidable;
        public Raidable Raidable { get { return raidable; } }

        public List<Raid> Raids { get { return raidable.RaidList; } }
        public bool IsRaided => Raids.Count > 0;

        [SerializeField]
        private Shield Shield;

        public bool IsShielded => Shield.IsActivate;

        public Plantable plantable;

        private void Awake()
        {
            if (raidable == null) raidable = GetComponent<Raidable>();

            plantable.OnCropReady += CropReadyHandler;
            plantable.OnCropStolen += CropGoneHandler;

            Shield.OnShieldActivate += ShieldActivateHandler;
            Shield.OnShieldBreak += ShieldBreakHandler;

            raidable.OnRaidEnd += RaidEndHandler;
        }

        public void ActivateShield(int hitPoint)
        {
            Shield.ActivateShield(hitPoint);
        }

        private void ShieldActivateHandler(Shield shield)
        {
            shield.gameObject.SetActive(true);
        }

        private void ShieldBreakHandler(Shield shield)
        {
            shield.gameObject.SetActive(false);
        }

        private void RaidEndHandler(Raidable raidable)
        {
            plantable.Crop.CropStealing();
        }

        private void CropGoneHandler(Plantable plantable)
        {
            raidable.SetRaidable(false);
        }

        private void CropReadyHandler(Crop crop, Plantable plantable)
        {
            raidable.SetRaidable(true);
        }

        public void TakeDamage(float damage)
        {
            Shield.TakeDamage(1);
        }

        public static implicit operator Vector3(Node node)
        {
            return node.transform.position;
        }

        public static implicit operator Quaternion(Node node)
        {
            return node.transform.rotation;
        }
    }
}