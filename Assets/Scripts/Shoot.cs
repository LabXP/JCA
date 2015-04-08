using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Shoot : MonoBehaviour
{
	
		public GameObject cannon;
		public GameObject roqueira;
		public GameObject bubblePosition;
		public GameObject[] bubble;
		[HideInInspector]
		public bool
				arrived = true;
		public bool hitWall = false;
		public Vector3 dir;
		public string color;
		public GameObject bubbleObject;
		public AudioClip[] shoot;
		public int level = 0;

		private Vector2 mousePos;
		private GameObject bubbleInstance;
		private Vector3 diff;
		private float rotZ = 90f;
		private int random;
		private AudioSource audio;
		private Animator anim;
		private int randomMax;
		private Animator roqueiraAnim;
		private bool shot = false;
		//private float minClamp = 85, maxClamp = 275;

		void Start ()
		{
				if (level == 1)
						randomMax = 3;
				else if (level == 2)
						randomMax = 4;
				else if (level == 3)
						randomMax = 5;
				random = UnityEngine.Random.Range (0, randomMax);
				bubbleInstance = (GameObject)UnityEngine.Object.Instantiate (bubble [random], bubblePosition.transform.position, Quaternion.identity);
				bubbleInstance.rigidbody2D.isKinematic = true;
				bubbleInstance.transform.parent = bubblePosition.transform;
				bubbleInstance.name = bubble [random].name;
				audio = GetComponent<AudioSource> ();
				anim = cannon.GetComponent<Animator> ();
				roqueiraAnim = roqueira.GetComponent<Animator> ();
			
		}

		void Update ()
		{

				if (FindObjectOfType<GameController> ().playing) {
						float h = Input.GetAxisRaw ("Horizontal");
						float mouseMovement = Input.GetAxis ("Mouse X");

						diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - cannon.transform.position;
						//normalize difference  
						diff.Normalize ();

						//calculate rotation
						if (mouseMovement != 0) {
								rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
						} else if (h != 0) {
								rotZ -= h * 100 * Time.deltaTime;
						}
						//apply to object

						//Debug.Log(rotZ);

						if (rotZ > 170) {
								rotZ = 170;
						} else if (rotZ < 10) {
								rotZ = 10;
						} else {
								cannon.transform.rotation = Quaternion.Euler (0, 0, rotZ);
						}


						if ((Input.GetMouseButtonDown (0) || Input.GetKeyDown ("space")) && arrived) {
				
								arrived = false;
								color = bubble [random].name;
								bubbleObject = bubbleInstance;
								PlaySound ();
								anim.SetTrigger ("Shoot");
								roqueiraAnim.SetTrigger ("Play");

								bubbleObject.transform.position = cannon.transform.position;
								bubbleObject.rigidbody2D.isKinematic = false;
								bubbleObject.rigidbody2D.velocity = cannon.transform.right * 20;
								bubbleObject.transform.parent = null;
					

								shot = true;
							
								

			
								/*mousePos = Input.mousePosition;
			Vector2 sp = Camera.main.WorldToScreenPoint(cannon.transform.position);
			dir = (mousePos - sp).normalized;*/

						}

						if (arrived && shot) {
								BubbleRandom ();
								bubbleInstance = (GameObject)Instantiate (bubble [random], bubblePosition.transform.position, Quaternion.identity);
								bubbleInstance.name = bubble [random].name;

								bubbleInstance.rigidbody2D.isKinematic = true;
								shot = false;
						}
				}
		
		}

		void BubbleRandom ()
		{
				List<string> colors = FindObjectOfType<GameController> ().CheckIfColor ();
				//for (int i = 0; i < colors.Count; i ++) {
				//Debug.Log (colors [i]);
				//}
				random = UnityEngine.Random.Range (0, randomMax);
				//Debug.Log ("1 " + bubble [random].name);
				if (!colors.Contains (bubble [random].name)) {
						//Debug.Log ("hi");
						for (int i = 0; i < bubble.Length; i ++) {
								if (colors.Contains (bubble [i].name)) {
										random = i;
										//Debug.Log ("2 " + bubble [random].name);
								}
						}
				}
		}
	
		void PlaySound ()
		{
				if (bubble [random].name == "Blue") {
						audio.clip = shoot [0];
				} else if (bubble [random].name == "Green") {
						audio.clip = shoot [1];
				} else if (bubble [random].name == "Purple") {
						audio.clip = shoot [2];
				} else if (bubble [random].name == "Red") {
						audio.clip = shoot [3];
				} else if (bubble [random].name == "Yellow") {
						audio.clip = shoot [4];
				}
				audio.Play ();
		}

		/*public static float ClampAngle (float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp (angle, min, max);
	}*/

}
