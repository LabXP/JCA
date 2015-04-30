using UnityEngine;
using System.Collections;

public class Opcoes : MonoBehaviour {

	public string Level;
	public Sprite hover;
	public Sprite NotHover;
	public GameObject Options;
	public GameObject[] Buttons;
	public GameObject Voltar;
	public AudioClip HoverSound;
	
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
		if (!Options.activeSelf){
		Buttons[0].SetActive(false);
		Buttons[1].SetActive(false);
		Buttons[2].SetActive(false);
		Voltar.GetComponent<SpriteRenderer>().sprite = Voltar.GetComponent<Opcoes>().NotHover;
		Options.SetActive(true);
		} else if (Options.activeSelf){
		Buttons[0].SetActive(true);
		Buttons[0].GetComponent<SpriteRenderer>().sprite = Buttons[0].GetComponent<MainMenu>().NotHover;
		Buttons[1].SetActive(true);
		Buttons[1].GetComponent<SpriteRenderer>().sprite = Buttons[1].GetComponent<Opcoes>().NotHover;
		Buttons[2].SetActive(true);
		Buttons[2].GetComponent<SpriteRenderer>().sprite = Buttons[2].GetComponent<Opcoes>().NotHover;
		Options.SetActive(false);	
		}
		 
		}
}
	

