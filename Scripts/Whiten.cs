using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//this class is for whitenning (making it more red), according to player's current condition

public class Whiten : MonoBehaviour
{
    // Start is called before the first frame update
    public Image playerCondition;
    public Gradient gradient;



    public void SetCondition(float N) {
        float a = (101f - N) / 100f;



        if (a > 0.5f) {
            a = 0.5f;
        }
        //print(a.ToString());

        //print(a.ToString());

        playerCondition.color  = new Color(playerCondition.color.r, playerCondition.color.g, playerCondition.color.b, a);




    }


}
