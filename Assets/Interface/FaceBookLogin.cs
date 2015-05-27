using UnityEngine;
using System.Collections;

public class FaceBookLogin : MonoBehaviour {
	public Sprite hover;
	public Sprite NotHover;
	public AudioClip HoverSound;

	void OnMouseEnter(){
		GetComponent<AudioSource>().PlayOneShot(HoverSound);
		gameObject.GetComponent<SpriteRenderer>().sprite = hover;
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = NotHover;
	}

	void OnMouseDown(){
		print("Logar com o facebook, :v");
	}
}
