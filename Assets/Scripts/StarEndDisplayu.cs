using UnityEngine;
using System.Collections;

public class StarEndDisplayu : MonoBehaviour
{
		//public int StarCount;
		public GameObject[] Stars;
		// Use this for initialization
		void Start ()
		{
	
		}

	
		// Update is called once per frame
		void Update ()
		{
				switch (FindObjectOfType<StarController> ().starCount [int.Parse (FindObjectOfType<StarController> ().level)]) {
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
