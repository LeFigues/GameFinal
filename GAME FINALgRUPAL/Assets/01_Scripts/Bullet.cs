using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 20;
    public float damage = 90;
    public float timeToDestroy = 10;
    public GameObject explosionEffect;
    public AudioClip explosionSound;
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.TakeDamage(damage);
            Destroyer();
        }
    }

    void Destroyer()
    {
        GameManager.instance.PlaySFX(explosionSound);
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}
