using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnFogBalls : MonoBehaviour
{
    public GameObject ball;
    
    [SerializeField] private int _amountBalls = 30;
    [SerializeField] private float _spawnAreaRadius = 5;

    // Start is called before the first frame update
    void Start()
    {
        float _distanceBetween = ball.gameObject.transform.localScale.x + .05f;
        float _radiusBalls = _distanceBetween/2f;
        float halfBallDistance = (_amountBalls / 2f) * _distanceBetween;
        Vector3 startPosition = new Vector3(-halfBallDistance, -halfBallDistance, 0f);

        for (int i = 0; i < _amountBalls; i++)
        {
            for (int j = 0; j < _amountBalls; j++)
            {
                Vector3 currPosition = startPosition + new Vector3(_distanceBetween * i, _distanceBetween * j, 0f);
                GameObject currBall = Instantiate(ball, currPosition, quaternion.identity, gameObject.transform);
                
                // check whether the currBall is in the designated spawn area 
                if (Vector3.Distance(currBall.transform.position, new Vector3(0, 0, 0)) < _spawnAreaRadius)
                {
                    Destroy(currBall);
                }
                else
                {
                    currBall.AddComponent<Collider2D>();
                    currBall.AddComponent<Rigidbody2D>().gravityScale = 0;
                }
            }
        }
    }
}