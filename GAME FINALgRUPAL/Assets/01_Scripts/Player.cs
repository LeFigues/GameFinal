using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public float speed = 2;
    public Rigidbody rb;
    [Header("Disparo")]
    public Bullet bulletPrefab;
    public Transform firePoint;
    bool canShoot = true;
    float timer = 0;
    public float timeBtwShoot = 0.5f;
    public float damage = 10;
    public float bulletSpeed = 7;
    public int bulletsCount = 20;
    public GameObject PlayerEffects;
    public float hp = 500;
    public Text hpTxt;
    public Text bulletsTxt;
    public AudioClip PlayerSound;

    void Start()
    {
        hpTxt.text=hp.ToString();
        bulletsTxt.text = bulletsCount.ToString();
       
    }

    void Update()
    {
        Rotation();
        Movement();
        CheckIfCanShoot();
        Shoot();
    }

    public void TakeDamage(float damagetaken)
    {
        hp -= damagetaken;
        if(hp <= 0)
        {
            hp = 0;
            SceneManager.LoadScene("OverMenu");
            Debug.Log("Game Over");
        }
        Instantiate(PlayerEffects, transform.position, transform.rotation);
        GameManager.instance2.PlaySFX(PlayerSound);
        hpTxt.text=hp.ToString();
    }


    void Rotation()
    {
        Vector3 dir = Input.mousePosition - new Vector3(Screen.width/2, Screen.height/2, 0);
        float angleY = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
        rb.rotation = Quaternion.Euler(0, -angleY, 0);
    }

    void Movement()
    {
        Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.velocity = dir * speed;
    }

    void CheckIfCanShoot()
    {
        if (!canShoot)
        {
            timer += Time.deltaTime;
            if(timer >= timeBtwShoot)
            {
                timer = 0;
                canShoot = true;
            }
        }
    }

    void Shoot()
    {
        if (canShoot && bulletsCount > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Bullet b = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                b.damage = damage;
                b.speed = bulletSpeed;
                canShoot = false;
                bulletsCount--;
                bulletsTxt.text = bulletsCount.ToString();
            }
        }
    }

    public void ActivatePower(PowerUpType power)
    {
        Debug.Log("poder " + power.ToString());
        switch (power)
        {
            case PowerUpType.Bullets:
                bulletsCount += 5;
                bulletsTxt.text = bulletsCount.ToString();
                break;
            case PowerUpType.DoubleDamage:
                Damage();
                break;
            case PowerUpType.Speed:
                Speed();
                break;
            case PowerUpType.Shield:
                break;
            case PowerUpType.Life:
                PowerUpLife();
                break;
        }
    }
    
    void Damage()
    {
        damage += 5;
        if(damage >80)
        {
            damage = 80;
        }
    }
    void Speed()
    {
        speed += 1;
        if(speed > 7)
        {
            speed = 7;
        }
    }
    void PowerUpLife()
    {
        hp += 20;
        if (hp > 100)
        {
           hp = 100;
        }
        hpTxt.text = hp.ToString();
    }
}
