using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpawnFogBalls : MonoBehaviour
{
    public GameObject ball;
    
    [SerializeField] private int _amountBalls = 30;

    // Start is called before the first frame update
    void Start()
    {
        float _distanceBetween = ball.gameObject.transform.localScale.x + .2f;
        float _radiusBalls = _distanceBetween/2f;
        float halfBallDistance = (_amountBalls / 2f) * _distanceBetween;
        Vector3 startPosition = new Vector3(-halfBallDistance, -halfBallDistance, 0f);

        for (int i = 0; i < _amountBalls; i++)
        {
            for (int j = 0; j < _amountBalls; j++)
            {
                Vector3 currPosition = startPosition + new Vector3(_distanceBetween * i, _distanceBetween * j, 0f);
                GameObject currBall = Instantiate(ball, currPosition, quaternion.identity, gameObject.transform);
                currBall.AddComponent<Collider2D>();
                currBall.AddComponent<Rigidbody2D>().gravityScale = 0;
            }
        }
    }
}
