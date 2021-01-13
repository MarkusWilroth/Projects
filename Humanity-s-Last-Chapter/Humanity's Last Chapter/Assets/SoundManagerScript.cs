using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip pistolSound, shotgunSound, ARSound, rifleSound, deathSound, pickupSound, clickSound;
    private static AudioSource pistolSrc, shotgunSrc, ARSrc, rifleSrc, deathSrc, pickupSrc, clickSrc;

    // Start is called before the first frame update
    void Start()
    {
        pistolSound = Resources.Load<AudioClip>("pistol");
        shotgunSound = Resources.Load<AudioClip>("shotgun");
        ARSound = Resources.Load<AudioClip>("AR");
        rifleSound = Resources.Load<AudioClip>("rifle");
        deathSound = Resources.Load<AudioClip>("fart");
        pickupSound = Resources.Load<AudioClip>("pickup");
        clickSound = Resources.Load<AudioClip>("click");

        AudioSource[] audios = GetComponents<AudioSource>();
        pistolSrc = audios[0];
        shotgunSrc = audios[1];
        ARSrc = audios[2];
        rifleSrc = audios[3];
        deathSrc = audios[4];
        pickupSrc = audios[5];
        clickSrc = audios[6];
    }

    // Update is called once per frame
    void Update()
    {
        //For sound testing

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    pistolSrc.PlayOneShot(pistolSound);
        //}
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    shotgunSrc.PlayOneShot(shotgunSound);
        //}
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    ARSrc.PlayOneShot(ARSound);
        //}
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    rifleSrc.PlayOneShot(rifleSound);
        //}
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    deathSrc.PlayOneShot(deathSound);
        //}
        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    pickupSrc.PlayOneShot(pickupSound);
        //}
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    clickSrc.PlayOneShot(clickSound);
        //}
    }

    public static void PlaySound(string clip)
    {
        if (clip == "pistol")
        {
            pistolSrc.PlayOneShot(pistolSound);
        }
        if (clip == "shotgun")
        {
            shotgunSrc.PlayOneShot(shotgunSound);
        }
        if (clip == "AR")
        {
            ARSrc.PlayOneShot(ARSound);
        }
        if (clip == "rifle")
        {
            rifleSrc.PlayOneShot(rifleSound);
        }
        if (clip == "death")
        {
            deathSrc.PlayOneShot(deathSound);
        }
        if (clip == "pickup")
        {
            pickupSrc.PlayOneShot(pickupSound);
        }
        if (clip == "click")
        {
            clickSrc.PlayOneShot(clickSound);
        }
    }
}
