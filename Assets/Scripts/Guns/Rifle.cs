using UnityEngine;

namespace Guns
{
    public class Rifle : BaseWeapon
    {
        [Header("Rifle Settings")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform  muzzlePoint;
        [SerializeField] private float      bulletSpeed  = 20f;
        [SerializeField] private float      bulletDamage = 20f;

        protected override void Shoot()
        {
            GameObject b = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);

            if (b.TryGetComponent<Bullet>(out var bullet))
                bullet.SetDamage(bulletDamage);

            var rb = b.GetComponent<Rigidbody>();
            rb.velocity = muzzlePoint.forward * bulletSpeed;
        }
    }
}