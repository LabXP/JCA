using UnityEngine;
using System.Collections;

public class PosicaoTexto : MonoBehaviour {
	public RectTransform Texto;
	public float PosX, PosY;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Texto.anchoredPosition = new Vector2(Screen.width / PosX, Screen.height / PosY);
	}
}
