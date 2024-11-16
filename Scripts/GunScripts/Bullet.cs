using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3;
    void Awake()
    {
        print("Bullet!");
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        print("Bullet collided with: " + collision.gameObject.name);
        // Destroy the object hit by the bullet
        // Make sure to check what you want to destroy based on tags or other criteria
        // For example, destroy only if it hits an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("Bullet hit an enemy!");
            Destroy(collision.gameObject);
        }

        // Destroy the bullet itself
        Destroy(gameObject);
    }
}
