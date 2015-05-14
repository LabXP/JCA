using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class FacebookPhoto : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		gameObject.GetComponent<Image>().overrideSprite = FindObjectOfType<UserController>().userPictureSprite;
			}
}
