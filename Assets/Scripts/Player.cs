using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletPrefab;
    private const float maxX = 3.6f;
    private const float minX = -3.6f;

    private SpriteRenderer sp;
    public Sprite shipLeft, shipRight, shipCustom;

    //private float speed = 3f;
    private bool isShooting;
    //private float cooldown = 0.5f;
    [SerializeField] private ObjectPool objectPool = null;
    public ShipStats shipStats;
    private Vector2 offScreenPos = new Vector2(0, -20f);
    private Vector2 startPos = new Vector2(0, -6.5f);
    private float dirx;

    private void Start()
    {
        shipStats.currentHealth = shipStats.maxHealth;
        shipStats.currentLifes = shipStats.maxLifes;
        transform.position = startPos;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
        UIManager.UpdateLives(shipStats.currentLifes);
        sp = GetComponent<SpriteRenderer>();
        sp.sprite = shipCustom;
        shipStats.fireRate = 0.8f;
        shipStats.shipSpeed = 3;
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.A) && transform.position.x > minX)
        {
            sp.sprite = shipLeft;
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
        }
        else if (Input.GetKey(KeyCode.D) && transform.position.x < maxX)
        {
            sp.sprite = shipRight;
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
        }
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
#endif
        dirx = Input.acceleration.x;
        //Debug.Log(dirx);
        if (dirx <= -0.1f && transform.position.x > minX)
        {
            transform.Translate(Vector2.left * Time.deltaTime * shipStats.shipSpeed);
            sp.sprite = shipLeft;
        }
        if (dirx >= 0.1f && transform.position.x < maxX)
        {
            transform.Translate(Vector2.right * Time.deltaTime * shipStats.shipSpeed);
            sp.sprite = shipRight;
        }
        if (dirx >= -0.1f && dirx <= 0.1f && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            sp.sprite = shipCustom;
        }
    }

    public void ShootButton()
    {
        if (!isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    public void AddHealth()
    {
        if (shipStats.currentHealth == shipStats.maxHealth)
        {
            UIManager.UpdateScore(250);
        }
        else
        {
            shipStats.currentHealth++;
            UIManager.UpdateHealthBar(shipStats.currentHealth);
        }
    }

    public void AddLife()
    {
        if (shipStats.currentLifes == shipStats.maxLifes)
        {
            UIManager.UpdateScore(1000);
        }
        else
        {
            shipStats.currentLifes++;
            UIManager.UpdateLives(shipStats.currentLifes);
        }
    }


    private IEnumerator Shoot()
    {
        isShooting = true;
        //Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        GameObject obj = objectPool.GetPooledObject();
        obj.transform.position = gameObject.transform.position;
        yield return new WaitForSeconds(shipStats.fireRate);
        isShooting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player Hit!..");
            collision.gameObject.SetActive(false);
            TakeDamage();
        }
    }

    private IEnumerator Respawn()
    {
        transform.position = offScreenPos;

        yield return new WaitForSeconds(2);

        shipStats.currentHealth = shipStats.maxHealth;

        transform.position = startPos;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
    }

    public void TakeDamage()
    {
        shipStats.currentHealth--;
        UIManager.UpdateHealthBar(shipStats.currentHealth);
        if (shipStats.currentHealth <= 0)
        {
            shipStats.currentLifes--;
            UIManager.UpdateLives(shipStats.currentLifes);
            if (shipStats.currentLifes <= 0)
            {
                Debug.Log("Game Over");
            }
            else
            {
                //Debug.Log("Respawn");
                StartCoroutine(Respawn());
            }
        }

    }
}
