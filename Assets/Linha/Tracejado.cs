using UnityEngine;
using System.Collections;

public class Tracejado : MonoBehaviour {
	private ParticleSystem ps;
	public bool batata;
	public GuideLine batatinha;

	void Awake(){
		ps = GetComponent<ParticleSystem>();
		batatinha = (GuideLine) FindObjectOfType(typeof(GuideLine));
	}
	
	// Update is called once per frame
	void Update () {
		if (batata){
			transform.position = batatinha.guide.point;
			if (batatinha.guide.transform == null){
				ps.Clear();
			}
		}
		if (!batata){
			transform.position = batatinha.dir.position;
		}
		print(transform.localEulerAngles.x);
			ps.startRotation = transform.localEulerAngles.x;
	}
}
