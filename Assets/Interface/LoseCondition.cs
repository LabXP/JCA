using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoseCondition : MonoBehaviour
{
		public Text texto;
	
		void Update ()
		{
				texto.text = "" + FindObjectOfType<GameController> ().bubblesLeft;
		}
}
