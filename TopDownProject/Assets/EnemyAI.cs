using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    public Transform target;
    public Vector2 moveDirection;
    private Animator anim;
    
    // Patrulha
    private Vector2 startPosition;
    private bool movingRight = true;
    public float patrolDistance = 10f;

    // Detecção do jogador
    public bool isChasingPlayer = false;
    public float chaseDistance = 6f;
    private bool combatTutorialShown = false; // Variável para evitar repetição

    // Desvio de obstáculos
    public LayerMask obstacleLayer;

    // Referência ao TutorialController
    public TutorialController tutorialController;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (anim == null)
        {
            Debug.LogError("Animator não encontrado! Certifique-se de que o GameObject tem um Animator.");
        }

        startPosition = transform.position;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.LogError("Objeto 'Player' não encontrado! Certifique-se de que ele tem a tag 'Player'.");
        }
    }

    private void Update()
    {
        if (target != null)
        {
            float distanciaJogador = Vector2.Distance(transform.position, target.position);

            if (distanciaJogador <= chaseDistance)
            {
                // Se o inimigo ainda não estava perseguindo, ativar perseguição
                if (!isChasingPlayer)
                {
                    isChasingPlayer = true; // Agora ele entrou em perseguição
                    
                    // Exibir o tutorial apenas quando a perseguição for confirmada
                    if (!combatTutorialShown)
                    {
                        tutorialController.ShowCombatTutorial();
                        combatTutorialShown = true;
                    }
                }

                ChasePlayer(); // Só chama ChasePlayer após confirmar o estado
            }
            else
            {
                Patrol();
                isChasingPlayer = false; // Resetar estado caso volte a patrulhar
            }
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void ChasePlayer()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, obstacleLayer);
        if (hit.collider != null)
        {
            if (hit.normal.x != 0) // Se há um obstáculo lateral, ajustar para cima/baixo
            {
                moveDirection = new Vector2(direction.x, direction.y + 1f).normalized;
            }
            else if (hit.normal.y != 0) // Se há um obstáculo superior/inferior, ajustar para os lados
            {
                moveDirection = new Vector2(direction.x + 1f, direction.y).normalized;
            }
            else // Caso não tenha desvio, recua
            {
                moveDirection = -direction;
            }
        }
        else
        {
            // Se não há obstáculos, segue o jogador
            moveDirection = direction;
        }

        if (anim != null)
        {
            anim.SetBool("isMoving", true);
        }

        AdjustSpriteDirection(moveDirection.x);
    }

    private void Patrol()
    {
        if (movingRight)
        {
            moveDirection = Vector2.right;
            if (transform.position.x >= startPosition.x + patrolDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            moveDirection = Vector2.left;
            if (transform.position.x <= startPosition.x - patrolDistance)
            {
                movingRight = true;
            }
        }

        if (anim != null)
        {
            anim.SetBool("isMoving", true);
        }

        AdjustSpriteDirection(movingRight ? 1 : -1);
        AvoidObstacles();
    }

    private void AvoidObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, 1f, obstacleLayer);
        if (hit.collider != null)
        {
            Debug.Log($"Obstáculo detectado: {hit.collider.gameObject.name}, ajustando movimento!");

            moveDirection += new Vector2(0, 0.5f); // Ajuste para contorno
        }
    }

    private void AdjustSpriteDirection(float directionX)
    {
        if (directionX != 0)
        {
            transform.localScale = new Vector3(directionX > 0 ? 1 : -1, 1, 1);
        }
    }
}