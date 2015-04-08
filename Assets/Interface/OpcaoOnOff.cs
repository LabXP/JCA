using UnityEngine;
using System.Collections;

public class OpcaoOnOff : MonoBehaviour {
	public Sprite On, Off;
	private GameObject Coisa;
	public AudioController audioController;
	public bool FX, Music;

	void Awake(){
		Coisa = GameObject.Find("AudioController");
		audioController = Coisa.GetComponent<AudioController>();
	}

	void Update(){
		if (FX){
		if(audioController.FX){
			gameObject.GetComponent<SpriteRenderer>().sprite = On;
		}
		}
		if (FX){
		if(!audioController.FX){
			gameObject.GetComponent<SpriteRenderer>().sprite = Off;
		}
		}
		if (Music){
			if(audioController.Music){
				gameObject.GetComponent<SpriteRenderer>().sprite = On;
			}
		}
		if (Music){
			if(!audioController.Music){
				gameObject.GetComponent<SpriteRenderer>().sprite = Off;
			}
		}
	}

	void OnMouseDown(){

		if (FX){
			if (gameObject.GetComponent<SpriteRenderer>().sprite == On){
			gameObject.GetComponent<SpriteRenderer>().sprite = Off;
			audioController.FX = false;
		} else if (gameObject.GetComponent<SpriteRenderer>().sprite = Off){
			gameObject.GetComponent<SpriteRenderer>().sprite = On;
			audioController.FX = true;
		}
		}
		if (Music){
				if (gameObject.GetComponent<SpriteRenderer>().sprite == On){
			gameObject.GetComponent<SpriteRenderer>().sprite = Off;
			audioController.Music = false;
		} else if (gameObject.GetComponent<SpriteRenderer>().sprite = Off){
			gameObject.GetComponent<SpriteRenderer>().sprite = On;
			audioController.Music = true;
		}
		}
		
	}
}
