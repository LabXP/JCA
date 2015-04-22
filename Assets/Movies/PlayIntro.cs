using UnityEngine;
using System.Collections;

public class PlayIntro : MonoBehaviour {

	public MovieTexture movTexture;
    void Start() {
        GetComponent<Renderer>().material.mainTexture = movTexture;
        movTexture.Play();
        Invoke("GoToLogin", 10);
    }

    void GoToLogin(){
    	Application.LoadLevel("Login");
    }
}
