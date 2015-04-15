using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUILives : MonoBehaviour {
	public Text texto;
	public string vidas;
	
		void Update ()
		{
			texto.text = "" + vidas;
		}
}
