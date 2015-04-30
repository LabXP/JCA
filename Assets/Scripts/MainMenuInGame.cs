using UnityEngine;
using System.Collections;

public class MainMenuInGame : MonoBehaviour
{
		public GameObject Hover, PopUp, Vitoria, Derrota;
		public AudioClip HoverSound;

		void OnMouseEnter ()
		{
				GetComponent<AudioSource>().PlayOneShot (HoverSound, 1f);
				Hover.SetActive (true);
				FindObjectOfType<GameController> ().playing = false;
		}
		void OnMouseExit ()
		{
				Hover.SetActive (false);
				FindObjectOfType<GameController> ().playing = true;
		}
		void OnMouseDown ()
		{
				if (!Vitoria.activeSelf && !Derrota.activeSelf) {
						PopUp.SetActive (true);
				}
		}
}
