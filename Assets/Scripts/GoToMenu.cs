using UnityEngine;
using System.Collections;

public class GoToMenu : MonoBehaviour {

	public Sprite Hover, NotHover;
	public string MainMenu;
	public AudioClip HoverSound;

	void OnMouseEnter(){
		audio.PlayOneShot(HoverSound, 1f);
		gameObject.GetComponent<SpriteRenderer>().sprite = Hover;
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = NotHover;	
	}

	void OnMouseDown(){
		Application.LoadLevel(MainMenu);
	}
}
