using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool OwnedByPlayer = false;
    private int damage;

    public Color colorVal;
    private SpriteRenderer renderer;
    private Rigidbody2D m_rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bullet start");
        renderer = GetComponent<SpriteRenderer>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();

    }

    public void Awake()
    {
        Debug.Log("Bullet awake");
        if (renderer == null)
            renderer = GetComponent<SpriteRenderer>();

        renderer.color = colorVal;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Colliding with tag " + col.collider.gameObject.tag);
        Debug.Log("Bullet color val:  " + colorVal.ToString());

        Color collidedObjColor = col.gameObject.GetComponent<SpriteRenderer>().color;

        Debug.Log("Collided object's color: " + collidedObjColor);
        //check for collision w/ shield tag - then check colors

        if (OwnedByPlayer)
        {
            if (col.collider.gameObject.tag == "Enemy")
            {
                //deal damage to enemy
                col.collider.gameObject.SendMessage("LoseHealth", damage);
                Destroy(gameObject);
            }
        }
        else
        {
            if (col.collider.gameObject.tag == "PlayerBody")
            {
                //deal damage to player
                col.collider.gameObject.SendMessage("LoseHealth", damage);
                Destroy(gameObject);
            }

            if (col.collider.gameObject.tag == "Shield" && colorVal != collidedObjColor)
            {
                Debug.Log("Shield and bullet not the same color");
                Destroy(gameObject);
            }

        }

        //check for shield collision
        if (col.collider.gameObject.tag == "Shield" && colorVal == collidedObjColor)
        {
            Debug.Log("Deflecting the shot");
            OwnedByPlayer = true;
            Debug.Log("Deflect velocity: " + -m_rigidbody2D.velocity);
            m_rigidbody2D.AddForce(Vector3.Reflect(Vector3.back * m_rigidbody2D.velocity.magnitude, col.contacts[0].normal), ForceMode2D.Impulse);
        }
        else
        {
            Debug.Log("Objects are not the same color");
        }



    }

    public void SetBulletColor(Color c)
    {
        Debug.Log("Setting bullet color");

        //update sprite renderer
        renderer.color = c;

        colorVal = renderer.color;
    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
