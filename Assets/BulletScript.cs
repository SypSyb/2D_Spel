using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour

{
    public float speed;
    public AudioSource shootingSound;
    public GameObject explosion;


    public void Awake()
    {
        shootingSound.Play();
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public IEnumerator Destroy()
    {
        yield return new WaitForSeconds(3);
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
