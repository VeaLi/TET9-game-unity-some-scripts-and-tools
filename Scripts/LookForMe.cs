using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//code for making possible look with a mouse + code to make the camera move slightly in time while standing,
//and more if running

public class LookForMe: MonoBehaviour
{

    public float MouseSensitivity = 120f;


    float yRotation = 0;

    public Transform playerBody;
    public float thres = 0.5f;
    private bool flip = true;
    public bool isMoving;
    public GameObject obj;
    public int del = 7;


    // Start is called before the first frame update
    void Start()
    {   obj = GameObject.FindWithTag("LetItBePlayer_tag");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()

    {
        isMoving = obj.GetComponent<WalkForMe>().isMoving;

        float MouseX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float MouseY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;


        if (1 == Random.Range(0, 3)) {

            if (flip == true) {thres -= Time.deltaTime * 0.7f;}
            else if (flip == false) {thres += Time.deltaTime * 0.7f;}

            if (thres < 0 & flip == true) {thres = -0.5f; flip = false;}

            else if (thres > 0 & flip == false) {thres = 0.5f; flip = true;}


            if (isMoving == false) {
                del = 3;
                yRotation -= (MouseY + thres / del);
            }

            else if (isMoving == true) {
                del = 2;
                yRotation -= (MouseY + thres / del);
            }
        }
        else if (1 != Random.Range(0, 4)) {
            yRotation -= MouseY;
        }
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);
        //print(thres);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * MouseX);
        //transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }
}