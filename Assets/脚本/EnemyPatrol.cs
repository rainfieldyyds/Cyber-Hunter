using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("移动")]
    public float moveSpeed = 2f;

    [Header("巡逻距离")]
    public float patrolDistance = 3f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // 出生位置
    private float startX;

    // true=向右  false=向左
    private bool movingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        // 记录出生位置
        startX = transform.position.x;
    }

    void FixedUpdate()
    {
        Patrol();
    }

    void Patrol()
    {
        if (movingRight)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            sr.flipX = true;

            // 到达右边界
            if (transform.position.x >= startX + patrolDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            sr.flipX = false;

            // 到达左边界
            if (transform.position.x <= startX - patrolDistance)
            {
                movingRight = true;
            }
        }
    }
}
