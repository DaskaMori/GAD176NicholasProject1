using System.Collections;
using UnityEngine;

namespace Guns
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [Header("Weapon Settings")]
        [SerializeField] protected float fireRate   = 0.1f;
        [SerializeField] protected int   maxAmmo    = 30;
        [SerializeField] protected float reloadTime = 2f;

        protected int   currentAmmo;
        protected bool  isReloading;
        private   float nextFireTime;
        
        public int GetCurrentAmmo() => currentAmmo;


        protected virtual void Awake()
        {
            currentAmmo = maxAmmo;
        }

        public void Fire()
        {
            if (isReloading || Time.time < nextFireTime) return;

            if (currentAmmo > 0)
            {
                Shoot();
                currentAmmo--;
                nextFireTime = Time.time + fireRate;
            }
            else
            {
                // TODO: play empty‐click sound here
            }
        }

        public void Reload()
        {
            if (isReloading || currentAmmo >= maxAmmo) return;
            StartCoroutine(ReloadRoutine());
        }

        private IEnumerator ReloadRoutine()
        {
            isReloading = true;
            yield return new WaitForSeconds(reloadTime);
            currentAmmo  = maxAmmo;
            isReloading  = false;
        }
    
        protected abstract void Shoot();
    }
}