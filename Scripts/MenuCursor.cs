using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this helps
//https://answers.unity.com/questions/1484585/how-to-disable-mouse-lock-script-when-returning-to.html

// this for making curor visible in the menu
public class MenuCursor : MonoBehaviour {

    // Use this for initialization
    void Start () {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update () {

    }
}