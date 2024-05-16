using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public float fireRate = 2f; // Fire rate
    public float FireSpeed = 1000;
    public GameObject bulletPrefab; // Bullet prefab
    public GameObject weaponShootPoint; // The point where bullets are instantiated
    public CharacterHealth characterHealth; // Add this line

    private float nextFire; // The time to fire next

    // Use this for initialization
    void Start () {

        characterHealth = GameObject.Find("Player").GetComponent<CharacterHealth>();
        nextFire = 1f; // Initialize with 0
    }

    // Update is called once per frame
    void Update () {
        if (Time.time > nextFire && characterHealth.health > 0) // It is time to shoot 
        {
            Shoot(); // Call shoot function 
            nextFire = Time.time + 1/fireRate; // Calculate next fire time
        }
    }

    void Shoot() {
        GameObject bullet = Instantiate(bulletPrefab, weaponShootPoint.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.right*FireSpeed);
    }

}
