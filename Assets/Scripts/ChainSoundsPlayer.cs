using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainSoundsPlayer : MonoBehaviour
{
	public float maxChain = .35f;
	//public float maxChaseVol = .3f;
	public float volChangeRate = .001f;

	private bool inRange = false;

	//public GameObject ambientHolder;
	public GameObject chainHolder;

	private AudioSource chainPlayer;
	//private AudioSource chasePlayer;
    // Start is called before the first frame update
    void Start()
    {
        chainPlayer= chainHolder.GetComponent<AudioSource>();
       //chasePlayer= chaseHolder.GetComponent<AudioSource>();
        inRange = false;
        chainPlayer.volume = 0.0f;
        //chasePlayer.volume = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inRange){
        	// increase volume of ambient to max and decrease chase to 0
        	StartCoroutine(fadeChainsOut());
        	 
        }
        else{
        	//decrease ambient to 0 and increase chase to max 
        	StartCoroutine(fadeChainsIn());
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
    //when player is now in range to be close to monster
    public void DisableChains()
    {
    	inRange = false;
    }

    //When player is no longer in range
    public void EnableChains()
    {
    	inRange = true;
    }
}
