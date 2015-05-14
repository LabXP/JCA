using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FacebookName : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		gameObject.GetComponent<Text>().text = FindObjectOfType<UserController>().userName;
	}

}
