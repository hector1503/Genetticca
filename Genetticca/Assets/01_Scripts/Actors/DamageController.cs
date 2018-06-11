using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

    public Color damageColor;//EEA1A1FF
    public float timeDuration= 0.25f;

    Color normalColor;
    float finalTime;

    //public CheckPoint[] CheckPointsList;
    Material material;

    // Use this for initialization
    void OnEnable() {
        material = GetComponentInChildren<Renderer>().material;
        normalColor = material.color;
        GetComponentInChildren<Renderer>().material.color = damageColor;
        finalTime = Time.time + timeDuration;
      
       
    }
	
	// Update is called once per frame
	void Update () {
        if (finalTime < Time.time)
        {
            material.color = normalColor;
            this.enabled=false;
        }
		
	}

  


}
