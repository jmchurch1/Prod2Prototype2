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
    
    [SerializeField] private int _amountBalls = 30;
    [SerializeField] private float _spawnAreaRadius = 5;
    private float _distanceBetween = .6f;
    private float _maxDistanceFromPlayer = 20;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player");
        
        float halfBallDistance = (_amountBalls / 2f) * _distanceBetween;
        Vector3 startPosition = new Vector3(-halfBallDistance, -halfBallDistance, 0f);

        for (int i = 0; i < _amountBalls; i++)
        {
            for (int j = 0; j < _amountBalls; j++)
            {
                Vector3 currPosition = startPosition + new Vector3(i * _distanceBetween, j * _distanceBetween, 0f);
                GameObject currBall = Instantiate(ball, currPosition, quaternion.identity, gameObject.transform);
                _fogBalls.Add(currBall);
                // check whether the currBall is in the designated spawn area 
                if (Vector3.Distance(currBall.transform.position, new Vector3(0, 0, 0)) < _spawnAreaRadius)
                {
                    _fogBalls.Remove(currBall);
                    Destroy(currBall);
                }
            }
        }
    }

    private void Update()
    {
        // this will be like the least efficient way possible of doing this
        for (int i = _fogBalls.Count - 1; i >= 0; i--)
        {
            GameObject currBall = _fogBalls[i];
            if (Vector3.Distance(currBall.GetComponent<StayGrounded>().GetHomePosition(), _player.transform.position) > _maxDistanceFromPlayer)
            {
                _fogBalls.Remove(currBall);
                Destroy(currBall);
            }
        }
        
        float halfBallDistance = (_amountBalls / 2f) * _distanceBetween;
        float xPosition = Mathf.Round(_player.transform.position.x/_distanceBetween) * _distanceBetween;
        float yPosition = Mathf.Round(_player.transform.position.y/_distanceBetween) * _distanceBetween;
        Vector3 startPosition = new Vector3(-halfBallDistance + xPosition, -halfBallDistance + yPosition, 0f);
        
        for (int i = 0; i < _amountBalls; i++)
        {
            for (int j = 0; j < _amountBalls; j++)
            {
                Vector3 currPosition = startPosition + new Vector3(_distanceBetween * i, _distanceBetween * j, 0f);
                bool goNext = false;
                foreach (GameObject tempBall in _fogBalls)
                {
                    if (tempBall.GetComponent<StayGrounded>().GetHomePosition() == currPosition);
                    goNext = true;
                    break;
                }
                
                if (goNext) continue;
                
                GameObject currBall = Instantiate(ball, currPosition, quaternion.identity, gameObject.transform);
                _fogBalls.Add(currBall);
            }
        }
    }
}
