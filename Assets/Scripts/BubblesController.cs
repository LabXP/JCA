using UnityEngine;
using System.Collections;

public class BubblesController : MonoBehaviour
{
	public bool isMoving = true;
	public int rotationLock = 0;

	//Mantem a rotação no zero
	void Update ()
	{
		transform.rotation = Quaternion.Euler (rotationLock, rotationLock, rotationLock);
	}

	//Verifica colisão
	void OnCollisionEnter2D (Collision2D obj)
	{
		if (obj.transform.tag == "Bubble" || obj.transform.tag == "Roof") {
			isMoving = false;
		}
	}

}