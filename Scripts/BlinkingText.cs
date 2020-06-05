using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//class for making text to blink

public class BlinkingText : MonoBehaviour
{
	// Start is called before the first frame update
	public Text text;
	private float timer;
	public float slower;
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{


		timer = timer + Time.deltaTime;
		if (timer <= 0.1 + slower) {

			text.enabled = true;
		}

		else if (timer > 0.1 + slower & timer < 0.2 + slower) {
			text.enabled = false;
		}

		else if (timer >= 0.2 + slower) {
			text.enabled = true;
			timer = 0f;
		}
	}
}
