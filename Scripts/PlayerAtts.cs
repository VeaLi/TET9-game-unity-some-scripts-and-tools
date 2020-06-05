using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//context player attributes and methods

public class PlayerAtts : MonoBehaviour
{

	public float maxHealth = 100f;
	public float currentHealth;
	public int collected = 0;

	public HealthBar healthBar;
	public GameObject apple;
	public GameObject NOTAPPLE; // that is  not an apple!!!!!!!!!1


	public AudioSource audioSource;
	public AudioClip clip;
	public float volume = 0.5f;

	public Counter counter;
	public Regen regen;
	public Whiten condition;

	public Image HideMinimap;
	public float targetTime = 3.0f;
	public float noise = 1.0f;

	public LevelChanger changer;
	public GameObject self;
	public AudioSource audioSource_ouch;
	public bool isShiftKeyDown;







	// Start is called before the first frame update




	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.name.Contains("apple")) {

			apple = GameObject.Find("/Grid/" + other.gameObject.name);

			audioSource.pitch = Random.Range(1, 20)/10f;//(audioSource.pitch+Random.Range(1, 20)/10f)/2f;
			audioSource.PlayOneShot(audioSource.clip, volume);
			self.GetComponent<WalkForMe>().speed = 2f;

			Destroy(apple);
			collected += 1;
			float RESTORE = Random.Range(0, 4);
			counter.SetText(collected, System.Math.Round(targetTime, 1), RESTORE);
			HideMinimap.color  = new Color(HideMinimap.color.r, HideMinimap.color.g, HideMinimap.color.b, 0.1f);
			targetTime += (0.7f + collected / 140f);

			if (currentHealth < 100f) {
				currentHealth += RESTORE;
				currentHealth = Mathf.Min(currentHealth, 100f);
				healthBar.SetHealth(currentHealth);
				condition.SetCondition(currentHealth);

			}

		}


	}

	void OnTriggerEnter(Collider other) {

		if (other.gameObject.tag == "Snake")
		{
			//Debug.Log("Snaaaaaake!!!");
			condition.SetCondition(1f);
			NOTAPPLE = GameObject.Find(other.gameObject.name);
			float DAMAGE  = 19f + Random.Range(-19+NOTAPPLE.GetComponent<QSnake>().RANGE, 5+NOTAPPLE.GetComponent<QSnake>().RANGE);
			counter.SetText(collected, System.Math.Round(targetTime, 1), DAMAGE * -1f);
			TakeDamage(DAMAGE);
			if (!audioSource_ouch.isPlaying) {
				audioSource_ouch.Play();
			}

			
			NOTAPPLE.GetComponent<QSnake>().shouldReset = true;
			//print("assigned?");
		}
	}


	void Start()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	// Update is called once per frame
	void Update()
	{
        
		isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);


		targetTime -= Time.deltaTime;
		noise -= Time.deltaTime;




		//if (targetTime<=2f){
		//	self.GetComponent<WalkForMe>().speed = 1.4f;
		//}

		if (targetTime <= 0.0f) {
			HideMinimap.color  = new Color(HideMinimap.color.r, HideMinimap.color.g, HideMinimap.color.b, 0.65f);

			if (!isShiftKeyDown) {
				self.GetComponent<WalkForMe>().speed = 1.2f;
			}

			targetTime = 0f;
		}

		if (noise <= 0.0f) {
			HideMinimap.transform.Rotate(new Vector3(0.0f, 0.0f, 90f));
			noise = 1f;
		}
		//if (Input.GetKeyDown(KeyCode.Space))
		//{
		//	TakeDamage(20);
		//}
	}

	void TakeDamage(float damage)
	{
		currentHealth -= damage;

		healthBar.SetHealth(currentHealth);

		if (currentHealth <= 0f) {

			changer.FadeToLevel("End");
		}

		condition.SetCondition(currentHealth);
	}

	void FixedUpdate() {
		counter.SetText(collected, System.Math.Round(targetTime, 1), 0);
		currentHealth += collected / 4700f;
		regen.SetText(collected / 4700f);
		currentHealth = Mathf.Min(currentHealth, 100f);
		healthBar.SetHealth(currentHealth);
		condition.SetCondition(currentHealth);

		if (isShiftKeyDown) {
			TakeDamage(0.05f);
			self.GetComponent<WalkForMe>().speed = 2.5f;
			counter.SetText(collected, System.Math.Round(targetTime, 1), -0.05f);
		}



	}
}