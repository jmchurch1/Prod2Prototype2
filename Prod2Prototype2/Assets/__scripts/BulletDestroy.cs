using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDestroy : MonoBehaviour
{

    [SerializeField] private float _bulletLifespan = 7f;
    private void Awake()
    {
        StartCoroutine("Kill");
    }

    IEnumerator Kill()
    {
        yield return new WaitForSeconds(_bulletLifespan);
        Destroy(gameObject);
    }
}
