using UnityEngine;
using System.Collections;

public class OptionsLobby : MonoBehaviour {
	public GameObject Options, Useless, Useless2;
	public GameObject[] HudNaFrente;
	public Sprite Hover, NotHover;
	public AudioClip HoverSound;



	
	void OnMouseEnter(){
		gameObject.GetComponent<SpriteRenderer>().sprite = Hover;
		GetComponent<AudioSource>().PlayOneShot(HoverSound, 1f);
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = NotHover;	
	}

	void OnMouseDown(){
		if (Options.activeSelf){
			gameObject.GetComponent<SpriteRenderer>().sprite = NotHover;
			Options.SetActive(false);
			//Useless.SetActive(true);
			//Useless2.SetActive(true);
			//foreach(GameObject hud in HudNaFrente){
			//	hud.SetActive(true);
			//}
		} else if (!Options.activeSelf) {
			Options.SetActive(true);
			//Useless.SetActive(false);
			//Useless2.SetActive(false);
			//foreach(GameObject hud in HudNaFrente){
			//	hud.SetActive(false);
			//}
		}
	}
}
