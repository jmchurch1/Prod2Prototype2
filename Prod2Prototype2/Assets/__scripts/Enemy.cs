using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _health = 10f;
    [SerializeField] private float _speed = 5f;
    
    private float _maxHealth;

    void Awake()
    {
        // set max health
        _maxHealth = _health;
        
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 102);
    }

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
        
        // get location with accuracy of two decimal points
        if (new Vector3( Mathf.Round(transform.position.x * 100f) / 100f,
            Mathf.Round(transform.position.x * 100f) / 100f,
            Mathf.Round(transform.position.x * 100f) / 100f) == new Vector3(0f, 0f, 0f))
        {
            Destroy(GameObject.Find("Player"));
        }
    }

    public void DecrementHealth(float damage)
    {
        _health -= damage;
        // change the color of enemy on hit
        float currPercentage = _health / _maxHealth;
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
        }
    }
}
