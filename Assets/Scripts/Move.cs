using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Move : MonoBehaviour {

	public float speed;
	public float jump;
	public GameObject rayOrigin;
	public float rayCheckDistance;
	Rigidbody2D rb;
    public Animator anim;
    public GameObject characterImage;
    public GameObject hand;
    private IEnumerator coroutine;
    public Renderer characterRend;
    public Renderer handRend;
    public bool canDamaged;
    public Text lifesText;
    public Text coinsText;
    public int lifes;
    public int coins;
    public float thrust;
    public float horizontalMove;
    public float dieSpeed;
    public GameObject dieScreen;
    public GunScript gunScript;
    public Explosion explosionScript;
    public GameObject shootingPoint;
    public GameObject eventSystem;
    public GameObject eventSystemDead;
    GameObject portal;
    bool isDead;
   public GameObject fadeIn;
    public GameObject fadeOut;
   public GameObject startPoint;

    void Start () {
        portal = GameObject.FindGameObjectWithTag("Portal");
        startPoint = GameObject.FindGameObjectWithTag("BeginPortal");

        eventSystemDead.SetActive(false);
        eventSystem.SetActive(true);
        rb = GetComponent <Rigidbody2D> ();
        canDamaged = true;
        dieScreen.SetActive(false);
        gunScript = hand.GetComponent<GunScript>();
        isDead = false;
        portal = null;

        this.transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, 0);

        fadeIn.SetActive(false);

        StartCoroutine(Begin(1.7f));

        

    }
		
	void FixedUpdate () {
        portal = GameObject.FindGameObjectWithTag("Portal");
        startPoint = GameObject.FindGameObjectWithTag("BeginPortal");

        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(GameObject.FindObjectOfType<Canvas>());
        DontDestroyOnLoad(GameObject.FindObjectOfType<Camera>());
        DontDestroyOnLoad(eventSystem);
        DontDestroyOnLoad(eventSystemDead);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("BeginPortal"));

        Physics2D.IgnoreLayerCollision(9, 10);

        lifesText.text = "Lifes: " + lifes;
        coinsText.text = "Coins: " + coins;
        
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayCheckDistance);
        if (Input.GetButton ("CrossButton") && hit.collider != null || Input.GetButton("Jump") && hit.collider != null && lifes > 0)
        {

            rb.AddForce(Vector2.up * jump, ForceMode2D.Impulse);

        }

        if (hit.collider != null)
        {
            
            anim.SetBool("isJumping", false);

        }
        else
        {
            if (hit.collider == null)
            {
                anim.SetBool("isJumping", true);
            }
        }

        if(Input.GetButton("SquareButton"))
        {
            gunScript.Shoot();
        }

        rb.velocity = new Vector3 (horizontalMove * speed, rb.velocity.y, 0);

        if(hand.transform.localScale.x < 0 && Input.GetAxis("Vertical") < -.5f)
        {
            hand.transform.localRotation = Quaternion.Euler(0, 0, -90);
            shootingPoint.transform.localRotation = Quaternion.Euler(0, 0,180);
        }
        else if(Input.GetAxis("Vertical") < -.5f)
        {
            hand.transform.localRotation = Quaternion.Euler(0, 0, 90);
            
        }
        else
        {
            
            hand.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if(Input.GetAxis("Horizontal") > 0.2f && lifes > 0 && isDead == false)
        {
            anim.SetBool("isWalking", true);
            characterImage.transform.localScale = new Vector3(1, 1, 1);
            hand.transform.localScale = new Vector3(1, 1, 1);
            hand.transform.localPosition = new Vector3(-.083f, -0.029f, -0.022f);
            shootingPoint.transform.localRotation = Quaternion.Euler(0, 0, 0);
            horizontalMove = speed;
        }
        else if(Input.GetAxis("Horizontal") < -0.2f && lifes > 0 && isDead == false)
        {
            anim.SetBool("isWalking", true);
            characterImage.transform.localScale = new Vector3(-1, 1, 1);
            hand.transform.localScale = new Vector3(-1, 1, 1);
            hand.transform.localPosition = new Vector3(.083f, -0.029f, -0.022f);
            shootingPoint.transform.localRotation = Quaternion.Euler(0, 0, 180);
            horizontalMove = -speed;

        }
        else
        {
            anim.SetBool("isWalking", false);
            horizontalMove = 0;
        }

        

        if(lifes == 0)
        {
            canDamaged = false;
            rb.gravityScale = -dieSpeed;
            anim.SetBool("isDeath", true);
            hand.SetActive(false);
            dieScreen.SetActive(true);
            isDead = true;
            eventSystem.SetActive(false);
            eventSystemDead.SetActive(true);
        }

        if(Input.GetButton("LeftJoystickPress") && isDead == false)
        {
            Time.timeScale = 0.3f;
            gunScript.ChangePitchToHalf();
            explosionScript.ChangePitchToHalf();
        }
        else
        {
            Time.timeScale = 1f;
            gunScript.ChangePitchToFull();
            explosionScript.ChangePitchToFull();
        }

	}

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy" && canDamaged == true && lifes > 1)
        {
            StartCoroutine(damage());
            lifes--;
            rb.AddForce(transform.up * thrust * 100);
        }
        else if(other.tag == "Enemy" && canDamaged == true)
        {
            lifes--;
        }

        if(other.tag == "Portal")
        {
            StartCoroutine(End(2));
            portal = other.gameObject;

        }


    }

    public IEnumerator End(float time)
    {
        
        Vector3 originalScale = this.transform.localScale;
        Vector3 destinationScale = new Vector3(.5f, .5f, .5f);

        transform.position = Vector3.Lerp(transform.position, portal.transform.position, Time.deltaTime);


        eventSystem.SetActive(false);
        eventSystemDead.SetActive(true);

        //transform.position = new Vector3(portal.transform.position.x, portal.transform.position.y, 0);
        isDead = true;
        rb.gravityScale = 0;
        

        canDamaged = false;

        anim.SetBool("isFinished", true);

        float currentTime = 0;
        isDead = true;
        hand.SetActive(false);
        

        do
        {

            this.transform.localScale = Vector3.Lerp(originalScale, destinationScale, currentTime / 10 * 5);
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);

        fadeIn.SetActive(true);
        characterRend.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        yield return new WaitForSeconds(.1f);
        StartCoroutine(Begin(1.5f));
    }

    public IEnumerator Begin(float time)
    {
        portal = GameObject.FindGameObjectWithTag("Portal");


        yield return new WaitForSeconds(.1f);
        this.transform.position = new Vector3(startPoint.transform.position.x, startPoint.transform.position.y, 0);

        characterRend.GetComponent<SpriteRenderer>().enabled = true;
        Vector3 originalScale = new Vector3(6, 6, 6);
        Vector3 destinationScale = new Vector3(.5f, .5f, .5f);
        rb.gravityScale = 0;
        eventSystem.SetActive(false);
        eventSystemDead.SetActive(true);
        anim.SetBool("isFinished", true);
        isDead = true;
        fadeIn.SetActive(false);
        fadeOut.SetActive(false);

        fadeOut.SetActive(true);

        float currentTime = 0;
        do
        {

            this.transform.localScale = Vector3.Lerp(destinationScale, originalScale, currentTime / 10 * 5);
            currentTime += Time.deltaTime;
            yield return null;
        }
        while (currentTime <= time);
        rb.gravityScale = 2;
        isDead = false;
        eventSystem.SetActive(true);
        eventSystemDead.SetActive(false);
        anim.SetBool("isFinished", false);
    }

    public IEnumerator damage()
    {
        canDamaged = false;
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 255, 255, 1);
        handRend.material.color = new Color32(255, 255, 255, 1);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 239, 239, 255);
        handRend.material.color = new Color32(255, 239, 239, 255);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 255, 255, 1);
        handRend.material.color = new Color32(255, 255, 255, 1);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 239, 239, 255);
        handRend.material.color = new Color32(255, 239, 239, 255);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 255, 255, 1);
        handRend.material.color = new Color32(255, 255, 255, 1);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 239, 239, 255);
        handRend.material.color = new Color32(255, 239, 239, 255);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 255, 255, 1);
        handRend.material.color = new Color32(255, 255, 255, 1);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 239, 239, 255);
        handRend.material.color = new Color32(255, 239, 239, 255);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 255, 255, 1);
        handRend.material.color = new Color32(255, 255, 255, 1);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 239, 239, 255);
        handRend.material.color = new Color32(255, 239, 239, 255);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 255, 255, 1);
        handRend.material.color = new Color32(255, 255, 255, 1);
        yield return new WaitForSeconds(.1f);
        characterRend.material.color = new Color32(255, 239, 239, 255);
        handRend.material.color = new Color32(255, 239, 239, 255);
        canDamaged = true;
    }

    


}