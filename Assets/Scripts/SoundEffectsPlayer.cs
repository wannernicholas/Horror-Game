using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
	public AudioClip creeckingFloorboard;
    public AudioClip bookGrab;
    public AudioClip bookPlace;
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void GrabBook()
    {
    	audioSrc.PlayOneShot(bookGrab,0.55f);
    }
    public void PlaceBook()
    {
    	audioSrc.PlayOneShot(bookPlace,0.55f);
    }
    public void CreeckFloor()
    {
    	audioSrc.PlayOneShot(creeckingFloorboard,0.85f);
    }
}
