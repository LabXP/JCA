using UnityEngine;
using System.Collections;

public class PlayIntro : MonoBehaviour
{

	//public MovieTexture movTexture;
<<<<<<< HEAD
	void Start ()
	{
		//GetComponent<Renderer>().material.mainTexture = movTexture;
		// movTexture.Play();
		Invoke ("GoToLogin", 10);
	}
=======
    void Start() {
      //  GetComponent<Renderer>().material.mainTexture = movTexture;
       // movTexture.Play();
        Invoke("GoToLogin", 10);
    }
>>>>>>> origin/master

	void GoToLogin ()
	{
		Application.LoadLevel ("Login");
	}
}
