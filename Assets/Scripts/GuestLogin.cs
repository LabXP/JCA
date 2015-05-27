using UnityEngine;
using System.Collections;

public class GuestLogin : MonoBehaviour {
	public GameObject[] FacebookShit;
	// Use this for initialization
	void Awake () {
		if (Object.FindObjectOfType<UserController>().Facebook == false){
			foreach (GameObject fbs in FacebookShit){
				fbs.SetActive(false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
