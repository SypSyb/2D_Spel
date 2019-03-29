using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public GameObject explosion;



    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            Instantiate(explosion, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    public void Damage()
    {
        health--;
        Debug.Log("Hit");
    }
}
