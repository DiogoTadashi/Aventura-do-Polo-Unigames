using System;
using System.Collections;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator animator;
    public float attackRange = 0.75f;
    public int attackDamage = 10;
    public LayerMask enemyLayer;
    private bool isAttacking = false;
    private static bool hasSword = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator não atribuído ao PlayerCombat!");
        }
        InventoryController inventory = FindAnyObjectByType<InventoryController>();

        if (PlayerPrefs.GetInt("HasSword", 1) == 0)
        {
            hasSword = false;
        }
        else if (inventory != null && inventory.HasSword)
        {
            UnlockAttack(); // Habilita o ataque automaticamente se a espada já estiver no inventário
        }
    }

    void Update()
    {
        // O jogador só pode atacar se tiver a espada
        if (Input.GetMouseButtonDown(0) && !isAttacking && hasSword) 
        {
            Attack();
        }
    }

    void Attack()
    {
        isAttacking = true;
        animator.SetBool("isAttacking", true);

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDirection = (mousePosition - (Vector2)transform.position).normalized;

        animator.SetFloat("AttackX", attackDirection.x);
        animator.SetFloat("AttackY", attackDirection.y);

        animator.SetTrigger("Attack");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position + (Vector3)attackDirection * attackRange, attackRange, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage);
        }

        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        animator.SetBool("isAttacking", false);
    }

    public void UnlockAttack() // Método para desbloquear o ataque ao pegar a espada
    {
        hasSword = true;
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 attackDirection = (mousePosition - (Vector2)transform.position).normalized;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)attackDirection * attackRange, attackRange);
    }
}