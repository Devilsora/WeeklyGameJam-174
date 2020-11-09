using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float verticalInput;
    private float horizontalInput;

    private Rigidbody2D m_rigidBody;

    public float speed = 5f;
    public int health = 10;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse(90f);   
        GetKeyboardInput();
    }

    public void LookAtMouse(float offset)
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg) - offset;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void GetKeyboardInput()
    {
        //moves in 

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");

        m_rigidBody.velocity = new Vector2(horizontalInput, verticalInput) * speed;
    }

    public void LoseHealth(int healthLoss)
    {
        health -= healthLoss;

        if (healthLoss <= 0)
        {
            Destroy(gameObject);
        }
            


    }
}
