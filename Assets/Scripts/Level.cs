using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class Level : MonoBehaviour
{
		public GameObject[] Stars;
		public SpriteRenderer[] sr;
		public Sprite[] Hover, NotHover;
		public int StarCount;
		public string LevelName;
		private string level;
		public AudioClip HoverSound;

		void Awake ()
		{
				level = Regex.Match (LevelName, @"\d+").Value;
		}
		void OnMouseDown ()
		{		
				if ((FindObjectOfType<StarController> ().starCount [int.Parse (level) - 1]) > 0){
				Application.LoadLevel (LevelName);
			}
		}
		void OnMouseEnter ()
		{
				GetComponent<AudioSource>().PlayOneShot (HoverSound, 1f);
				sr [0].sprite = Hover [0];
				sr [1].sprite = Hover [1];
		}
		void OnMouseExit ()
		{
				sr [0].sprite = NotHover [0];
				sr [1].sprite = NotHover [1];
		}
		void Update ()
		{
				switch (FindObjectOfType<StarController> ().starCount [int.Parse (level)]) {
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

				}
		}

}
