using UnityEngine;
using System.Collections;

public class GuideLine : MonoBehaviour {
	public Transform start, dir;
	public LayerMask lm, lmLeft, lmRight;
	private float dist = Mathf.Infinity;
	// Use this for initialization
	void Start () {
	}
	public RaycastHit2D guide;
	// Update is called once per frame
	void Update () {
		Vector3 direc = dir.position - start.position;
		guide = Physics2D.CircleCast(start.position, 0.35f, direc, dist, lm);

		if (guide.transform != null && guide.transform.tag == "LeftWall"){
		 GetComponent<LineRenderer>().SetVertexCount(3);
		 Vector3 incomingVec = new Vector3(guide.point.x, guide.point.y, 0) - start.position;
         Vector3 reflectVec = Vector3.Reflect(incomingVec, guide.normal);
         RaycastHit2D guide2 = Physics2D.CircleCast(guide.point, 0.35f, reflectVec, dist, lmLeft);
		 Debug.DrawLine(start.position, guide.point, Color.red);
		 Debug.DrawLine(guide.point, guide2.point, Color.green);
         Debug.DrawRay(guide.point, reflectVec, Color.cyan);
         GetComponent<LineRenderer>().SetPosition(2, guide2.point);
     	} else if (guide.transform != null && guide.transform.tag == "RightWall"){
     	
		 GetComponent<LineRenderer>().SetVertexCount(3);
		 Vector3 incomingVec = new Vector3(guide.point.x, guide.point.y, 0) - start.position;
         Vector3 reflectVec = Vector3.Reflect(incomingVec, guide.normal);
         RaycastHit2D guide2 = Physics2D.CircleCast(guide.point, 0.35f, reflectVec, dist, lmRight);
		 Debug.DrawLine(start.position, guide.point, Color.red);
		 Debug.DrawLine(guide.point, guide2.point, Color.green);
         Debug.DrawRay(guide.point, reflectVec, Color.cyan);
         GetComponent<LineRenderer>().SetPosition(2, guide2.point);
     	}
     	else { 
     		GetComponent<LineRenderer>().SetVertexCount(2);
     	}
     	 Debug.DrawLine(start.position, guide.point, Color.red);
     	 GetComponent<LineRenderer>().SetPosition(0, start.position);

     	 GetComponent<LineRenderer>().SetPosition(1, guide.point);

     	float distanceA = Vector3.Distance(start.position, guide.point);
		GetComponent<LineRenderer>().GetComponent<Renderer>().material.mainTextureScale = new Vector2(distanceA/4, 1);
	}

}
