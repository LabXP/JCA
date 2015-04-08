using UnityEngine;
using System.Collections;

public class ErrorDisplay : MonoBehaviour {
	public bool PasswordError, ServerError;
	public GameObject[] Errors;

	void Update(){
		if (PasswordError){
			StartCoroutine(Error(0));
		} else if (ServerError){
			StartCoroutine(Error(1));
		}
	}

	IEnumerator Error(int i){
		Errors[i].SetActive(true);
		yield return new WaitForSeconds(3f);
		Errors[i].SetActive(false);
	}
}
