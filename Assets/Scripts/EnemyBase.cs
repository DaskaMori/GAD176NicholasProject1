using UnityEngine;
using System.Collections;

[RequireComponent(typeof(HealthComponent))]
public abstract class EnemyBase : MonoBehaviour {
    [Header("AI Settings")]
    [SerializeField] protected float moveSpeed = 3f;
    [SerializeField] protected float detectionRange = 10f;
    [SerializeField] protected float attackRange = 2f;

    protected HealthComponent health;
    protected Transform player;
    protected CharacterController controller;
    protected bool isAttacking;

    protected virtual void Awake() {
        health = GetComponent<HealthComponent>();
        player = GameObject.FindWithTag("Player").transform;
        controller = GetComponent<CharacterController>();
        health.OnDeath += Die;
    }

    protected virtual void Update() {
        if (!health.IsAlive) return;
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange) {
            if (!isAttacking) StartCoroutine(AttackRoutine());
        } else if (dist <= detectionRange) {
            MoveTowards(player.position);
        }
    }

    protected void MoveTowards(Vector3 targetPos) {
        Vector3 dir = (targetPos - transform.position).normalized;
        controller.Move(dir * moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(dir);
    }

    protected abstract IEnumerator AttackRoutine();

    protected virtual void Die() {
        controller.enabled = false;
        this.enabled = false;
    }
}