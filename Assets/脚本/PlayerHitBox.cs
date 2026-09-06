using UnityEngine;
using System.Collections.Generic;

public class PlayerHitBox : MonoBehaviour
{

    public int damage = 10;


    public SpriteRenderer playerSprite;


    private BoxCollider2D box;


    private Vector2 originalOffset;


    private bool active;


    //防止一次攻击连续扣血
    private HashSet<Collider2D> hitObjects = new HashSet<Collider2D>();



    void Start()
    {

        box = GetComponent<BoxCollider2D>();

        originalOffset = box.offset;

        box.enabled = false;


        if(playerSprite == null)
        {
            playerSprite =
            GetComponentInParent<SpriteRenderer>();
        }

    }



    //动画事件调用
    public void EnableHitBox()
    {

        active=true;

        hitObjects.Clear();


        FlipHitBox();


        box.enabled=true;

    }



    //动画事件调用
    public void DisableHitBox()
    {

        active=false;

        box.enabled=false;

    }



    void FlipHitBox()
    {

        if(playerSprite.flipX)
        {

            box.offset =
            new Vector2(
                -originalOffset.x,
                originalOffset.y
            );

        }
        else
        {

            box.offset =
            originalOffset;

        }

    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if(!active)
            return;


        if(hitObjects.Contains(other))
            return;



        //普通敌人
        EnemyHealth enemy =
        other.GetComponent<EnemyHealth>();


        if(enemy != null)
        {

            hitObjects.Add(other);

            enemy.TakeDamage(damage);

            return;

        }



        //Boss
        BossHealth boss =
other.GetComponent<BossHealth>();

if(boss != null)
{
    boss.TakeDamage(damage);
}

    }

}