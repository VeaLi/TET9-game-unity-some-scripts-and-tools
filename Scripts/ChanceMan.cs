using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this class id for displaing the blue apple on the minimap, more player collects, higer chances to see the blue apple and angry birds

public class ChanceMan : MonoBehaviour
{
	// Start is called before the first frame update
	public Image man;
	private float timer;
	private GameObject obj;
	private int collected;

    public GameObject Grid;
	public  int intNeedToCollect = 1;

	void Start()
	{
        man.enabled = false;
        obj = GameObject.FindWithTag("LetItBePlayer_tag");
        intNeedToCollect += Grid.GetComponent<GridBehaviour>().FRUITs;
	}

	// Update is called once per frame
	void Update()
	{
        
        collected = obj.GetComponent<PlayerAtts>().collected;

		timer = timer + Time.deltaTime;
		if (timer <= 0.1) {

			if (1 == Random.Range(0, intNeedToCollect-collected)) {
				man.enabled = true;
			}
		}

		else if (timer > 0.1 & timer < 0.2) {
			man.enabled = false;
		}

		else if (timer >= 0.2) {
			if (1 == Random.Range(0, intNeedToCollect-collected)) {
				man.enabled = true;
			}
			timer = 0f;
		}
	}
}
