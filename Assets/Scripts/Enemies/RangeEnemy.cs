using System.Collections;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(HealthComponent), typeof(CharacterController))]
    public class RangedEnemy : EnemyBase
    {
        [Header("Ranged Settings")]
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed = 15f;
        [SerializeField] private float bulletDamage = 20f;

        protected override IEnumerator AttackRoutine()
        {
            isAttacking = true;

            Vector3 dir = (player.position - transform.position).normalized;
            Vector3 spawnPos = transform.position + dir;
            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.LookRotation(dir));

            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = dir * bulletSpeed;

            yield return new WaitForSeconds(2f);
            isAttacking = false;
        }
    }
}