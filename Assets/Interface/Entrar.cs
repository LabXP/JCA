using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Entrar : MonoBehaviour {
	public GameObject[] text;

	public Sprite hover;
	public Sprite NotHover;
	public AudioClip HoverSound;

	

	void Update(){

	}


	void OnMouseEnter(){
		GetComponent<AudioSource>().PlayOneShot(HoverSound);
		gameObject.GetComponent<SpriteRenderer>().sprite = hover;

	}
	void OnMouseDown(){
		Application.LoadLevel("MainScreen0");
		Object.FindObjectOfType<UserController>().Facebook = false;
	}

	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = NotHover;
	}

}
