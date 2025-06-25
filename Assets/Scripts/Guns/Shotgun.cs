using UnityEngine;

namespace Guns
{
    public class Shotgun : BaseWeapon
    {
        [Header("Shotgun Settings")]
        [SerializeField] private int   pelletCount = 6;
        [SerializeField] private float spreadAngle = 15f;
        [SerializeField] private float pelletDamage = 10f;

        protected override void Shoot()
        {
            for (int i = 0; i < pelletCount; i++)
            {
                Vector3 dir = Quaternion.Euler(
                    Random.Range(-spreadAngle, spreadAngle),
                    Random.Range(-spreadAngle, spreadAngle),
                    0f
                ) * transform.forward;

                if (Physics.Raycast(transform.position, dir, out RaycastHit hit))
                {
                    if (hit.collider.TryGetComponent<IDamageable>(out var target))
                    {
                        float falloff = Mathf.Clamp01(1f - (hit.distance / 50f));
                        target.TakeDamage(pelletDamage * falloff);
                    }
                }
            }
        }
    }
}