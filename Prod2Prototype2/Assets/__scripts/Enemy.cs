using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 0;
    [SerializeField] private float _speed = 0;

    private GameObject player;
    
    private float _maxHealth;
    private float _maxSpeed;

    void Start()
    {
        //_health = UnityEngine.Random.Range(1, 10);
        _speed = Random.Range(1, 4);

        //What Caleb added to try and have different health values at different speeds
        if (_speed == 4)
        {
            _health = Random.Range(1, 2);
        }
        else if (_speed == 3)
        {
            _health = Random.Range(3, 5);
        }
        else if (_speed == 2)
        {
            _health = Random.Range(6, 9);
        }
        else if (_speed == 1)
        {
            _health = Random.Range(10, 14);
        }

        float colliderChance = Random.Range(0, 4);
        if (colliderChance < 1)
        {
            gameObject.AddComponent<CircleCollider2D>().radius = .4f;
        }
    }
    void Awake()
    {
        player = GameObject.Find("Player");
        // set max health
        _maxHealth = _health;
        _maxSpeed = _speed;

        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 102);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = (gameObject.transform.position - player.transform.position).normalized;
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
        // change the color of enemy on hit

        //Commenting this out in case what I do doesn't work
        /*float currPercentage = _health / _maxHealth;
        if (currPercentage >= .75f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else if (currPercentage >= .5f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (currPercentage >= .25f)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 255);
        } */

        if (_health >= 9)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
        else if (_health >= 6)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (_health >= 3)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (_health >= 1)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 255);
        }
    }
}
