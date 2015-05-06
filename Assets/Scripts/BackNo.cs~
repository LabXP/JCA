using UnityEngine;
using System.Collections;

public class BackNo : MonoBehaviour
{
	public Sprite Hover, NotHover;
	public GameObject PopUp;
	public AudioClip HoverSound;

	void OnMouseEnter ()
	{
		GetComponent<AudioSource> ().PlayOneShot (HoverSound, 1f);
		gameObject.GetComponent<SpriteRenderer> ().sprite = Hover;
	}
	void OnMouseExit ()
	{
		gameObject.GetComponent<SpriteRenderer> ().sprite = NotHover;
	}
	void OnMouseDown ()
	{
		PopUp.SetActive (false);
		Invoke ("Resume", 0.05f);
		gameObject.GetComponent<SpriteRenderer> ().sprite = NotHover;
	}
	void Resume ()
	{
		GameController.playing = true;		
	}
}
