using UnityEngine;
using System.Collections;

public class LineFollow : MonoBehaviour
{

		public GameObject cannon;

		private float mouseMovement;
		private Vector3 initialPosition;
		private TrailRenderer trailRenderer;

		void Start ()
		{
				initialPosition = transform.position;
				trailRenderer = GetComponent<TrailRenderer> ();
				trailRenderer.startWidth = 0.2f;
				trailRenderer.endWidth = 0.2f;
		}


		void Update ()
		{
				if (FindObjectOfType<GameController> ().playing) {
						float h = Input.GetAxisRaw ("Horizontal");
						mouseMovement = Input.GetAxis ("Mouse X");
						gameObject.transform.rotation = Quaternion.identity;

		
						if (mouseMovement != 0 || h != 0) {
								rigidbody2D.isKinematic = false;
								collider2D.isTrigger = false;
								transform.position = initialPosition;
								rigidbody2D.velocity = cannon.transform.right * 50;
						}
						if (transform.position == initialPosition) {
								trailRenderer.time = 0f;
						} else {
								trailRenderer.time = 20f;
						}
				} else {
			
						trailRenderer.time = 0f;
				}
		}

		void OnCollisionEnter2D (Collision2D obj)
		{
				if (obj.collider.tag == "Bubble" || obj.collider.tag == "Roof") {
						collider2D.isTrigger = true;
						rigidbody2D.isKinematic = true;
				}
		}

}
