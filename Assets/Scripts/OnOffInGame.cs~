using UnityEngine;
using System.Collections;

public class OnOffInGame : MonoBehaviour
{
	public GameObject Botao, Coisa;
	public AudioController audioController;
	public bool FX, Music;

	void Awake ()
	{
		Coisa = GameObject.Find ("AudioController");
		audioController = Coisa.GetComponent<AudioController> ();
	}

	void Update ()
	{
		if (FX) {
			if (audioController.FX) {
				Botao.SetActive (false);
			}
		}
		if (FX) {
			if (!audioController.FX) {
				Botao.SetActive (true);
			}
		}
		if (Music) {
			if (audioController.Music) {
				Botao.SetActive (false);
			}
		}
		if (Music) {
			if (!audioController.Music) {
				Botao.SetActive (true);
			}
		}
	}

	void OnMouseDown ()
	{
		if (Music) {
			if (!Botao.activeSelf) {
				Botao.SetActive (true);
				audioController.Music = false;
			} else if (Botao.activeSelf) {
				Botao.SetActive (false);
				audioController.Music = true;
			}
		}
		if (FX) {
			if (!Botao.activeSelf) {
				Botao.SetActive (true);
				audioController.FX = false;
			} else if (Botao.activeSelf) {
				Botao.SetActive (false);
				audioController.FX = true;
			}
		}
		
	}
	void OnMouseEnter ()
	{
		GameController.playing = false;
	}
	void OnMouseExit ()
	{
		GameController.playing = true;
	}
}
