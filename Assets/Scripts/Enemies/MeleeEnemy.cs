using System.Collections;
using UnityEngine;

namespace Enemies
{
    public class MeleeEnemy : EnemyBase
    {
        [Header("Melee Settings")]
        [SerializeField] private float damage   = 20f;
        [SerializeField] private float cooldown = 1f;

        protected override IEnumerator AttackRoutine()
        {
            isAttacking = true;

            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                player.GetComponent<IDamageable>()?.TakeDamage(damage);
            }

            yield return new WaitForSeconds(cooldown);
            isAttacking = false;
        }
    }
}