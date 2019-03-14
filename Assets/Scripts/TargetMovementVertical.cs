using UnityEngine;
using System.Collections;

public class TargetMovementVertical : MonoBehaviour
{
    public int maxSpeed;
    public int maxLength;
    public float maxHeight;
    public GameObject particle;

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
            particle.transform.rotation = Quaternion.Euler(0, 0, 0);


        }
       else if (transform.position.x < -3)
        {
            maxSpeed = 3;
            particle.transform.rotation = Quaternion.Euler(0, 180, 0);

        }



    }


}