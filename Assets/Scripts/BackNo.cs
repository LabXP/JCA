using UnityEngine;
using System.Collections;

public class BackNo : MonoBehaviour {
	public Sprite Hover, NotHover;
	public GameObject PopUp;
	public AudioClip HoverSound;

	void OnMouseEnter(){
		audio.PlayOneShot(HoverSound, 1f);
		gameObject.GetComponent<SpriteRenderer>().sprite = Hover;
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = NotHover;
	}
	void OnMouseDown(){
		PopUp.SetActive(false);
		gameObject.GetComponent<SpriteRenderer>().sprite = NotHover;
	}
}
