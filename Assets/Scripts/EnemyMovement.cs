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
    [SerializeField] private GameObject damageCheck;
    [SerializeField] private GameObject timeText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 5f)
        {
            isMove = true;
        }
        else
            isMove = false;
        Move();
        TryAttack();


        //if (Input.GetKey(KeyCode.E))
        //{
        //    SlowMotionOn();
        //}
        //else
        //{
        //    SlowMotionOff();
        //}
    }
    public void Move()
    {
        if (isMove)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            Vector2 targetPosition = (Vector2)transform.position + direction * speed * Time.deltaTime;
            if(isMove&&!isAttack)
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
        if (distanceToPlayer < 2f)
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
        timeText.SetActive(true);
    }
    public void SlowMotionOff()
    {
        Time.timeScale = 1f;
        player.GetComponent<Animator>().speed = 1;
        timeText.SetActive(false);
    }

    public void DamageCheckController()
    {
        if (!damageCheck.gameObject.activeSelf)
        {
            damageCheck.gameObject.SetActive(true);
        }
        else
        {
            damageCheck.gameObject.SetActive(false);
        }

    }
}
