using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool OwnedByPlayer = false;
    private int damage;
    private Color colorVal;
    private SpriteRenderer renderer;
    private Rigidbody2D m_rigidbody2D;  
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Bullet start");
        renderer = GetComponent<SpriteRenderer>();
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        colorVal = Color.white;

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
            if (col.collider.gameObject.tag == "Player" || (col.collider.gameObject.tag == "GreenShield" && colorVal == Color.blue
                                                            || col.collider.gameObject.tag == "BlueShield" && colorVal == Color.green))
            {
                //deal damage to player
                col.collider.gameObject.SendMessage("LoseHealth", damage);
                Destroy(gameObject);
            }
        }

        //check for shield collision

        //DEIFNITELY need a better way to handle this - kind of gross
        if (col.collider.gameObject.tag == "GreenShield" && colorVal == Color.green
            || col.collider.gameObject.tag == "BlueShield" && colorVal == Color.blue)
        {
            OwnedByPlayer = true;

            m_rigidbody2D.velocity = -m_rigidbody2D.velocity;
        }

            
    }

    public void SetBulletColor(Color c)
    {
        colorVal = c;

        //update sprite renderer
        renderer.color = c;

    }
}
