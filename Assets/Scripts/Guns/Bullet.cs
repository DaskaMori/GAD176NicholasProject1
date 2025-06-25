using UnityEngine;

namespace Guns
{
    public class Bullet : MonoBehaviour
    {
        [Header("Bullet Settings")]
        [SerializeField] private float damage   = 20f;
        [SerializeField] private float lifeTime = 5f;

        private void Awake()
        {
            Destroy(gameObject, lifeTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent<IDamageable>(out var d))
                d.TakeDamage(damage);

            Destroy(gameObject);
        }


        public void SetDamage(float dmg)
        {
            damage = dmg;
        }
    }
}