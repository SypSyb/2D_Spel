using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	public float speed;
	public float jump;
	public GameObject rayOrigin;
	public float rayCheckDistance;
	Rigidbody2D rb;
    public Animator anim;
    public GameObject characterImage;
  
  

	void Start () {
		rb = GetComponent <Rigidbody2D> ();
	}
		
	void FixedUpdate () {
		float x = Input.GetAxis ("Horizontal");
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin.transform.position, Vector2.down, rayCheckDistance);
        if (Input.GetButton ("CrossButton") && hit.collider != null) {

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


        rb.velocity = new Vector3 (x * speed, rb.velocity.y, 0);

        if(Input.GetAxis("Horizontal") > 0)
        {
            anim.SetBool("isWalking", true);
            characterImage.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("isWalking", true);
            characterImage.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            anim.SetBool("isWalking", false);
            characterImage.transform.localScale = new Vector3(1, 1, 1);
        }

	}
}