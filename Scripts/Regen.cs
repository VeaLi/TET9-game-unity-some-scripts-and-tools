using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this class is for updating regeneration rate text in the left corner

public class Regen : MonoBehaviour
{
	// Start is called before the first frame update
	public Text regen;


	public void SetText(float HP)
	{
		regen.text = "  +" + (System.Math.Round((HP / 2f), 4)).ToString() + " hp/sec";



	}
}
