using UnityEngine;
using System.Collections;

public class detonate : MonoBehaviour {
	public float detonatetime;
	public GameObject gameobject;
	// Use this for initialization
	void Awake () {
		DestroyObject (gameobject, 1f);
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
