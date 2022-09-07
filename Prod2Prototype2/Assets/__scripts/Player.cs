using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bullet;

    public Transform muzzle;

    [SerializeField] private float _bulletForce = 5f;
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        // https://answers.unity.com/questions/760900/how-can-i-rotate-a-gameobject-around-z-axis-to-fac.html
        // Get Angle in Radians
        float AngleRad = Mathf.Atan2(ray.origin.y, ray.origin.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        if (Input.GetButtonDown("Fire1"))
        {
            // Instantiate bullet
            GameObject currBullet = Instantiate(bullet, muzzle.position, transform.rotation);
            // Get direction from player to mouse
            Vector3 dir = (ray.origin - transform.position).normalized;
            currBullet.GetComponent<Rigidbody2D>().AddForce(dir * _bulletForce);
        }
    }
}
