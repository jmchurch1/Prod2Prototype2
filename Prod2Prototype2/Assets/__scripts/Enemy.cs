using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 10;
    [SerializeField] private float _speed = 5f;

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (gameObject.transform.position - new Vector3(0, 0, 0)).normalized;
        gameObject.transform.position -= dir * _speed * Time.deltaTime;
        // if the health of the enemy is less than 0 destroy it
        if (_health <= 0)
        {
            // remove enemy from list of enemies (also increment kill counter)
            ScoreCounter.ScoreInstance.RemoveEnemy(gameObject);
            Destroy(gameObject);
        }
    }

    public void DecrementHealth(float damage)
    {
        _health -= damage;
    }
}
