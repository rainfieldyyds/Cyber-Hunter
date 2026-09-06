using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossAI : MonoBehaviour
{

    public Transform player;
    public float jumpPower = 60f;

    public float walkSpeed = 2f;

    public float stayAttackTime = 3f;

public float backJumpDistance = 3f;

public float backJumpSpeed = 8f;
public bool dead;


float attackStayTimer;

bool backJumping;
public float leftLimit;
public float rightLimit;

Vector3 backJumpTarget;
    //距离
    public float attackDistance = 3f;
    public float farDistance = 8f;
    public float jumpDistance = 12f;

    
    public float actionCooldown = 2f;
    enum BossMoveState
{
    Idle,
    Walk
}


BossMoveState moveState;
float moveTimer;
float moveDuration;

    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sr;


    bool attacking = false;


    Vector2 jumpTarget;



    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }



    void Update()
    {
        if(dead)
        return;
        
        if(player == null)
            return;


        //朝向玩家
        if(!backJumping)
{
    FacePlayer();
}



        float distance =
            Vector2.Distance(transform.position, player.position);



        //近距离停留计时
if(distance <= attackDistance)
{
    attackStayTimer += Time.deltaTime;


    if(attackStayTimer >= stayAttackTime && !backJumping)
    {
        StartCoroutine(BackJump());
        return;
    }
}
else
{
    attackStayTimer = 0;
}



//正常AI
if(!attacking)
{


    if(distance > jumpDistance)
    {
        StartCoroutine(JumpAttack());
    }


    else if(distance > attackDistance)
    {
        RandomMove();
    }


    else
    {
        RandomAttack();
    }

}

    }





    //====================
    // Idle Walk随机
    //====================

    void RandomMove()
{

    if(moveTimer <= 0)
    {

        int r = Random.Range(0,100);


        if(r < 50)
        {
            moveState = BossMoveState.Idle;
        }
        else
        {
            moveState = BossMoveState.Walk;
        }


        //这个状态持续时间
        moveDuration =
        Random.Range(1.5f,2f);


        moveTimer = moveDuration;

    }



    moveTimer -= Time.deltaTime;



    switch(moveState)
    {


        case BossMoveState.Idle:
        anim.SetFloat("Speed",0);
        break;
        case BossMoveState.Walk:
        anim.SetFloat("Speed",1);
        float dir =sr.flipX ? 1 : -1;
        transform.position +=Vector3.right *dir *walkSpeed *Time.deltaTime;
        break;

    }

}
    //====================
    //攻击随机
    //====================

    void RandomAttack()
    {

        attacking=true;


        int r=Random.Range(0,100);


        if(r<30)
        {
            anim.SetTrigger("Attack");
        }


        else if(r<60)
        {
            anim.SetTrigger("HeavyAttack");
        }


        else
        {
            StartCoroutine(JumpAttack());
        }


        Invoke(nameof(ResetAttack),actionCooldown);

    }




    void ResetAttack()
    {
        attacking=false;
    }



    //====================
    //跳跃攻击
    //====================

    IEnumerator JumpAttack()
{

    attacking=true;


    jumpTarget=player.position;


    anim.SetTrigger("JumpAttack");


    yield return new WaitForSeconds(actionCooldown);


    attacking=false;

}
    //动画事件调用
    public void Jump()
    {
       Vector2 dir =(jumpTarget - (Vector2)transform.position).normalized;
        rb.velocity =new Vector2(dir.x*jumpPower,jumpPower);
    }
    //====================
    //朝向
    //====================

    void FacePlayer()
    {


        if(player.position.x >
        transform.position.x)
        {

            //玩家在右边

            sr.flipX=true;

        }

        else
        {

            //玩家在左边

            sr.flipX=false;

        }


    }

IEnumerator BackJump()
{

    backJumping = true;
    attacking = true;


    attackStayTimer = 0;



    //播放动画
    anim.SetTrigger("BackJump");



    //记录当前方向
    float dir;


    if(sr.flipX)
    {
        //面向右
        dir = -1;
    }
    else
    {
        //面向左
        dir = 1;
    }



    //计算后方目标点

    backJumpTarget =
    transform.position +
    Vector3.right *
    dir *
    backJumpDistance;

    backJumpTarget.x =
Mathf.Clamp(
    backJumpTarget.x,
    leftLimit,
    rightLimit
);



    float timer = 0;


    while(timer < 1)
    {


        transform.position =
        Vector3.MoveTowards(
            transform.position,
            backJumpTarget,
            backJumpSpeed *
            Time.deltaTime
        );


        timer += Time.deltaTime;


        yield return null;

    }


    backJumping=false;
    attacking=false;

}
public HitBox hitBox;


public void EnableHitBox()
{
    hitBox.EnableHitBox();
}


public void DisableHitBox()
{
    hitBox.DisableHitBox();
}

}