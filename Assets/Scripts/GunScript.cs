using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Transform shootingPoint;

    bool isFiring;
    public BulletScript bullet;
    public BulletScript traceBullet;
    public BulletScript Heavybullet;
    bool canShoot = true;

    int index = 1;
    int bulletChoose;


    public AudioSource Clips;

    private void Awake()
    {

    }




    public void Update()
    { 

         if(Input.GetButtonDown("TriangleButton"))
        {
            index++;
            if(index == 4)
            {
                index = 1;
            }
        }

    }




    public void Shoot()
    {
        if(canShoot)
        {
            canShoot = false;
            StartCoroutine("Shooting");
        }
        

        
    }

    public void ChangePitchToHalf()
    {
        bullet.shootingSound.pitch = .3f;
    }

    public void ChangePitchToFull()
    {
        bullet.shootingSound.pitch = 1;
    }

    public IEnumerator Shooting()
    {

       Clips.Play();

        switch(index)
        {
            case 1:
                Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
                break;
            case 2:
                Instantiate(traceBullet, shootingPoint.position, shootingPoint.rotation);
                break;
            case 3:
                Instantiate(Heavybullet, shootingPoint.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, 22.5f));
                Instantiate(Heavybullet, shootingPoint.position, shootingPoint.rotation);
                Instantiate(Heavybullet, shootingPoint.position, Quaternion.Euler(transform.rotation.x, transform.rotation.y, -22.5f));
                break;


        }
        
        yield return new WaitForSeconds(.25f);
        canShoot = true;
    }




}


