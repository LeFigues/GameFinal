using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform target;
    public float speed = 1;
    public float hp = 50;
    public float dropChance = 30;
    public float damage = 5;
    public List<PowerUp> powerUpList = new List<PowerUp>();

    public GameObject explosionEnemy;
    public AudioClip explosionSound;

    void Start()
    {
        speed = Random.Range(0.5f, 3.0f);
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Rotation();
        Movement();
    }

    void Rotation()
    {
        Vector3 dir = target.position - transform.position;
        float angleY = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg - 0;
        transform.rotation = Quaternion.Euler(0, angleY, 0);
    }
    void Movement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void TakeDamage(float damagetaken)
    {
        hp -= damagetaken;
        if(hp <= 0)
        {
            hp = 0;
            Destroyer();
        }
    }

    void Destroyer()
    {
        int chance = Random.Range(0, 50);
        if(chance >= dropChance)
        {
            Instantiate(powerUpList[Random.Range(0, powerUpList.Count)],
                transform.position, transform.rotation);
        }
        Instantiate(explosionEnemy, transform.position, transform.rotation);
        GameManager.instance2.PlaySFX(explosionSound);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.TakeDamage(damage);
        }
    }
}
