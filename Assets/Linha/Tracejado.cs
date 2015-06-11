using UnityEngine;
using System.Collections;

public class Tracejado : MonoBehaviour {

	public GuideLine gl;

	void Awake(){
		

	}
	
	// Update is called once per frame
	void Update () {
		transform.position = gl.pontoA;
	}
}
