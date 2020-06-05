using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//enemy (birds) attributes and methods
//it uses qtable to move

public class QSnake : MonoBehaviour
{
	// Start is called before the first frame update
	public float targetX = 0f;
	public float targetY = 0f;

	Vector3 move;
	public float speed = 1f;
	public Rigidbody rbs;

	public GameObject obj;
	public GameObject table;

	//snake personal position
	private float libXs = 0;
	private float libYs = 0;

	public Dictionary<string, string> qT = new Dictionary<string, string>();
	public Dictionary<string, int> qI = new Dictionary<string, int>();

	public float targetVal = 1f;

	public float GHostSpeed = 1f;
	public int RANGE = 4;

	private int collected = 0;

	public bool shouldReset = false;

	//public GameObject snakeCube;

	void Start()
	{
		obj = GameObject.FindWithTag("LetItBePlayer_tag");

		//LetItBeQtable_tag
		int chance = Random.Range(0, 1);
		if (chance == 0) {
			table = GameObject.FindWithTag("table2");
		}
		else if (chance == 1) {
			table = GameObject.FindWithTag("LetItBeQTable_tag");
		}


		//rbs.MovePosition(new Vector3(Random.Range(0, 32), 0, Random.Range(0, 75)));

		//GetComponent<Renderer>().material.color = Color.blue;
		//snakeCube = GameObject.Find("/Snake/SnakeCube");
		//snakeCube.renderer.material.color = Color.black;
	}

	// Update is called once per frame
	void Update()


	{



		char direction = '0';
		string res;
		float xs = 0f;
		float zs = 0f;


		string goal;

		libXs = Mathf.Round(rbs.position.x);
		libYs = Mathf.Round(rbs.position.z);

		// print("sx: "+libXs.ToString()+" sy: "+libYs.ToString());

		//libY - row
		//libX - column

		targetX = obj.GetComponent<WalkForMe>().libX;
		targetY = obj.GetComponent<WalkForMe>().libY;

		//print("tx: "+targetX.ToString()+" ty: "+targetY.ToString());

		qT = table.GetComponent<QTable>().q;
		qI = table.GetComponent<QTable>().qI;

		//print(targetY.ToString()+"_"+targetX.ToString());
		//for movement

		//x_z 0_1 up
		//0_-1 down
		//-1_0 left
		//1_0 right

		//up1 down2 left3 right4

		//print(libYs.ToString() + "_|" + libXs.ToString());

		//print("here");
		//print(qI["2_2"]);
		res = qT.TryGetValue(libYs.ToString() + "_" + libXs.ToString(), out res) ? res : "NO";;
		// print("->"+res);
		if (res != "NO") {
			goal = targetY.ToString() + "_" + targetX.ToString();
			//print("->"+qI[goal].ToString());
			direction = res[qI[goal]];
		}


		///anyway shoudl load smooth map though because there was not lats update on
		// rewards after marking paths as complete so many rewards were just as initialized and practically the same
		/*
		if (direction=='5'){
		libXs = Mathf.Floor(rbs.position.x);
		libYs = Mathf.Floor(rbs.position.z);

		res = qT[libYs.ToString() + "_" + libXs.ToString()];
		// print("->"+res);
		goal = targetY.ToString() + "_" + targetX.ToString();
		//print("->"+qI[goal].ToString());
		direction = res[qI[goal]];
		} */


		//print("->"+qI[goal].ToString()+"__"+direction);
		//print(direction);
		//up is right


		if (direction == '0') {
			xs = 0f; //wtf wtf wtf does it -1*-1 = 1 wtf wtf wtf wtf
			zs = -1f;
			speed = 1f;
		}

		if (direction == '1') {
			xs = 0f;
			zs = 1f;
			speed = 1f;
		}

		if (direction == '2') {
			xs =  -1f;
			zs = 0f;
			speed = 1f;
		}

		if (direction == '3') {
			xs = 1f;
			zs = 0f;
			speed = 1f;
		}




		//float mean = (up+down+left+right)/4f;
		//print(mean);

		if (direction == '4' || direction == '5' || res == "NO" ) { // || direction == '1' || direction == '2' || direction == '3' || direction == '0'){
			speed = 1f;

			print("STUCK or malinformed");


			float  up =  Mathf.Pow(((libYs + 1) - (targetY)), 2) + Mathf.Pow(((libXs) -  (targetX)), 2); //
			float down =  Mathf.Pow(((libYs - 1) - (targetY)), 2) + Mathf.Pow((libXs -  (targetX)), 2);
			float left =  Mathf.Pow(((libYs) - (targetY)), 2) + Mathf.Pow(((libXs - 1) -  (targetX)), 2); //
			float right =  Mathf.Pow(((libYs) - (targetY)), 2) + Mathf.Pow(((libXs + 1) -  (targetX)), 2);

			//print('u'+up.ToString()+'d'+down.ToString()+'l'+left.ToString()+'r'+right.ToString());

			float dir = 0f;
			if (up < down & up < left & up < right) {
				dir = 0f;
				//print("up");
			}

			if (down < up & down < left & down < right) {
				dir = 1f;
				//print("down");
			}

			if (left < up & left < down & left < right) {
				dir = 2f;
				//print("left");
			}

			if (right < up & right < down & right < left) {
				dir = 3f;
				//print("right");
			}


			//xs =y zs ==x
			if (dir == 0) {


				xs = 0f; //wtf wtf wtf does it -1*-1 = 1 wtf wtf wtf wtf
				zs = 1f;


			}

			if (dir == 1) {

				xs = 0f;
				zs = -1f;



			}

			if (dir == 2) {

				xs =  -1f;
				zs = 0f;


			}

			if (dir == 3) {

				xs = 1f;
				zs = 0f;



			}
		} else {
			print("Fully acknoledged!");
		}

		//if direction is undefined or stuck


		if (targetVal >= 1) {
			move = new Vector3(xs, 0, zs);
			targetVal = 0f;


		}

		//transform.right * xs + transform.forward * zs;
		//print("move: "+move.ToString());
		//print("pos: "+rbs.position.ToString());

	}


