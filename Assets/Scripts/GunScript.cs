using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public Transform shootingPoint;

    bool isFiring;
    public BulletScript bullet;
    bool canShoot = true;
 






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
        Instantiate(bullet, shootingPoint.position, shootingPoint.rotation);
        yield return new WaitForSeconds(.25f);
        canShoot = true;
    }




}


