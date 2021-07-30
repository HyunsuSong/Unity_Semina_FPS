using UnityEngine;
using System.Collections;

/**
 *	Rapidly sets a light on/off.
 *	
 *	(c) 2015, Jean Moreno
**/

[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
	public float time = 0.05f;
	
	private float timer;
	
	void Start ()
	{
		timer = 0.0f;
	}

    private void Update()
    {
		timer += Time.deltaTime;

		if(timer >= time)
        {
			GetComponent<Light>().enabled = !GetComponent<Light>().enabled;

			timer = 0.0f;
		}
    }
}
