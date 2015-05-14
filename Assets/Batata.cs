using UnityEngine;
using System.Collections;

public class Batata : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		if (gameObject.activeSelf) {
			GameController.playing = false;
		}

		/* if(player acertou a sequencia certa){
			ganhou uma vida ! ! !
		}*/
	}
}
