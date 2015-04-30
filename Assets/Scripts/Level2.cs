using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Level2 : MonoBehaviour
{
		public GameObject[] Stars;
		public SpriteRenderer[] sr;
		public Sprite[] Hover, NotHover;
		public int StarCount;
		public string LevelName;
		private string level;
		private Color levelColor = new Color(0.243f, 0.243f, 0.243f, 1.000f);
		public AudioClip HoverSound;
		public GameObject[] Hoverparticles;
		private Animator anim;
		public MeshRenderer[] LevelMaterial;

		void Awake ()
		{		
				LevelMaterial = GetComponentsInChildren<MeshRenderer>() as MeshRenderer[];
				anim = gameObject.GetComponentInChildren<Animator>();
				level = Regex.Match (LevelName, @"\d+").Value;
				if ((FindObjectOfType<StarController> ().starCount [int.Parse (level) - 1]) == 0){
				//gameObject.SetActive(false);
					foreach (MeshRenderer levelMat in LevelMaterial){
						levelMat.material.color = levelColor;
					}
			}
				
		}
		void OnMouseDown ()
		{		
				if ((FindObjectOfType<StarController> ().starCount [int.Parse (level) - 1]) > 0){
				Application.LoadLevel (LevelName);
			}	
		}
		void OnMouseEnter ()
		{
			if ((FindObjectOfType<StarController> ().starCount [int.Parse (level) - 1]) > 0){
				anim.SetBool("Hover", true);
				GetComponent<AudioSource>().PlayOneShot (HoverSound, 1f);
				Hoverparticles[0].SetActive(true);
				Hoverparticles[1].SetActive(true);
			}
				/*sr [0].sprite = Hover [0];
				sr [1].sprite = Hover [1];*/
		}
		void OnMouseExit ()
		{		
				anim.SetBool("Hover", false);
				Hoverparticles[0].SetActive(false);
				Hoverparticles[1].SetActive(false);
				/*
				sr [0].sprite = NotHover [0];
				sr [1].sprite = NotHover [1];
				*/
		}
		void Update ()
		{
				/*switch (FindObjectOfType<StarController> ().starCount [int.Parse (level)]) {
				case 0:
						Stars [0].SetActive (false);
						Stars [1].SetActive (false);
						Stars [2].SetActive (false);
						break;
				case 1:
						Stars [0].SetActive (true);
						Stars [1].SetActive (false);
						Stars [2].SetActive (false);
						break;
				case 2:
						Stars [0].SetActive (true);
						Stars [1].SetActive (true);
						Stars [2].SetActive (false);
						break;
				case 3:
						Stars [0].SetActive (true);
						Stars [1].SetActive (true);
						Stars [2].SetActive (true);
						break;

				}*/
		}

}
