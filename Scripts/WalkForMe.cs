using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


//class for making it possible to move the player
//also triggers the messages accrording to player's position

public class WalkForMe : MonoBehaviour
{


    public float speed = 1.1f; // one cell?
    public Rigidbody rb;
    Vector3 move;

    //public player position
    public float libX = 0;
    public float libY = 0;
    public AudioSource steps;
    public bool isMoving = false;
    public Vector3 oldPosition;
    public Text pressE;
    public Text goalText;

    public GameObject Grid;
    private  int intNeedToCollect = 0;
    public GameObject Player;
    private int collected;
    public LevelChanger changer;


    void Start()
    {
        intNeedToCollect = Grid.GetComponent<GridBehaviour>().FRUITs;


    }

    // Update is called once per frame
    void Update()
    {
        //for input

        //-1 for a and 1 for d
        collected = Player.GetComponent<PlayerAtts>().collected;
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");


        move = transform.right * x + transform.forward * z;
        //print(Mathf.Floor(rb.position.x).ToString()+ "__" + Mathf.Floor(rb.position.z).ToString());

        float tx = rb.position.x;
        float tz = rb.position.z;

        if (tx <= 1.76f & tz <= 1.1f) {
            pressE.enabled = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (collected >= intNeedToCollect) {
                    changer.FadeToLevel("MadeIt");
                }
                else {
                    pressE.enabled = false;
                    goalText.text = "Not yet! Firstly I need " + (intNeedToCollect - collected).ToString() + " fruits to collect";
                    goalText.enabled = true;

                }
            }

        }
        else {
            pressE.enabled = false;
            goalText.enabled = false;
            if (collected >= intNeedToCollect) {
                goalText.text = "You have had enough. Escape!";
                goalText.enabled = true;
            }
        }


        libX = Mathf.Round(tx);
        libY = Mathf.Round(tz);//z is y in 3d unity
        //print(libY.ToString()+"_"+libX.ToString());







    }

    // 50 times a second default
    void FixedUpdate()
    {
        //for movement

        rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);

        if (rb.position != oldPosition) {
            isMoving = true;
            oldPosition = rb.position;

        } else {
            isMoving = false;
        }

        if (isMoving) {
            if (!steps.isPlaying) {
                steps.Play();
                //print("isMoving");
            }
        }
        else {
            steps.Stop();
        }
        rb.velocity = Vector3.zero;//that can be it)
    }

}