	void FixedUpdate()
	{
		if (!shouldReset) {
			if (collected != obj.GetComponent<PlayerAtts>().collected) {
				if (GHostSpeed < 2.5f & 1 == Random.Range(0, RANGE * 4)) {

					GHostSpeed = GHostSpeed + obj.GetComponent<PlayerAtts>().collected / 1000f;
					//print(GHostSpeed);
				}
				collected =  obj.GetComponent<PlayerAtts>().collected;
			}

			int chance = Random.Range(0, RANGE);
			if (chance != 1) {
				targetVal += (GHostSpeed * Time.fixedDeltaTime);

				rbs.MovePosition(rbs.position + move * Time.fixedDeltaTime * GHostSpeed);
			}
		}

		if (shouldReset & 1 == Random.Range(0, RANGE * 50)) {
			print("Moving to Florida)");
			//rbs.MovePosition(new Vector3(Random.Range(0, 32), 0, Random.Range(0, 75)));// random is so random
			if (1 == Random.Range(0, 2)) {
			rbs.MovePosition(new Vector3(3, 0, 16));
			}
			else {
				rbs.MovePosition(new Vector3(54, 0, 6));
			}
			shouldReset = false;

		}
		else {

			if (collected != obj.GetComponent<PlayerAtts>().collected) {
				if (GHostSpeed < 2.5f & 1 == Random.Range(0, RANGE * 4)) {
					GHostSpeed = GHostSpeed + obj.GetComponent<PlayerAtts>().collected / 1000f;
					print(GHostSpeed);
				}
				collected =  obj.GetComponent<PlayerAtts>().collected;
			}

			int chance = Random.Range(0, RANGE);
			if (chance != 1) {
				targetVal += (GHostSpeed * Time.fixedDeltaTime);

				rbs.MovePosition(rbs.position + move * Time.fixedDeltaTime * GHostSpeed);
			}

		}
		//print(GHostSpeed.ToString());

		//print(rbs.position);


		//rbs.velocity = Vector3.zero;//that can be it)

	}
}
