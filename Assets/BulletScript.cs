﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour

{
    public float speed;
    public AudioSource shootingSound;
    public GameObject explosion;
    public Enemy enemy;


    public GameObject enemyObject = null;
    private float distance;
    private float minimumDistance = 20f;


    public void Awake()
    {
        //shootingSound.Play();

        if (this.gameObject.tag == "TraceBullet")
        {
            //StartCoroutine(DestroyTrace());
        }
        else if(this.gameObject.tag == "HeavyBullet")
        {
           StartCoroutine(DestroyHeavy());
        }
        else
        {
            StartCoroutine(Destroy());
        }

        

    }

    // Update is called once per frame
    void Update()
    {




        Physics2D.IgnoreLayerCollision(10, 10);
        
        if (this.gameObject.tag == "TraceBullet")
        {
            enemyObject = GameObject.FindWithTag("Enemy");
            float step = speed * Time.deltaTime;


            Vector3 vectorToTarget = enemyObject.transform.position - transform.position;
            float moveDistance = speed * Time.deltaTime;
            if (vectorToTarget.magnitude > moveDistance)
            {
                transform.position += vectorToTarget.normalized * moveDistance;
            }
            else
            {
                transform.position = enemyObject.transform.position;
            }



            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemyObject.transform.position - transform.position), 10 * Time.deltaTime).normalized;

            transform.Translate(Vector3.right * speed * Time.deltaTime);
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

            Debug.DrawRay(transform.position, enemyObject.transform.position - transform.position);
                
                
                
                
            

        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

        
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
    public IEnumerator DestroyHeavy()
    {
        yield return new WaitForSeconds(.5f);
        Destroy(this.gameObject);
    }
    public IEnumerator DestroyTrace()
    {
        yield return new WaitForSeconds(10);
        Destroy(this.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {


       Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            enemy.Damage();
            Instantiate(explosion, this.transform.position, this.transform.rotation);
           Destroy(this.gameObject);
        }
    }
}
