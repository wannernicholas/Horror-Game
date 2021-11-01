using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsPlayer : MonoBehaviour
{
	public AudioClip creeckingFloorboard;
    public AudioClip bookGrab;
    public AudioClip bookPlace;
    public AudioClip monsterGrowl;
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
        if(!audioSrc.isPlaying){
    	   audioSrc.PlayOneShot(creeckingFloorboard,0.85f);
        }
    }
    public void MonsterGrowl()
    {
        StartCoroutine(WaitASec());
    }
    IEnumerator WaitASec()
    {
        if(!audioSrc.isPlaying){
            audioSrc.clip = monsterGrowl;
            audioSrc.volume = .35f;
           audioSrc.Play();
           
        }
        yield return new WaitForSeconds(Random.Range(10f,16f));

    }
}
