using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpType type;
    public float timeToDestroy = 10;
    public AudioClip explosionSound;
    public GameObject PowerUpEffect;

    void Start()
    {
        Destroy(gameObject, timeToDestroy);
        Instantiate(PowerUpEffect, transform.position, transform.rotation);
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();
            p.ActivatePower(type);
            Destroy(gameObject);
        }
        GameManager.instance3.PlaySFX(explosionSound);
        Instantiate(PowerUpEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

public enum PowerUpType
{
    Bullets,
    Shield,
    DoubleDamage,
    Speed,
    Life
}
