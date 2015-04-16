using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Entrar : MonoBehaviour {
	public GameObject[] text;
	public string[] LoginInfo;

	public Sprite hover;
	public Sprite NotHover;
	public AudioClip HoverSound;
	public GameObject BlankError;

	public bool Logou;


	void OnMouseEnter(){
		GetComponent<AudioSource>().PlayOneShot(HoverSound);
	}
	void OnMouseDown(){
		LoginInfo[0] = text[0].GetComponent<InputField>().text;
		LoginInfo[1] = text[1].GetComponent<InputField>().text;
		if (LoginInfo[0] == "" || LoginInfo[1] == ""){
			StartCoroutine(Error());
		}
	}
	IEnumerator Error(){
		BlankError.SetActive(true);
		yield return new WaitForSeconds(3f);
		BlankError.SetActive(false);
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
