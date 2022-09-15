using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnFogBalls : MonoBehaviour
{
    public GameObject ball;

    private GameObject _player;
    private List<GameObject> _fogBalls = new List<GameObject>();
    private List<Vector3> _ballPositions = new List<Vector3>();
    
    private int _amountBalls = 30;
    private float _spawnAreaRadius = 5;
    private float _distanceBetween = 1;
    private float _maxDistanceFromPlayer = 20;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        
        // Spawn the balls around the player
        SpawnBalls();
        
        //InvokeRepeating("ProceduralGeneration",0f,2f);
    }

    private void SpawnBalls()
    {
        float halfBallDistance = (_amountBalls / 2f);
        int playerPositionX = (int) (Math.Round( _player.transform.position.x/_distanceBetween) * _distanceBetween);
        int playerPositionY = (int) (Math.Round( _player.transform.position.y/_distanceBetween) * _distanceBetween);
        Vector3 startPosition = new Vector3(-halfBallDistance, -halfBallDistance, 0f) + new Vector3(playerPositionX, playerPositionY, 0f);

        for (int i = 0; i < _amountBalls; i++)
        {
            for (int j = 0; j < _amountBalls; j++)
            {
                Vector3 currPosition = startPosition + new Vector3(i * _distanceBetween, j * _distanceBetween, 0f);
                // check if the balls position is currently being used or if it is in spawn radius
                if (_ballPositions.Contains(currPosition) || Vector3.Distance(currPosition, _player.transform.position) < _spawnAreaRadius)
                    continue;
                
                GameObject currBall = Instantiate(ball, currPosition, quaternion.identity, gameObject.transform);
                _fogBalls.Add(currBall);
                _ballPositions.Add(currBall.transform.position);
            }
        }
    }

    private void Update()
    {
        // loop through balls, checking whether they are too far away or not
        
        
        // this will be like the least efficient way possible of doing this
        for (int i = _fogBalls.Count - 1; i >= 0; i--)
        {
            GameObject currBall = _fogBalls[i];
            if (Vector3.Distance(currBall.GetComponent<StayGrounded>().GetHomePosition(), _player.transform.position) > _maxDistanceFromPlayer)
            {
                _fogBalls.Remove(currBall);
                _ballPositions.Remove(currBall.transform.position);
                Destroy(currBall);
            }
        }
        
        SpawnBalls();
    }
/*
    void ProceduralGeneration()
    {
        float halfBallDistance = (_amountBalls / 2f) * _distanceBetween;
        float xPosition = Mathf.Round(_player.transform.position.x/_distanceBetween) * _distanceBetween;
        float yPosition = Mathf.Round(_player.transform.position.y/_distanceBetween) * _distanceBetween;
        Vector3 startPosition = new Vector3(-halfBallDistance + xPosition, -halfBallDistance + yPosition, 0f);
        
        for (int i = 0; i < _amountBalls; i++)
        {
            for (int j = 0; j < _amountBalls; j++)
            {
                Vector3 currPosition = startPosition + new Vector3(_distanceBetween * i, _distanceBetween * j, 0f);
                if (_ballPositions.Contains(currPosition) || Vector3.Distance(_player.transform.position,currPosition) < 1)
                    continue;

                GameObject currBall = Instantiate(ball, currPosition, quaternion.identity, gameObject.transform);
                _fogBalls.Add(currBall);
            }
        }
    }
    */
}
