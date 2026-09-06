using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;   // 按住Shift时的速度
    [Header("Heavy Attack")]
public float holdTime = 0.5f;

private bool isHoldingAttack = false;

private bool isAttacking = false;
public bool canControl=true;
private bool isSprinting;
    [Header("Attack")]
    public float heavyAttackHoldTime = 0.5f;
    

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private float move;
    

    private bool isBlocking;
    public bool isDead;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
{
    if(!canControl)
    return;

    if(isDead)
    {
        return;
    }


    Move();

    Attack();

    Block();

}

    void Move()
{
    // 防御时不能移动
    if (isBlocking)
    {
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetFloat("Speed", 0);
        return;
    }

    move = Input.GetAxisRaw("Horizontal");

    // Shift 加速
    float currentSpeed = moveSpeed;

    isSprinting = false;


if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
{
    currentSpeed = sprintSpeed;

    isSprinting = true;
}

    rb.velocity = new Vector2(move * currentSpeed, rb.velocity.y);

    animator.SetFloat("Speed", Mathf.Abs(move));
    animator.SetBool("IsSprint", isSprinting);

    if (move > 0)
        sr.flipX = false;
    else if (move < 0)
        sr.flipX = true;
}

    void Attack()
{
    if (isBlocking || isAttacking)
        return;

    if (Input.GetMouseButtonDown(0))
    {
        animator.SetTrigger("Attack");

        isHoldingAttack = true;
        isAttacking = true;
    }

    if (Input.GetMouseButtonUp(0))
    {
        isHoldingAttack = false;
    }
}

    void Block()
    {
        isBlocking = Input.GetMouseButton(1);

        animator.SetBool("Block", isBlocking);
    }
    public void CheckHeavyAttack()
{
    if (isHoldingAttack)
    {
        animator.SetTrigger("HeavyAttack");
    }
}
public void EndAttack()
{
    isAttacking = false;
    isHoldingAttack = false;
}
public PlayerHitBox playerhitBox;


public void EnableHitBox()
{
    playerhitBox.EnableHitBox();
}


public void DisableHitBox()
{
    playerhitBox.DisableHitBox();
}
}