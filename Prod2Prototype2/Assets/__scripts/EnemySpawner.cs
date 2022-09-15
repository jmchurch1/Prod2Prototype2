using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;

    private GameObject _player;
    private Camera _mainCamera;
    
    [SerializeField] private float _spawnRate = 4f;
    [SerializeField] private float _enemyAmount = 5f;
    
    private Vector3 _viewportPos;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        _mainCamera = Camera.main;
        _viewportPos = _mainCamera.WorldToViewportPoint(_player.transform.position);

        InvokeRepeating("SpawnEnemies",0, _spawnRate);
    }

    private void Update()
    {
        _viewportPos = _mainCamera.WorldToViewportPoint(_player.transform.position);
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void SpawnEnemies()
    {
        // get positions outside height and width of viewport
        float x = _mainCamera.orthographicSize + 5;
        float y = _mainCamera.orthographicSize * _mainCamera.aspect + 5;
        
        // randomize side
        if (Random.value >= 0.5f)
            x *= -1;
        if (Random.value >= 0.5f)
            y *= -1;
        
        for (int i = 0; i < _enemyAmount; i++)
        {
            float randOffset1 = Random.Range(-2, 2);
            float randOffset2 = Random.Range(-2, 2);
            GameObject currEnemy = Instantiate(enemy, _viewportPos + new Vector3(x + randOffset1, y + randOffset2, 0f), Quaternion.identity);
            
            // add enemy to list of enemies
            ScoreCounter.ScoreInstance.AddEnemy(currEnemy);
        }
    }
}
