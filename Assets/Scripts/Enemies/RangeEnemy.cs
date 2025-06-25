using System.Collections;
using Guns;
using UnityEngine;

namespace Enemies
{
    public class RangedEnemy : EnemyBase
    {
        [Header("Gun")]
        [SerializeField] private BaseWeapon weapon;

        [Header("Tactics")]
        [Tooltip("Desired distance to keep from player when firing")]
        [SerializeField] private float desiredRange    = 5f;
        [Tooltip("Cooldown between shots, in seconds")]
        [SerializeField] private float attackCooldown  = 2f;

        protected override void Update()
        {
            if (!health.IsAlive) return;

            float dist = Vector3.Distance(transform.position, player.position);

            Vector3 lookDir = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(lookDir);

            if (dist > desiredRange)
            {
                MoveTowards(player.position, moveSpeed);
            }
            else
            {
                if (!isAttacking)
                    StartCoroutine(AttackRoutine());
            }
        }


        protected override IEnumerator AttackRoutine()
        {
            isAttacking = true;

            weapon.Fire();

            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
        }

        private void MoveTowards(Vector3 target, float speed)
        {
            Vector3 dir = (target - transform.position).normalized;
            controller.Move(dir * (speed * Time.deltaTime));
        }
    }
}