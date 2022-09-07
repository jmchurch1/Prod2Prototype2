using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{
    [SerializeField] private float _bulletDamage = 2f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // check layer of the object with a trigger collider
        if (other.gameObject.layer == 3)
        {
            // remove health from enemy
            other.gameObject.GetComponent<Enemy>().DecrementHealth(_bulletDamage);
            // destroy bullet
            Destroy(gameObject);
        }
    }
}
