using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{

		public GameObject[] bubblesPrefab;
		public GameObject victory, defeat;
		[HideInInspector]
		public bool
				playing = true;
		[HideInInspector]
		public GameObject[,]
				bubbles = new GameObject[31, 20];
		public AudioClip destroyAudio;
		public AudioClip fallingAudio;
		[HideInInspector]
		public int
				bubblesAmount;
		[HideInInspector]
		public int
				bubblesLimit;
		[HideInInspector]
		public int
				rows;
		[HideInInspector]
		public int
				bubblesVariety;
		[HideInInspector]
		public static int
				life = 5;
		[HideInInspector]
		public int
				totalPoints;

		private int columns = 20;
		private Vector2 initialPosition;
		private float bubbleSize = 1;
		private AudioSource audios;
		[HideInInspector]
		public int
				bubblesLeft;
		private int shotBubbles;
		private int fallingBubbles;
		private int timesDown;
		private int turnPoints;
		private bool combo;
		private int basePoints = 100;
		private int pointsModifier = 1;
		[HideInInspector]
		public bool
				win = false, lose = false;

		void Start ()
		{
				for (int i = 0; i < bubbles.GetLength(0); i++) {
						for (int j = 0; j < bubbles.GetLength(1); j ++) {
								bubbles [i, j] = null;
						}
				}
				initialPosition = new Vector2 (-9.5f, 4.5f);
				InitialMatrix (rows, columns);
				audios = GetComponent<AudioSource> ();
				bubblesLeft = bubblesAmount;
		}
		void Update ()
		{
				if (Input.GetKeyDown ("q")) {
						/*Debug.Log (IsThisRowSmaller (0));
						for (int i = 0; i < bubbles.GetLength(0); i++) {
								for (int j = 0; j < bubbles.GetLength(1); j++) {
										if (bubbles [i, j] == null) {
												Debug.Log (i + "\t" + j);
										}
								}
						}*/
						win = true;
						
				}

				if (bubblesLeft <= 0 && !lose) {
						Debug.Log ("Lose");
						defeat.SetActive (true);
						life--;
						lose = true;

						Debug.Log ("Life" + life);
						//Application.LoadLevel (Application.loadedLevel);
				}
				Win ();

				/*if (!win && !lose) {
						playing = true;
				} else {
						playing = false;
				}*/
		}

		void Win ()
		{
				int num = 0;
				for (int i = 0; i < bubbles.GetLength(0); i++) {
						for (int j = 0; j < bubbles.GetLength(1); j++) {
								if (bubbles [i, j] != null) {
										num++;
								}
						}
				}
				if (num == 0) {
						win = true;
						Debug.Log ("win");
						victory.SetActive (true);
				}
		}
		//Constroi as bolhas iniciais 
		void InitialMatrix (int row, int column)
		{
				for (int i = 0; i < row; i++) {
						//Numero de colunas de acordo com linha menor ou maior
						if (IsThisRowSmaller (i)) {
								column -= 1;
						} else {
								column = this.columns;
						}

						//Lista que vai dentro da outra
						for (int j = 0; j < column; j++) {
								//Instancia bolha
								GameObject bubbleInstance = InstantiateBubble (i, j);
								bubbleInstance.GetComponent<Rigidbody2D>().isKinematic = true;
								//Adiciona na lista
								bubbles [i, j] = bubbleInstance;
						}
				}
				//Destroy (bubbles [2,5]);
		}

		//Instancia uma bolha aleatoria na linha / coluna
		GameObject InstantiateBubble (int row, int column)
		{
				int rand = UnityEngine.Random.Range (0, this.bubblesVariety);
				GameObject bubbleInstance = (GameObject)Instantiate (this.bubblesPrefab [rand], new Vector2 (ColumnToPosition (column, row), RowToPosition (row)), Quaternion.identity);
				bubbleInstance.name = bubblesPrefab [rand].name;
				return bubbleInstance;
		}

		//Verifica se essa linha e menor
		bool IsThisRowSmaller (int row)
		{
				/*bool firstRowSmall = false;
				try {
						//Se a primeira for menor ou maior, e usa isso pra ver a linha em especifico
						for (int i = 0; i < bubbles.GetLength(1); i ++) {
								if (bubbles [0, i] != null && bubbles [0, i].transform.position.x - (int)bubbles [0, i].transform.position.x == 0) {
										firstRowSmall = true;
								} else {
										firstRowSmall = false;
								}
						}
				} catch (System.IndexOutOfRangeException) {
						
				}*/

				if (firstRowIsSmaller && row % 2 == 0 || !firstRowIsSmaller && row % 2 != 0) {
						return true;
				} else {
						return false;
				}
		}

		//Da a posiçao y de acordo com a linha
		float RowToPosition (int row)
		{
				return this.initialPosition.y - (row * this.bubbleSize);
		}

		public int PositionToRow (float position)
		{
				return (int)(this.initialPosition.y - position);
		}

		//Da a posiçao x de acordo com a coluna
		float ColumnToPosition (int column, int row)
		{
				if (!IsThisRowSmaller (row)) {
						return this.initialPosition.x + (column * this.bubbleSize);
				} else {
						return this.initialPosition.x + (column * this.bubbleSize) + (bubbleSize / 2);
				}
		}

		public int PositiontoColumn (float position, int row)
		{
				if (!IsThisRowSmaller (row)) {
						return (int)Mathf.Abs (this.initialPosition.x - position);
				} else {
						return (int)Mathf.Abs ((this.initialPosition.x + (bubbleSize / 2)) - position);
				}
		}
	
		bool IsSpotOccupied (int row, int column)
		{
				try {
						if (bubbles [row, column] != null) {
								return true;
						} else {
								return false;
						}
				} catch (System.IndexOutOfRangeException) {
						return true;
				}
		}
	
		//Posiciona a bolha apos ser jogada
		public Vector2 PositionBubble (Vector2 position)
		{
				float xPosition = position.x, yPosition = position.y;

				//Posiciona o y (arredonda pro mais perto)
				float yWhole = (int)yPosition;
				float yRemainder = yPosition - yWhole;
		
				if (yRemainder > 0.5) {
						yPosition = yWhole + 0.5f;
				} else {
						yPosition = yWhole - 0.5f;
				}

				//Posiciona o x (arredonda pro mais perto)
				if (IsThisRowSmaller (PositionToRow (yPosition))) {
						float xWhole = (int)xPosition;
						float xRemainder = xPosition - xWhole;
			
						if (xRemainder > 0.5) {
								xPosition = xWhole + 1f;
						} else {
								xPosition = xWhole;
						}
				} else {
						float xWhole = (int)xPosition;
						float xRemainder = xPosition - xWhole;
			
						if (xRemainder > 0.5) {
								xPosition = xWhole + 0.5f;
						} else {
								xPosition = xWhole - 0.5f;
						}
				}
				//Debug.Log (position);
				//Debug.Log (PositiontoColumn (xPosition, PositionToRow (yPosition)));
				//Verifica se nao ha bolhas ali
				bool spot1 = IsSpotOccupied (PositionToRow (yPosition), PositiontoColumn (xPosition, PositionToRow (yPosition)));
				//Debug.Log (spot1 + " " + PositionToRow (yPosition) + " " + PositiontoColumn (xPosition, PositionToRow (yPosition)));
				//Se tem, procura novo lugar
				if (spot1) {
						return FindNewPosition (PositionToRow (yPosition), PositiontoColumn (xPosition, PositionToRow (yPosition)), position);
				} else {
						return new Vector2 (xPosition, yPosition);
				}
		}

		Vector2 FindNewPosition (int row, int column, Vector2 position)
		{
				int finalColumn = column;
				int finalRow = row;
				//Debug.Log (column);
				//Debug.Log (row);
				bool tryPos1 = IsSpotOccupied (row, column + 1);
				bool tryPos2 = IsSpotOccupied (row, column - 1);
				//Debug.Log (tryPos1);
				//Debug.Log (tryPos2);
		
				//Posiciona de acordo com as outras bolhas
				if (!tryPos1 && !tryPos2) {
						if (Mathf.Abs (ColumnToPosition (column + 1, row) - position.x) < Mathf.Abs (ColumnToPosition (column - 1, row) - position.x)) {
								finalColumn = column + 1;
						} else {
								finalColumn = column - 1;
						}
				} else if (!tryPos1 && tryPos2) {
						finalColumn = column + 1;
				} else if (tryPos1 && !tryPos2) {
						finalColumn = column - 1;
				} else {
						//Se todas as posiçoes tao ocupadas, tenta de novo uma linha pra baixo
						return FindNewPosition (row + 1, column, position);
				}
				return new Vector2 (ColumnToPosition (finalColumn, finalRow), RowToPosition (finalRow));
		}

		public void DestroyBubbles (int row, int column)
		{

				List<GameObject> bubblesToDestroy = new List<GameObject> ();
				List<int> rows = new List<int> ();
				List<int> columns = new List<int> ();

				bubblesToDestroy.Add (bubbles [row, column]);
				rows.Add (row);
				columns.Add (column);

				for (int i = 0; i < bubblesToDestroy.Count; i ++) {
						row = rows [i];
						column = columns [i];
						//Debug.Log (row + " " + column);
						//Lados
						try {
								if (this.bubbles [row, column + 1] != null && this.bubbles [row, column].name == this.bubbles [row, column + 1].name && !bubblesToDestroy.Contains (this.bubbles [row, column + 1])) {
										bubblesToDestroy.Add (bubbles [row, column + 1]);
										rows.Add (row);
										columns.Add (column + 1);
								}
						} catch (System.IndexOutOfRangeException) {
						}
						try {
								if (this.bubbles [row, column - 1] != null && this.bubbles [row, column].name == this.bubbles [row, column - 1].name && !bubblesToDestroy.Contains (this.bubbles [row, column - 1])) {
										bubblesToDestroy.Add (this.bubbles [row, column - 1]);
										rows.Add (row);
										columns.Add (column - 1);
								}
						} catch (System.IndexOutOfRangeException) {
						}
						//Embaixo
						try {
								if (this.bubbles [row + 1, column] != null && this.bubbles [row, column].name == this.bubbles [row + 1, column].name && !bubblesToDestroy.Contains (this.bubbles [row + 1, column])) {
										bubblesToDestroy.Add (this.bubbles [row + 1, column]);
										rows.Add (row + 1);
										columns.Add (column);
								}
						} catch (System.IndexOutOfRangeException) {
						}
						try {
								if (IsThisRowSmaller (row)) {
										if (this.bubbles [row + 1, column + 1] != null && this.bubbles [row, column].name == this.bubbles [row + 1, column + 1].name && !bubblesToDestroy.Contains (this.bubbles [row + 1, column + 1])) {
												bubblesToDestroy.Add (this.bubbles [row + 1, column + 1]);
												rows.Add (row + 1);
												columns.Add (column + 1);
										}
								} else {
										if (this.bubbles [row + 1, column - 1] != null && this.bubbles [row, column].name == this.bubbles [row + 1, column - 1].name && !bubblesToDestroy.Contains (this.bubbles [row + 1, column - 1])) {
												bubblesToDestroy.Add (this.bubbles [row + 1, column - 1]);
												rows.Add (row + 1);
												columns.Add (column - 1);
										}
								}
						} catch (System.IndexOutOfRangeException) {
						}
						//Em cima
						try {
								if (this.bubbles [row - 1, column] != null && this.bubbles [row, column].name == this.bubbles [row - 1, column].name && !bubblesToDestroy.Contains (this.bubbles [row - 1, column])) {
										bubblesToDestroy.Add (this.bubbles [row - 1, column]);
										rows.Add (row - 1);
										columns.Add (column);
								}
						} catch (System.IndexOutOfRangeException) {
						}
						try {
								if (IsThisRowSmaller (row)) {
										if (this.bubbles [row - 1, column + 1] != null && this.bubbles [row, column].name == this.bubbles [row - 1, column + 1].name && !bubblesToDestroy.Contains (this.bubbles [row - 1, column + 1])) {
												bubblesToDestroy.Add (this.bubbles [row - 1, column + 1]);
												rows.Add (row - 1);
												columns.Add (column + 1);
										}
								} else {
										if (this.bubbles [row - 1, column - 1] != null && this.bubbles [row, column].name == this.bubbles [row - 1, column - 1].name && !bubblesToDestroy.Contains (this.bubbles [row - 1, column - 1])) {
												bubblesToDestroy.Add (this.bubbles [row - 1, column - 1]);
												rows.Add (row - 1);
												columns.Add (column - 1);
										}
								}
						} catch (System.IndexOutOfRangeException) {
						}
				}

				if (bubblesToDestroy.Count >= 3) {
						shotBubbles = bubblesToDestroy.Count;
						for (int i = 0; i < this.bubbles.GetLength(0); i ++) {
								for (int j = 0; j < this.bubbles.GetLength(1); j ++) {
										try {
												if (bubblesToDestroy.Contains (this.bubbles [i, j])) {
														GameObject children = this.bubbles [i, j].transform.FindChild ("explosion").gameObject;
														children.SetActive (true);
														Destroy (this.bubbles [i, j], 0.5f);
												}
										} catch (System.IndexOutOfRangeException) {
										}
								}
						}
						this.audios.clip = this.destroyAudio;
						this.audios.Play ();
						Invoke ("DestroyHangingBubbles", 0.5f);
						Invoke ("LetShoot", 0.7f);
				} else {
						Invoke ("LetShoot", 0.3f);
						shotBubbles = 0;
						bubblesLeft--;
						if (bubblesAmount - bubblesLimit == bubblesLeft) {
								//Invoke ("NewRow", 0.2f);
								//NewRow ();
								bubblesAmount -= bubblesLimit;
						}
				}
				//Invoke ("DestroyHangingBubbles", 0.5f);
				//Invoke ("DestroyBugs", 1f);

				
		}

		void DestroyBugs ()
		{
				GameObject[] allBubbles = GameObject.FindGameObjectsWithTag ("Bubble");
				GameObject[] array4 = allBubbles;
				for (int k = 0; k < array4.Length; k++) {
						GameObject gameObj = array4 [k];
						int objRow = PositionToRow (gameObj.transform.position.y);
						int objColumn = PositiontoColumn (gameObj.transform.position.x, PositionToRow (gameObj.transform.position.y));
			
						if (bubbles [objRow, objColumn] == null && objRow < 10) {
								bool isConnected = IsBubbleConnected (objRow, objColumn);
								if (!isConnected) {
										Destroy (gameObj);
										fallingBubbles += 1;
										Debug.Log (objRow);
								}
						}
				}
		}
		/*void NewRow ()
		{
				
				List<List<GameObject>> temp = new List<List<GameObject>> ();
				temp = bubbles;
				bubbles.InsertRange (1, temp);

				for (int i = 0; i < bubbles.Count; i++) {
						foreach (GameObject bubble in bubbles[i]) {
								if (bubble != null) {
										bubble.gameObject.transform.position = new Vector2 (bubble.transform.position.x, bubble.transform.position.y - 1f);
								}
						}
				}
				
				int column = this.columns;
				if (IsThisRowSmaller (1)) {
						column = this.columns;
				} else {
						column -= 1;
				}
				for (int i = 0; i < column; i++) {
						GameObject instance = InstantiateBubble (1, i);
						instance.rigidbody2D.isKinematic = true;
						bubbles [0] [i] = instance;
						bubbles [0] [i].transform.position = new Vector2 (bubbles [0] [i].transform.position.x, bubbles [0] [i].transform.position.y + 1f);
				}

		}*/
		void NewRow ()
		{

				GameObject[,] temp = new GameObject[31, 20];
				Array.Copy (bubbles, 0, temp, 0, 70);
				Array.Copy (temp, 0, bubbles, 21, 70);

				/*foreach (GameObject bubble in bubbles) {
						if (bubble != null)
								bubble.transform.position = new Vector2 (bubble.transform.position.x, bubble.transform.position.y - 1);
				}*/
				/*GameObject[] allBubbles = GameObject.FindGameObjectsWithTag ("Bubble");
				for (int k = 0; k < allBubbles.Length; k++) {
						GameObject gameObject = allBubbles [k];
						gameObject.transform.position = (new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y - 1f));
				}
				Vector2 position = new Vector2 (-9, 4.5f);
				int column = this.columns;
				if (IsThisRowSmaller (1)) {
						column = this.columns;
						position.x = -9.5f;
				} else {
						column -= 1;
						position.x = -9f;
				}
*/
				/*for (int i = 0; i < column; i++) {
						GameObject instance = InstantiateBubble (1, i);
						instance.rigidbody2D.isKinematic = true;
						bubbles [0, i] = instance;
						bubbles [0, i].transform.position = new Vector2 (bubbles [0, i].transform.position.x, bubbles [0, i].transform.position.y + 1f);
				}*/

				/*for (int l = 0; l < column; l++) {
						int num = UnityEngine.Random.Range (0, this.bubblesVariety);
						GameObject gameObject2 = (GameObject)Instantiate (this.bubblesPrefab [num], position, Quaternion.identity);
						gameObject2.rigidbody2D.isKinematic = true;
						position.x = position.x + this.bubbleSize;
			
						bubbles [0, l] = gameObject2;
				}*/
				Vector2 position = new Vector2 (-9, 4.5f);
				GameObject[] allBubbles = GameObject.FindGameObjectsWithTag ("Bubble");
				GameObject[] array4 = allBubbles;
				for (int k = 0; k < array4.Length; k++) {
						GameObject gameObj = array4 [k];
						if (gameObj.transform.position.y > -6.5f)
								gameObj.transform.position = (new Vector2 (gameObj.transform.position.x, gameObj.transform.position.y - 1f));
				}
				position.y = 4.5f;
				if (!this.firstRowIsSmaller) {
						this.columns = 19;
						position.x = -9f;
				} else {
						this.columns = 20;
						position.x = -9.5f;
				}
				for (int l = 0; l < this.columns; l++) {
						int num = UnityEngine.Random.Range (0, this.bubblesVariety);
						GameObject gameObject2 = (GameObject)Instantiate (this.bubblesPrefab [num], position, Quaternion.identity);
						gameObject2.GetComponent<Rigidbody2D>().isKinematic = true;
						position.x = position.x + this.bubbleSize;
						bubbles [0, l] = gameObject2;
				}
				timesDown++;
		}

		bool firstRowIsSmaller {
				get {
						return this.timesDown % 2 != 0;
				}
		}
		void DestroyHangingBubbles ()
		{
				for (int i = 0; i < bubbles.GetLength(0); i++) {
						for (int j = 0; j < bubbles.GetLength(1); j++) {
								if (IsThisRowSmaller (i) && bubbles [i, j] != null) {
										try {
												if (bubbles [i - 1, j] == null && bubbles [i - 1, j + 1] == null) {
														IsBubbleConnected (i, j);
														Debug.Log ("calling function" + i + " " + j);
												}
										} catch (System.IndexOutOfRangeException) {
												try {
														if (bubbles [i - 1, j] == null) {
																IsBubbleConnected (i, j);
																Debug.Log ("calling function" + i + " " + j);
														}
												} catch (System.IndexOutOfRangeException) {
												}
												try {
														if (bubbles [i - 1, j + 1] == null) {
																IsBubbleConnected (i, j);
																Debug.Log ("calling function" + i + " " + j);
														}
												} catch (System.IndexOutOfRangeException) {
												}
										}
								} else if (!IsThisRowSmaller (i) && bubbles [i, j] != null) {
										try {
												if (bubbles [i - 1, j] == null && bubbles [i - 1, j - 1] == null) {
														IsBubbleConnected (i, j);
														Debug.Log ("calling function" + i + " " + j);
												}
										} catch (System.IndexOutOfRangeException) {
												try {
														if (bubbles [i - 1, j] == null) {
																IsBubbleConnected (i, j);
																Debug.Log ("calling function" + i + " " + j);
														}
												} catch (System.IndexOutOfRangeException) {
												}
												try {
														if (bubbles [i - 1, j - 1] == null) {
																IsBubbleConnected (i, j);
																Debug.Log ("calling function" + i + " " + j);
														}
												} catch (System.IndexOutOfRangeException) {
												}
										}
								}
						}
				}
				
				Points (shotBubbles, fallingBubbles);
				fallingBubbles = 0;
		}

		bool IsBubbleConnected (int row, int column)
		{
				List<GameObject> bubblesToDestroy = new List<GameObject> ();
				List<int> rows = new List<int> ();
				List<int> columns = new List<int> ();
		
				bubblesToDestroy.Add (bubbles [row, column]);
				rows.Add (row);
				columns.Add (column);

				bool isConnected = false;
		
				Debug.Log ("function called" + row + " " + column);

				for (int i = 0; i < bubblesToDestroy.Count; i ++) {
						row = rows [i];
						column = columns [i];
						
						if (row == 0) {
								isConnected = true;
						}
						//Lados
						try {
								if (this.bubbles [row, column + 1] != null && !bubblesToDestroy.Contains (this.bubbles [row, column + 1])) {
										bubblesToDestroy.Add (bubbles [row, column + 1]);
										rows.Add (row);
										columns.Add (column + 1);
								}
						} catch (System.IndexOutOfRangeException) {
								//Debug.Log ("Catch" + row + " " + column);
						}
						try {
								if (this.bubbles [row, column - 1] != null && !bubblesToDestroy.Contains (this.bubbles [row, column - 1])) {
										bubblesToDestroy.Add (this.bubbles [row, column - 1]);
										rows.Add (row);
										columns.Add (column - 1);
								}
						} catch (System.IndexOutOfRangeException) {
								//Debug.Log ("Catch" + row + " " + column);
						}
						//Embaixo
						try {
								if (this.bubbles [row + 1, column] != null && !bubblesToDestroy.Contains (this.bubbles [row + 1, column])) {
										bubblesToDestroy.Add (this.bubbles [row + 1, column]);
										rows.Add (row + 1);
										columns.Add (column);
								}
						} catch (System.IndexOutOfRangeException) {
								//Debug.Log ("Catch" + row + " " + column);
						}
						try {
								if (IsThisRowSmaller (row)) {
										if (this.bubbles [row + 1, column + 1] != null && !bubblesToDestroy.Contains (this.bubbles [row + 1, column + 1])) {
												bubblesToDestroy.Add (this.bubbles [row + 1, column + 1]);
												rows.Add (row + 1);
												columns.Add (column + 1);
										}
								} else {
										if (this.bubbles [row + 1, column - 1] != null && !bubblesToDestroy.Contains (this.bubbles [row + 1, column - 1])) {
												bubblesToDestroy.Add (this.bubbles [row + 1, column - 1]);
												rows.Add (row + 1);
												columns.Add (column - 1);
										}
								}
						} catch (System.IndexOutOfRangeException) {
								//Debug.Log ("Catch" + row + " " + column);
						}
						//Em cima
						try {
								if (this.bubbles [row - 1, column] != null && !bubblesToDestroy.Contains (this.bubbles [row - 1, column])) {
										bubblesToDestroy.Add (this.bubbles [row - 1, column]);
										rows.Add (row - 1);
										columns.Add (column);
								}
						} catch (System.IndexOutOfRangeException) {
								//Debug.Log ("Catch" + row + " " + column);
						}
						try {
								if (IsThisRowSmaller (row)) {
										if (this.bubbles [row - 1, column + 1] != null && !bubblesToDestroy.Contains (this.bubbles [row - 1, column + 1])) {
												bubblesToDestroy.Add (this.bubbles [row - 1, column + 1]);
												rows.Add (row - 1);
												columns.Add (column + 1);
										}
								} else {
										if (this.bubbles [row - 1, column - 1] != null && !bubblesToDestroy.Contains (this.bubbles [row - 1, column - 1])) {
												bubblesToDestroy.Add (this.bubbles [row - 1, column - 1]);
												rows.Add (row - 1);
												columns.Add (column - 1);
										}
								}
						} catch (System.IndexOutOfRangeException) {
								//Debug.Log ("Catch" + row + " " + column);
						}

						
				}

				Debug.Log ("is connected" + isConnected + " " + rows [0] + " " + columns [0]);
				if (!isConnected) {
						fallingBubbles += bubblesToDestroy.Count;
						for (int i = 0; i < this.bubbles.GetLength(0); i ++) {
								for (int j = 0; j < this.bubbles.GetLength(1); j ++) {
										try {
												if (bubblesToDestroy.Contains (this.bubbles [i, j])) {
														GameObject children = this.bubbles [i, j].transform.FindChild ("caindo").gameObject;
														children.SetActive (true);
														Destroy (this.bubbles [i, j], 0.5f);
												}
										} catch (System.IndexOutOfRangeException) {
												//Debug.Log ("Catch" + i + " " + j);
										}
								}
						}
						base.Invoke ("PlayFallingSound", 0.5f);
						return false;
				}
				return true;
		}
		private void PlayFallingSound ()
		{
				this.audios.clip = this.fallingAudio;
				this.audios.Play ();
		}

		private void LetShoot ()
		{
		
				FindObjectOfType<Shoot> ().arrived = true;
		}
		public void Points (int shotBubbles, int extraBubbles)
		{
				int num = shotBubbles;
				this.turnPoints = 0;
				if (shotBubbles >= 6) {
						this.combo = true;
				}
				while (shotBubbles - 3 >= 0) {
						this.turnPoints += this.basePoints;
						shotBubbles -= 3;
				}
				int num2 = shotBubbles;
				this.turnPoints += num2 * 25;
				this.turnPoints += extraBubbles * 50;
				this.turnPoints *= this.pointsModifier;
				this.totalPoints += this.turnPoints;
				if (num >= 10 || this.turnPoints >= 2000) {
						life++;
				}
				if (this.combo) {
						this.pointsModifier *= 2;
				}
				this.combo = false;
				/*if (this.pointsModifier >= 8) {
						this.challenge1Counter++;
						if (this.challenge1Counter >= 4) {
								this.challenge1 = true;
						}
				} else {
						this.challenge1Counter = 0;
				}*/
				//	Debug.Log (totalPoints);
		}

		public List<string> CheckIfColor ()
		{
				List<string> colors = new List<string> ();
				for (int i = 0; i < bubbles.GetLength(0); i++) {
						for (int j = 0; j < bubbles.GetLength(1); j ++) {

								if (bubbles [i, j] != null && !colors.Contains (bubbles [i, j].name)) {
										colors.Add (bubbles [i, j].name);
								}
								
						}
				}

				return colors;
				
		}
}
