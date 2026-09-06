using UnityEngine;

public class HitBox : MonoBehaviour
{

    public int damage = 10;

    public SpriteRenderer bossSprite;

    BoxCollider2D box;

    Vector2 originalOffset;

    bool hit;



    void Start()
    {

        box = GetComponent<BoxCollider2D>();

        originalOffset = box.offset;


        bossSprite =
        GetComponentInParent<SpriteRenderer>();


        box.enabled=false;

    }



    public void EnableHitBox()
    {

        hit=false;


        if(bossSprite.flipX)
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


        box.enabled=true;

    }



    public void DisableHitBox()
    {
        box.enabled=false;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {

        if(hit)
            return;


        if(other.CompareTag("Player"))
        {

            hit=true;


            PlayerHealth hp =
            other.GetComponent<PlayerHealth>();


            if(hp != null)
            {
                hp.TakeDamage(damage);
            }

        }

    }

}