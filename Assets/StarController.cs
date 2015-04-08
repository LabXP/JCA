using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class StarController : MonoBehaviour
{
		public int[]
				starCount = new int[31];
		private int batata = 0;
		[HideInInspector]
		public string
				level;

		// Use this for initialization
		void Awake ()
		{
		
				DontDestroyOnLoad (transform.gameObject);
				
				Debug.Log (level);
		}
	
		// Update is called once per frame
		void Update ()
		{
				//print ("a"+starCount [int.Parse (level)]);
				//print (batata);
				level = Regex.Match (Application.loadedLevelName, @"\d+").Value;
//				print (level+"a");
				if (FindObjectOfType<GameController> () != null && FindObjectOfType<GameController> ().win) {
						batata = FindObjectOfType<GameController> ().bubblesLeft;
						Stars ();
				}
		}
		void Stars ()
		{
				if (Application.loadedLevelName == "Level1" || Application.loadedLevelName == "Level2" || Application.loadedLevelName == "Level3" || Application.loadedLevelName == "Level4" || Application.loadedLevelName == "Level5") {
						if (batata <= 3 && starCount [int.Parse (level)] < 1) {
								starCount [int.Parse (level)] = 1;
						} else if (batata >= 4 && batata <= 9 && starCount [int.Parse (level)] < 2) {
								starCount [int.Parse (level)] = 2;
						} else if (batata >= 10 && starCount [int.Parse (level)] < 3) {
								starCount [int.Parse (level)] = 3;
						}
				} else if (Application.loadedLevelName == "Level6" || Application.loadedLevelName == "Level7" || Application.loadedLevelName == "Level8" || Application.loadedLevelName == "Level9" || Application.loadedLevelName == "Level10" || Application.loadedLevelName == "Level11") {
						if (batata <= 3 && starCount [int.Parse (level)] < 1) {
								starCount [int.Parse (level)] = 1;
						} else if (batata >= 4 && batata <= 9 && starCount [int.Parse (level)] < 2) {
								starCount [int.Parse (level)] = 2;
						} else if (batata >= 10 && starCount [int.Parse (level)] < 3) {
								starCount [int.Parse (level)] = 3;
						}
				} else if (Application.loadedLevelName == "Level12" || Application.loadedLevelName == "Level13" || Application.loadedLevelName == "Level14" || Application.loadedLevelName == "Level15" || Application.loadedLevelName == "Level16" || Application.loadedLevelName == "Level17" || Application.loadedLevelName == "Level18" || Application.loadedLevelName == "Level19" || Application.loadedLevelName == "Level20") {
						if (batata <= 5 && starCount [int.Parse (level)] < 1) {
								starCount [int.Parse (level)] = 1;
						} else if (batata >= 6 && batata <= 10 && starCount [int.Parse (level)] < 2) {
								starCount [int.Parse (level)] = 2;
						} else if (batata >= 11 && starCount [int.Parse (level)] < 3) {
								starCount [int.Parse (level)] = 3;
						}
				} else {
						if (batata <= 12) {
								starCount [int.Parse (level)] = 1;
						} else if (batata >= 13 && batata <= 20) {
								starCount [int.Parse (level)] = 2;
						} else if (batata >= 21) {
								starCount [int.Parse (level)] = 3;
						}
				}
		}
}
