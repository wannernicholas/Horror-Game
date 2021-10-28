using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPickup : MonoBehaviour
{
    // Start is called before the first frame update
    float originalY;

	public float amplitude = 0.5f;
    public float frequency = 1f;
    public float rainbowSpeed = 15f;
    private Outline outline;
    private float hue;
    private float sat;
    private float bri;

    void Start()
    {
        this.originalY = this.transform.position.y;
        outline = this.GetComponent<Outline>();
    }

    void Update()
    {
    	Vector3 temp = this.transform.position;
        temp.y = originalY +Mathf.Sin ( Time.fixedTime *Mathf.PI * frequency) * amplitude;
    	this.transform.position = temp;
    	Color.RGBToHSV(outline.OutlineColor, out hue, out sat, out bri);
    	hue+= rainbowSpeed / 10000;
    	if(hue >=1){
    		hue = 0;
    	}
    	sat = 1;
    	bri = 1;
    	outline.OutlineColor = Color.HSVToRGB(hue,sat,bri);
    	outline.OutlineWidth = 10+ Mathf.Sin ( Time.fixedTime *Mathf.PI * frequency*1.6f) * amplitude*10;
    }

}
