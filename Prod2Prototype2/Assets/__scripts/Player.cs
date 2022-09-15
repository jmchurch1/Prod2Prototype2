using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;

    public Transform muzzle;

    public AudioSource bulletSound;

    [SerializeField] private float _shotWaitTime = .1f;
    private bool _waitingShot = false;
    
    
    

    private float _bulletForce = 500f;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // https://answers.unity.com/questions/760900/how-can-i-rotate-a-gameobject-around-z-axis-to-fac.html
        Vector3 currPlayerPos = gameObject.transform.position;
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(ray.origin.y - currPlayerPos.y, ray.origin.x - currPlayerPos.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        if (Input.GetButton("Fire1"))
        {
            if (!_waitingShot)
                StartCoroutine("Shoot", ray);
        }
    }

    private IEnumerator Shoot(Ray ray)
    {
        _waitingShot = true;
        yield return new WaitForSeconds(_shotWaitTime);
        // play bullet shot sound
        bulletSound.Play();
        // Instantiate bullet
        GameObject currBullet = Instantiate(bullet, muzzle.position, transform.rotation);
        // Get direction from player to mouse
        Vector3 dir = (ray.origin - transform.position).normalized;
        currBullet.GetComponent<Rigidbody2D>().AddForce(dir * _bulletForce);
        // reset waiting shot bool
        _waitingShot = false;
    }
}
