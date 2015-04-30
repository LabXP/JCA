using UnityEngine;
using System;

public class Bubble : MonoBehaviour
{
	
		public float positionX, positionY;
		public static int row, column;
		public static bool getDown = false;


		void OnCollisionEnter2D (Collision2D obj)
		{
				if (obj.collider.tag == "Wall") {
						FindObjectOfType<Shoot> ().dir.x *= -1;
				}
				if (obj.gameObject.tag == "Bubble" || obj.gameObject.tag == "Roof") {
						if (!GetComponent<Rigidbody2D>().isKinematic) {
								GetComponent<Rigidbody2D>().isKinematic = true;
								Position ();
								//FindObjectOfType<Shoot> ().arrived = true;
								FindObjectOfType<Shoot> ().hitWall = false;
						}
				}
		}

		void Update ()
		{
				//gameObject.transform.rotation = Quaternion.identity;
		}

		void Position ()
		{

				//Debug.Log (gameObject.transform.position.y);
				//positionY = RoundY (gameObject.transform.position.y);
				//Debug.Log (positionY);

				//Debug.Log (transform.position.x);
				//positionX = FindObjectOfType<BubbleMatrix> ().SetColumn (transform.position.x, positionY);

				Vector2 position = FindObjectOfType<GameController> ().PositionBubble (gameObject.transform.position);
				gameObject.transform.position = position;

				//MUITA ENJAMBRAÇAO LOUCA
				//try {
				FindObjectOfType<GameController> ().bubbles [FindObjectOfType<GameController> ().PositionToRow (position.y), FindObjectOfType<GameController> ().PositiontoColumn (position.x, FindObjectOfType<GameController> ().PositionToRow (position.y))] = gameObject;
				/*} catch (System.ArgumentOutOfRangeException) {
						try {
								FindObjectOfType<GameController> ().bubbles [FindObjectOfType<GameController> ().PositionToRow (position.y)].Insert (FindObjectOfType<GameController> ().PositiontoColumn (position.x, FindObjectOfType<GameController> ().PositionToRow (position.y)), gameObject);
						} catch (System.ArgumentOutOfRangeException) {
								FindObjectOfType<GameController> ().bubbles.Add (null);
								FindObjectOfType<GameController> ().bubbles [FindObjectOfType<GameController> ().PositionToRow (position.y)].Insert (FindObjectOfType<GameController> ().PositiontoColumn (position.x, FindObjectOfType<GameController> ().PositionToRow (position.y)), gameObject);
						}
				}*/

				FindObjectOfType<GameController> ().DestroyBubbles (FindObjectOfType<GameController> ().PositionToRow (position.y), FindObjectOfType<GameController> ().PositiontoColumn (position.x, FindObjectOfType<GameController> ().PositionToRow (position.y)));


				/*row = BubbleMatrix.GetRow (positionY);
				column = FindObjectOfType<BubbleMatrix> ().GetColumn (positionX, BubbleMatrix.GetRow (positionY));
				BubbleMatrix.table [row, column] = true;
				BubbleMatrix.colors [row, column] = FindObjectOfType<Shoot> ().color;
				BubbleMatrix.tableObjects [row, column] = FindObjectOfType<Shoot> ().bubbleObject;
				//Debug.Log (BubbleMatrix.GetRow(positionY));
				//Debug.Log(BubbleMatrix.GetColumn(positionX, BubbleMatrix.GetRow(positionY)));

				FindObjectOfType<BubbleMatrix> ().DestroyBubbles (row, column);
*/
				/*if (contactPoint.x > contactBubble.transform.position.x){
			positionX = contactBubble.transform.position.x + bubbleRadius;
		}
		else if (contactPoint.x < contactBubble.transform.position.x){
			positionX = contactBubble.transform.position.x - bubbleRadius;
		}
		else {
			positionX = contactBubble.transform.position.x + bubbleRadius;
		}

		positionY = contactBubble.transform.position.y - 0.9f;
		
		Vector3 position = new Vector3 (positionX, positionY, contactBubble.transform.position.z);

		Collider[] hitColliders = Physics.OverlapSphere(position, 0.1f);

		if (hitColliders.Length == 0 || (hitColliders.Length <= 1 && hitColliders[0] == gameObject.collider)){
			
			gameObject.transform.position = position;
		}
		else{
			if (contactPoint.x >= contactBubble.transform.position.x){
				positionX = contactBubble.transform.position.x + (bubbleRadius * 2);
			}
			else if (contactPoint.x <= contactBubble.transform.position.x){
				positionX = contactBubble.transform.position.x - (bubbleRadius * 2);
			}
			positionY = contactBubble.transform.position.y;
			
			position = new Vector3 (positionX, positionY, contactBubble.transform.position.z);
			hitColliders = Physics.OverlapSphere(position, 0.1f);

			if (hitColliders.Length >= 1 && hitColliders[0] != gameObject.collider){
				if (positionX > contactBubble.transform.position.x){
					positionX -= bubbleRadius * 3;
				}
				else{
					positionX += bubbleRadius * 3;
				}
				positionY = contactBubble.transform.position.y - 0.9f;
				
				position = new Vector3 (positionX, positionY, contactBubble.transform.position.z);
				Debug.Log(position);
				hitColliders = Physics.OverlapSphere(position, 0.1f);

				if (hitColliders.Length >= 1 && hitColliders[0] != gameObject.collider){
					if (positionX > contactBubble.transform.position.x){
						positionX += bubbleRadius;
					}
					else{
						positionX -= bubbleRadius;
					}
					position = new Vector3 (positionX, positionY, contactBubble.transform.position.z);
					Debug.Log(position);
				}
			}

		}
		
		position = new Vector3 (positionX, positionY, contactBubble.transform.position.z);
		gameObject.transform.position = position;
		*/
		}
		
		float RoundY (float y)
		{

				float whole = (int)y;
				float remainder = y - whole;

				if (remainder > 0.5) {
						return whole + 0.5f;
				} else {
						return whole - 0.5f;
				}
		}
}
