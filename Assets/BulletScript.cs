using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour

{
    public float speed;
    public AudioSource shootingSound;
    public GameObject explosion;



    public GameObject enemyObject;
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

        enemyObject = GameObject.FindWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {




        Physics2D.IgnoreLayerCollision(10, 10);
        
        if (this.gameObject.tag == "TraceBullet")
        {
            float step = speed * Time.deltaTime;

            

            transform.Translate(Vector3.right * speed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(enemyObject.transform.position - transform.position), 10 * Time.deltaTime).normalized;
            transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

            Debug.DrawRay(transform.position, enemyObject.transform.position - transform.position);
                
                
                
                
            

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
            Instantiate(explosion, this.transform.position, this.transform.rotation);
           Destroy(this.gameObject);
        }
    }
}
