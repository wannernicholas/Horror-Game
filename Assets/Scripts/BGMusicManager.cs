using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicManager : MonoBehaviour
{
	public float maxAmbientVol = .15f;
	public float maxChaseVol = .3f;
	public float volChangeRate = .001f;
    public float maxChain = .35f;

    private bool inChainRange = false;
	private bool ambientIsPlaying = true;

	public GameObject ambientHolder;
	public GameObject chaseHolder;
    public GameObject chainHolder;
    private AudioSource chainPlayer;

	private AudioSource ambientPlayer;
	private AudioSource chasePlayer;
    // Start is called before the first frame update
    void Start()
    {
        ambientPlayer= ambientHolder.GetComponent<AudioSource>();
        chasePlayer= chaseHolder.GetComponent<AudioSource>();
        chainPlayer= chainHolder.GetComponent<AudioSource>();
        ambientIsPlaying = true;
        inChainRange = false;
        chainPlayer.volume = 0.0f;
        ambientPlayer.volume = maxAmbientVol;
        chasePlayer.volume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ambientIsPlaying){
        	// increase volume of ambient to max and decrease chase to 0
        	StartCoroutine(fadeChaseOut());
        	 
        }
        else{
        	//decrease ambient to 0 and increase chase to max 
        	StartCoroutine(fadeChaseIn());
        }
        if(inChainRange){
            StartCoroutine(fadeChainsIn());
        }
        else{
            StartCoroutine(fadeChainsOut());
        }
     
    }

    IEnumerator fadeChainsIn()
    {
        float temp;
        if(chainPlayer.volume < maxChain){
            temp = chainPlayer.volume + volChangeRate;
            chainPlayer.volume = temp; //* Time.fixedTime;
        }
        
        yield return new WaitForSeconds(0.1f);

    }

    IEnumerator fadeChainsOut()
    {
        float temp;
        if(chainPlayer.volume > 0){
            temp = chainPlayer.volume - volChangeRate;
            chainPlayer.volume = temp; //* Time.fixedTime;
        }
        
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator fadeChaseOut()
    {
        float temp;
        if(ambientPlayer.volume < maxAmbientVol){
            temp = ambientPlayer.volume + volChangeRate;
            ambientPlayer.volume = temp; //* Time.fixedTime;
        }
        
        if (chasePlayer.volume > 0){
            temp = chasePlayer.volume - volChangeRate;
            chasePlayer.volume = temp; //* Time.fixedTime;
        }
        yield return new WaitForSeconds(0.1f);

    }

    IEnumerator fadeChaseIn()
    {
        float temp;
        if(ambientPlayer.volume > 0){
            temp = ambientPlayer.volume - volChangeRate;
            ambientPlayer.volume = temp; //* Time.fixedTime;
        }
        if (chasePlayer.volume < maxAmbientVol){
            temp = chasePlayer.volume + volChangeRate;
            chasePlayer.volume = temp; //* Time.fixedTime;
        }
        yield return new WaitForSeconds(0.1f);
    }
    //when player is now in range to be close to monster
    public void SwitchToChase()
    {
    	ambientIsPlaying = false;
    }

    //When player is no longer in range
    public void SwitchToAmbient()
    {
    	ambientIsPlaying = true;
    }
    public void DisableChains()
    {
        inChainRange = false;
    }

    //When player is no longer in range
    public void EnableChains()
    {
        inChainRange = true;
    }
}
