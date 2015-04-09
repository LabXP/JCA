using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {
		public Text texto;
	
		void Update ()
		{
				texto.text = "" + FindObjectOfType<HighScore> ().score;
		}
}
