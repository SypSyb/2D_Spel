using UnityEngine;
using System.Collections;

public class TargetMovementVertical : MonoBehaviour
{
    public int maxSpeed;
    public int maxLength;
    public float maxHeight;

    private Vector3 startPosition;

    // Use this for initialization
    void Start()
    {
        maxSpeed = 3;
        

        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveVertical();
        transform.Translate(maxSpeed * Time.deltaTime, 0, 0);

    }

    void MoveVertical()
    {
         transform.position = new Vector3(transform.position.x, startPosition.y + Mathf.Sin(Time.time * 3) * maxHeight, transform.position.z);
        if (transform.position.x > 3)
        {
            maxSpeed = -3;
            transform.localScale = new Vector3(5.15f, 5.15f, 5.15f);


        }
       else if (transform.position.x < -3)
        {
            maxSpeed = 3;
            transform.localScale = new Vector3(-5.15f, 5.15f, 5.15f);
        }



    }


}