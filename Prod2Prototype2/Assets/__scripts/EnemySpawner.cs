using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    
    [SerializeField] private float _spawnRate = 4f;
    [SerializeField] private float _enemyAmount = 5f;
    
    private Vector3 viewportPos;
    
    // Start is called before the first frame update
    void Start()
    {
        viewportPos = Camera.main.WorldToViewportPoint(new Vector3(0, 0, 0));

        StartCoroutine("SpawnEnemies");
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(_spawnRate);
        // get random point outside the viewport
        float x = Random.Range(-10f, -5f);
        float y = Random.Range(-10f, -5f);
        
        // randomize side
        if (Random.value >= 0.5f)
            x *= -1;
        if (Random.value > -0.5f)
            y *= -1;
        
        for (int i = 0; i < _enemyAmount; i++)
        {
            float randOffset1 = Random.Range(-2, 2);
            float randOffset2 = Random.Range(-2, 2);
            Instantiate(enemy, new Vector3(x + randOffset1, y + randOffset2, 0f), Quaternion.identity);
        }
        
        // recall spawn enemies
        StartCoroutine("SpawnEnemies");
    }
}
