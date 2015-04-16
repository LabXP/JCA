using UnityEngine;
using System.Collections;


public class MainMenu : MonoBehaviour {

	public string Level;
	public Sprite hover;
	public Sprite NotHover;
	public AudioClip HoverSound;
	public AudioSource AmbientMusic;

	void OnMouseOver(){
		gameObject.GetComponent<SpriteRenderer>().sprite = hover;
		
	}
	void OnMouseEnter(){
		GetComponent<AudioSource>().PlayOneShot(HoverSound);
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = NotHover;
	}
	void OnMouseDown(){
		StartCoroutine(ChangeLevel());
	}

	IEnumerator ChangeLevel(){
		float fadeTime = GameObject.Find("Jogar").GetComponent<Fading>().BeginFade(1);
		yield return new WaitForSeconds(fadeTime);
				Application.LoadLevel(Level);
	
	}
}
