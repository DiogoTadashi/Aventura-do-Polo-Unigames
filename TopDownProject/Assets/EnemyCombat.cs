using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 0.80f;
    public float attackCooldown = 1.75f;
    private bool isAttacking = false;
    private EnemyAI enemyAI;
    private HealthUI healthUI;

    private float eixoX;
    private float eixoY;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyAI = GetComponent<EnemyAI>();

        healthUI = FindAnyObjectByType<HealthUI>();

        if (animator == null)
        {
            Debug.LogError("🚨 Animador não atribuído ao EnemyCombat!");
        }

        if (enemyAI == null)
        {
            Debug.LogError("🚨 EnemyAI não encontrado!");
        }

        if (healthUI == null)
        {
            Debug.LogError("🚨 HealthUI não foi encontrado!");
        }

        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("🚨 O inimigo não tem um Collider2D, o que pode afetar a detecção de ataque!");
        }
    }

    void Update()
    {
        if (enemyAI != null)
        {
            Collider2D enemyCollider = GetComponent<Collider2D>();
            if (enemyCollider != null)
            {
                //pega a alt do inim e divide na metade p colocar a area de ataque no meio para pegar todos os lados
                float heightOffset = enemyCollider.bounds.size.y / 2;
                Vector2 attackPosition = new Vector2(transform.position.x, transform.position.y + heightOffset);

                Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition, attackRange);

                Collider2D playerCollider = null;
                foreach (Collider2D col in colliders)
                {
                    if (col.CompareTag("Player"))
                    {
                        playerCollider = col;
                        break;
                    }
                }

                if (playerCollider != null && enemyAI.isChasingPlayer && !isAttacking)
                {
                    float distance = Vector2.Distance(playerCollider.bounds.center, attackPosition);

                    if (distance <= attackRange)
                    {
                        StartCoroutine(Attack());
                    }
                }
            }
        }
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        eixoX = enemyAI.moveDirection.x;
        eixoY = enemyAI.moveDirection.y;

        animator.SetTrigger("Attack");
        animator.SetFloat("MoveX", eixoX);
        animator.SetFloat("MoveY", eixoY);

        yield return new WaitForSeconds(0.2f);

        Vector2 attackPosition = new Vector2(transform.position.x, transform.position.y + 0.3f);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPosition, attackRange);
        Collider2D playerCollider = null;
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                playerCollider = col;
                break;
            }
        }

        if (playerCollider != null && healthUI != null)
        {
            healthUI.TakeDamage(1);
        }

        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    private void OnDrawGizmosSelected()
    {
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError("🚨 ERRO: O inimigo não possui um Collider2D! O Gizmos pode estar desalinhado.");
            return;
        }

        Vector2 gizmoPosition = new Vector2(transform.position.x, transform.position.y + 0.3f);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(gizmoPosition, attackRange);
    }
}