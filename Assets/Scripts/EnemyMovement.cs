using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    GameObject player;
    bool isMove;
    bool isAttack;
    [SerializeField] private float speed;
    Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        TryAttack();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMove = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isMove = false;
        }

    }
    public void Move()
    {
        if (isMove)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            Vector2 targetPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
            animator.Play("Run");
            transform.position = targetPosition;
        }
        else
        {
            if (!isAttack)
            {
                animator.Play("Idle");

            }
            else
            {
                animator.Play("Attack1");
            }
        }

    }
    private void TryAttack()
    {
        float distanceToPlayer= Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 1f)
        {
            isAttack = true;
            Attack();
        }
        else
        {
            isAttack = false;
            Time.timeScale = 1f;
        }

    }
    private void Attack()
    {
        isMove = false;
        animator.Play("Attack1");
    }
    public void SlowMotionOn()
    {
        Time.timeScale = 0.1f;
        player.GetComponent<Animator>().speed *= 50;
    }
    public void SlowMotionOff()
    {
        Time.timeScale = 1f;
    }
}
