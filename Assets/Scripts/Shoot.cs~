using UnityEngine;
using System.Collections;

/*input do jogador e atira bolhas*/
public class Shoot : MonoBehaviour
{

	public GameObject cannon; //canhao
	public Transform initialBubblePosition; //lugar do lado do canhao que mostra a proxima bolha
	public bool canShoot = true;
	public AudioSource bubbleAudioSource;
	public AudioClip[] bubbleAudioClips;

	private Bubbles bubbleInstance, thrownBubble;	//bubbles usados no lançamento (proxima bolha, bolha sendo jogada)
	private Vector3 diff; //controle do mouse
	private float rotZ = 90f; //controle de rotaçao
	private Animator cannonAnimator; //animator do canhao
	private bool shot = false; //controla se uma bolha foi lançada
	private Helper helper;

	void Start ()
	{
		//cria a bolha inicial
		bubbleInstance = new Bubbles (initialBubblePosition.position.x, initialBubblePosition.position.y);
		cannonAnimator = cannon.GetComponent<Animator> ();
		helper = GameObject.FindObjectOfType<Helper> ();
	}

	void FixedUpdate ()
	{
		if (GameController.playing) {
			if (canShoot) {
				//controle de mouse e teclado (TODO CONTROLE COM TOUCH)
				float h = Input.GetAxisRaw ("Horizontal");
				float mouseMovement = Input.GetAxis ("Mouse X");

				diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - cannon.transform.position;
				//normalize difference  
				diff.Normalize ();

				//calculate rotation
				if (mouseMovement != 0) {
					rotZ = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
				} else if (h != 0) {
					rotZ -= h * 100 * Time.deltaTime;
				}
				//apply to object

				//Debug.Log(rotZ);

				if (rotZ > 170) {
					rotZ = 170;
				} else if (rotZ < 10) {
					rotZ = 10;
				} else {
					cannon.transform.rotation = Quaternion.Euler (0, 0, rotZ);
				}
			}
			//atira a bolha
			if ((Input.GetMouseButtonDown (0) || Input.GetKey ("space")) && shot == false && canShoot == true) {

				canShoot = false;

				//faz a bolha se mover
				thrownBubble = bubbleInstance;
				thrownBubble.bubbleObject.GetComponent<Rigidbody2D> ().isKinematic = false;
				thrownBubble.bubbleObject.transform.position = cannon.transform.position;
				thrownBubble.bubbleObject.GetComponent<Rigidbody2D> ().velocity = cannon.transform.right * 17;
				cannonAnimator.SetTrigger ("Shoot");

				PlaySound (thrownBubble);

				shot = true;
			}

		}
		//faz a bolha parar
		if (shot && !thrownBubble.bubbleObjectController.isMoving) {
			//	Debug.Log (thrownBubble.bubbleObjectController.isMoving);
			if (thrownBubble.bubbleObject != null) {
				thrownBubble.bubbleObject.GetComponent<Rigidbody2D> ().isKinematic = true;
				helper.TakeATurn (thrownBubble);
			}
		}

		if (canShoot && shot) {
			//cria a proxima bolha
			bubbleInstance = new Bubbles (initialBubblePosition.position.x, initialBubblePosition.position.y, helper.ShootableColor ());
			shot = false;
		}
	}

	void PlaySound (Bubbles bubble)
	{
		string color = bubble.getColor ();

		switch (color) {
		case "Blue":
			bubbleAudioSource.clip = bubbleAudioClips [0];
			bubbleAudioSource.Play ();
			break;
		case "Red":
			bubbleAudioSource.clip = bubbleAudioClips [1];
			bubbleAudioSource.Play ();
			break;
		case "Yellow":
			bubbleAudioSource.clip = bubbleAudioClips [2];
			bubbleAudioSource.Play ();
			break;
		case "Green":
			bubbleAudioSource.clip = bubbleAudioClips [3];
			bubbleAudioSource.Play ();
			break;
		case "Purple":
			bubbleAudioSource.clip = bubbleAudioClips [4];
			bubbleAudioSource.Play ();
			break;
		}
	}
}
