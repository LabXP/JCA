using UnityEngine;
using System.Collections;

public class LivesText : MonoBehaviour {
	public int LivesCount;
	// Use this for initialization
	void Start () {
	Vector3 posicao;
		/*posicao = transform.position;
		posicao.x = (float) Screen.width / 8357;
		posicao.y = (float) Screen.height / (Screen.height - (Screen.height / 3000));
		transform.position = posicao;*/
		transform.position = new Vector3 (0.09f, 0.9f, 0);
		gameObject.GetComponent<GUIText>().fontSize = (int) Screen.width / 20;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<GUIText>().text = ":" + LivesCount;
	}
}
