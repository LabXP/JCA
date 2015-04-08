using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIScore : MonoBehaviour
{
		public Text texto;
	
		void Update ()
		{
				texto.text = "" + FindObjectOfType<GameController> ().totalPoints;
		}
}
