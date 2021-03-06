﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public AudioSource explosionSound;

    // Start is called before the first frame update
    void Awake()
    {
        explosionSound.Play();
        StartCoroutine(ExplosionDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePitchToHalf()
    {
        explosionSound.pitch = .3f;
    }

    public void ChangePitchToFull()
    {
        explosionSound.pitch = 1;
    }

    public IEnumerator ExplosionDestroy()
    {
        yield return new WaitForSeconds(0.6f);
        Destroy(this.gameObject);
    }
}
