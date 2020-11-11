using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    private int health;
    private float shootTime = 1.5f;
    private float shootTimeCounter;
    public float bulletSpeed;
    public GameObject bulletLaunchPoint;
    public GameObject bulletPrefab;

    private PlayerController playerRef;
    private SpriteRenderer renderer;

    private Color colorVar = Color.green;
    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindObjectOfType<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimeCounter += Time.deltaTime;

        if (shootTimeCounter >= shootTime)
        {
            ShootBullet();
            shootTimeCounter = 0.0f;
        }
    }

    public void LoseHealth(int healthLoss)
    {
        health -= healthLoss;

        if(health <= 0)
            Destroy(gameObject);
    }

    public void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = colorVar;
    }

    public void ShootBullet()
    {
        //shoots bullet in direction of player
        Vector2 bulletDir = playerRef.gameObject.transform.position - transform.position;

        bulletDir *= bulletSpeed;

        GameObject newBullet = Instantiate(bulletPrefab, bulletLaunchPoint.transform.position,
            bulletLaunchPoint.transform.rotation);

        newBullet.GetComponent<Bullet>().SetBulletColor(colorVar);

        newBullet.GetComponent<Rigidbody2D>().AddForce(bulletDir, ForceMode2D.Impulse);

    }

    
}
