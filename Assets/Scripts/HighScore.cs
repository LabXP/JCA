using UnityEngine;
using System.Collections;

public class HighScore : MonoBehaviour {
	public int score;
	public bool control;
	// Use this for initialization
	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (FindObjectOfType<GameController> () != null && FindObjectOfType<GameController> ().win && !control) {
			control = true;
						if ((FindObjectOfType<StarController> ().starCount [int.Parse (FindObjectOfType<StarController> ().level)]) <= 0){
						score += FindObjectOfType<GameController> ().totalPoints;
						
					}
			}
		if (FindObjectOfType<GameController> () != null && !FindObjectOfType<GameController> ().win && control) {
			control = false;
		}
	}
}
