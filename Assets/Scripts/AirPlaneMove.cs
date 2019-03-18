using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirPlaneMove : MonoBehaviour
{

    public int lifes;
    public float speed;
    Rigidbody2D rb;
    public Renderer characterRend;
    public bool canDamaged;
    public Text lifesText;
    public Text coinsText;
    public int coins;
    public GunScript gunScript;
    public GameObject shootingPoint;
    public GameObject dieScreen;
    bool isDead;
    public float horizontalMove;
    public float verticalMove;
    public GameObject explosion;
    public AudioSource damageSound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canDamaged = true;
        dieScreen.SetActive(false);
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {

        Physics2D.IgnoreLayerCollision(9, 10);

        lifesText.text = "Lifes: " + lifes;
        coinsText.text = "Coins: " + coins;

        if (Input.GetButton("SquareButton"))
        {
            gunScript.Shoot();
        }
        rb.velocity = new Vector3(horizontalMove * speed, verticalMove * speed, 0);

        if (Input.GetAxis("Horizontal") > 0 && lifes > 0)
        {
            horizontalMove = Input.GetAxis("Horizontal") * speed;
        }
        else if (Input.GetAxis("Horizontal") < 0 && lifes > 0)
        {
            horizontalMove = Input.GetAxis("Horizontal") * speed;
        }
        else
        {
            horizontalMove = 0;
        }

        if (Input.GetAxis("Vertical") > 0 && lifes > 0)
        {
            verticalMove = -Input.GetAxis("Vertical") * speed;
        }
        else if (Input.GetAxis("Vertical") < 0 && lifes > 0)
        {
            verticalMove = -Input.GetAxis("Vertical") * speed;
        }
        else
        {
            verticalMove = 0;
        }

        if (lifes == 0)
        {
            canDamaged = false;
            dieScreen.SetActive(true);
            characterRend.material.color = new Color32(255, 255, 255, 0);
            if (isDead == false)
            {
                StartCoroutine(Die());
            }

        }

        if (Input.GetButton("LeftJoystickPress") && isDead == false)
        {
            Time.timeScale = 0.3f;
            gunScript.ChangePitchToHalf();
        }
        else
        {
            Time.timeScale = 1f;
            gunScript.ChangePitchToFull();
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" && canDamaged == true && lifes > 1)
        {
            StartCoroutine(damage());
            lifes--;
        }
        else if (other.tag == "Enemy" && canDamaged == true)
        {
            lifes--;
        }


    }




    IEnumerator Die()
        {
           
            isDead = true;
            Instantiate(explosion, transform.position, transform.rotation);
            yield return new WaitForSeconds(0);

        }

        public IEnumerator damage()
        {
            canDamaged = false;
            damageSound.Play();
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 255, 255, 1);
          
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 239, 239, 255);
   
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 255, 255, 1);
       
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 239, 239, 255);
        
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 255, 255, 1);
        
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 239, 239, 255);
       
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 255, 255, 1);
        
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 239, 239, 255);
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 255, 255, 1);
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 239, 239, 255);
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 255, 255, 1);
            yield return new WaitForSeconds(.1f);
            characterRend.material.color = new Color32(255, 239, 239, 255);
            canDamaged = true;
        }

    
}
