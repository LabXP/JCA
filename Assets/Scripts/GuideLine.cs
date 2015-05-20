using UnityEngine;
using System.Collections;

public class GuideLine : MonoBehaviour {
	public Transform start, dir;
	public LayerMask lm;
	private float dist = Mathf.Infinity;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direc = dir.position - start.position;
		RaycastHit2D guide = Physics2D.CircleCast(start.position, 1f, direc, dist, lm);
		if (guide.transform.tag == "Wall"){
		 Vector3 incomingVec = new Vector3(guide.point.x, guide.point.y, 0) - start.position;
         Vector3 reflectVec = Vector3.Reflect(incomingVec, guide.normal);
		 Debug.DrawLine(start.position, guide.point, Color.red);
         Debug.DrawRay(guide.point, reflectVec, Color.green);
     	}
     	 Debug.DrawLine(start.position, guide.point, Color.red);
	}
}
