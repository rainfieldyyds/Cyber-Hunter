using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{

    public int damage = 10;


    private SpriteRenderer enemySprite;

    private BoxCollider2D box;


    private Vector2 originalOffset;



    void Start()
    {

        box = GetComponent<BoxCollider2D>();


        enemySprite =
        GetComponentInParent<SpriteRenderer>();


        //记录默认攻击方向
        originalOffset = box.offset;

    }



    void Update()
    {

        FlipHitBox();

    }



    void FlipHitBox()
    {

        if(enemySprite.flipX)
        {

            //面向右
            box.offset =
            new Vector2(
                -originalOffset.x,
                originalOffset.y
            );

        }
        else
        {

            //面向左
            box.offset =
            originalOffset;

        }

    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.CompareTag("Player"))
        {

            PlayerHealth hp =
            other.GetComponent<PlayerHealth>();


            if(hp != null)
            {
                hp.TakeDamage(damage);
            }

        }

    }

}