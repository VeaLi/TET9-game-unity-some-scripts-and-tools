using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// general player stats counter

public class Counter : MonoBehaviour
{
	// Start is called before the first frame update
	public GameObject Grid;
	public  int intNeedToCollect = 0;
	public Text cnt;
	public Text speedup;
	public Text restore;

	public LevelChanger changer;

	void Awake() {

		intNeedToCollect = Grid.GetComponent<GridBehaviour>().FRUITs;
	}


	public void SetText(int N, double SEC, float HP)
	{
		cnt.text = 'x' + N.ToString() + "/" + intNeedToCollect.ToString();
		if (HP > 0) {
			restore.text = '+' + HP.ToString(); restore.color =  Color.green;
			restore.color  = new Color(restore.color.r, restore.color.g, restore.color.b, 0.45f);
		}

		else if (HP == -0.2f) {
			restore.text = ' ' + HP.ToString(); restore.color =  Color.black;
			restore.color  = new Color(restore.color.r, restore.color.g, restore.color.b, 1f);

		} else if (HP < 0f) {
			restore.text = ' ' + HP.ToString(); restore.color =  Color.red;
			restore.color  = new Color(restore.color.r, restore.color.g, restore.color.b, 0.45f);

		}//else{restore.text = " ";}

		if (SEC > 0f & HP != -0.1f) {
			speedup.text = "You move 1.5X  faster for " + SEC.ToString();
			speedup.color =  Color.white;
			speedup.color  = new Color(speedup.color.r, speedup.color.g, speedup.color.b, 0.65f);
		}

		else if (HP == -0.1f) {
			speedup.color = Color.red;
			speedup.text = "You move 2X  faster";
		}
		else {
			speedup.text = " ";

			if (HP > 0f) {restore.text = " ";}
		}

		//if (N == intNeedToCollect) {
		//	//SceneManager.LoadScene("Menu");
		//	changer.FadeToLevel("MadeIt");
		//}


	}
}
