using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Entrar : MonoBehaviour {
	public GameObject[] text;
	public string[] LoginInfo;

	public Sprite hover;
	public Sprite NotHover;
	public AudioClip HoverSound;

	public bool Logou;


	void OnMouseEnter(){
		audio.PlayOneShot(HoverSound);
	}
	void OnMouseDown(){
		LoginInfo[0] = text[0].GetComponent<InputField>().text;
		LoginInfo[1] = text[1].GetComponent<InputField>().text;
	}

	void OnMouseOver(){
		gameObject.GetComponent<SpriteRenderer>().sprite = hover;
	}
	void OnMouseExit(){
		gameObject.GetComponent<SpriteRenderer>().sprite = NotHover;
	}
	void Update(){
		if (Logou){
			Application.LoadLevel("MainScreen");
		}
	}

}
