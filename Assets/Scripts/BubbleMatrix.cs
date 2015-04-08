using System;
using System.Collections.Generic;
using UnityEngine;

public class BubbleMatrix : MonoBehaviour
{
		public GameObject[] bubbles;
		public GameObject[] bubbleExplosions;
		[HideInInspector]
		public static bool[,]
				table = new bool[31, 20];
		public static string[,] colors = new string[31, 20];
		public static GameObject[,] tableObjects = new GameObject[31, 20];
		[HideInInspector]
		public int
				bubblesVariety;
		[HideInInspector]
		public int
				ballsAmount;
		[HideInInspector]
		public int
				ballsLimit;
		[HideInInspector]
		public int
				rows;
		public bool win;
		public int totalPoints;
		public AudioClip destroyAudio;
		public AudioClip fallingAudio;
		private int columns;
		private int fullRowColumn = 20;
		private float bubbleSize = 1f;
		private float rowDistance = 1f;
		private Vector2 position;
		private bool fullRow = true;
		private int basePoints = 100;
		private int points;
		private int pointsModifier = 1;
		private bool combo;
		private int bubblesAmount;
		private int fallingBubbles;
		private static int life = 5;
		private GameObject[] allBubbles;
		private int timesDown;
		private int ballsLeft;
		private bool challenge1;
		private int challenge1Counter;
		private AudioSource audio;
		public bool firstRowIsSmaller {
				get {
						return this.timesDown % 2 != 0;
				}
		}
		private void Start ()
		{
				this.position = new Vector2 (-9.5f, 4.5f);
				for (int i = 0; i < BubbleMatrix.tableObjects.GetLength(0); i++) {
						for (int j = 0; j < BubbleMatrix.tableObjects.GetLength(1); j++) {
								BubbleMatrix.table [i, j] = false;
								BubbleMatrix.colors [i, j] = null;
								BubbleMatrix.tableObjects [i, j] = null;
						}
				}
				this.ballsLeft = this.ballsAmount;
				this.BuildMatrix ();
				this.audio = base.GetComponent<AudioSource> ();
		}
		private void BuildMatrix ()
		{
				for (int i = 0; i < this.rows; i++) {
						if (this.fullRow) {
								this.position.x = -9.5f;
								this.columns = this.fullRowColumn;
						} else {
								this.position.x = -9f;
								this.columns = this.fullRowColumn - 1;
						}
						for (int j = 0; j < this.columns; j++) {
								int num = UnityEngine.Random.Range (0, this.bubblesVariety);
								GameObject gameObject = (GameObject)Instantiate (this.bubbles [num], this.position, Quaternion.identity);
								gameObject.name = bubbles [num].name;
								gameObject.rigidbody2D.isKinematic = true;
								this.position.x = this.position.x + this.bubbleSize;
								BubbleMatrix.table [i, j] = true;
								BubbleMatrix.colors [i, j] = this.bubbles [num].name;
								BubbleMatrix.tableObjects [i, j] = gameObject;
						}
						this.position.y = this.position.y - this.rowDistance;
						this.fullRow = !this.fullRow;
				}
		}
		private void Update ()
		{
				if (Input.GetKeyDown ("q")) {
						for (int i = 0; i < 9; i++) {
								for (int j = 0; j < BubbleMatrix.tableObjects.GetLength(1); j++) {
										if (!BubbleMatrix.table [i, j]) {
												Debug.Log (i + "\t" + j);
										}
								}
						}
				}
				if (this.ballsLeft <= 0) {
						BubbleMatrix.life--;
						if (this.challenge1) {
								BubbleMatrix.life++;
						}
						Debug.Log (BubbleMatrix.life);
						Application.LoadLevel (Application.loadedLevel);
				}
				this.Win ();
		}
		public void Win ()
		{
				int num = 0;
				for (int i = 0; i < BubbleMatrix.table.GetLength(0); i++) {
						for (int j = 0; j < BubbleMatrix.table.GetLength(1); j++) {
								if (BubbleMatrix.table [i, j]) {
										num++;
								}
						}
				}
				if (num == 0) {
						this.win = true;
						Debug.Log ("win");
				}
		}
		public bool IsSpotOccupied (int row, int column)
		{
				Debug.Log (row + " " + column);
				if (row >= 0 && row <= BubbleMatrix.table.GetLength (0) - 1 && column >= 0 && column <= BubbleMatrix.table.GetLength (1) - 1) {
						Debug.Log ("sup " + BubbleMatrix.table [row, column]);
						return BubbleMatrix.table [row, column];
				}
				return false;
		}
		public bool IsConnectedOnTop (int row, int column)
		{
				if (row < 1) {
						return true;
				}
				if ((this.firstRowIsSmaller && row % 2 == 0) || (!this.firstRowIsSmaller && row % 2 != 0)) {
						bool flag = this.IsSpotOccupied (row - 1, column);
						bool flag2 = this.IsSpotOccupied (row - 1, column + 1);
						if (flag || flag2) {
								Debug.Log ("top left " + (row - 1) + " " + column);
								Debug.Log ("top right " + (row - 1) + "  " + (column + 1));
								return true;
						}
						return false;
				} else {
						bool flag3 = this.IsSpotOccupied (row - 1, column);
						bool flag4 = this.IsSpotOccupied (row - 1, column - 1);
						if (flag3 || flag4) {
								return true;
						}
						return false;
				}
		}
		public static int GetRow (float y)
		{
				return (int)(4.5f - y);
		}
		public int GetColumn (float x, int row)
		{
				int result;
				if ((this.firstRowIsSmaller && row % 2 == 0) || (!this.firstRowIsSmaller && row % 2 != 0)) {
						result = (int)Mathf.Abs (-9f - x);
				} else {
						result = (int)Mathf.Abs (-9.5f - x);
				}
				return result;
		}
		public float SetColumn (float x, float y)
		{
				float result = (float)((int)x);
				for (int i = 0; i < 2; i++) {
						int row = BubbleMatrix.GetRow (y);
						if ((!this.firstRowIsSmaller && row % 2 == 0) || (this.firstRowIsSmaller && row % 2 != 0)) {
								float num = 0.5f;
								float num2 = (float)((int)x) + num;
								int column = (int)(num2 + 9.5f);
								float num3 = (float)((int)x) - num;
								int column2 = (int)(num3 + 9.5f);
								bool flag = this.IsSpotOccupied (row, column);
								bool flag2 = this.IsSpotOccupied (row, column2);
								bool flag3 = this.IsConnectedOnTop (row, column);
								bool flag4 = this.IsConnectedOnTop (row, column2);
								Debug.Log ("top1" + flag3);
								Debug.Log ("top2" + flag4);
								if (!flag && !flag2) {
										if (Mathf.Abs (num2 - x) < Mathf.Abs (num3 - x) && flag3) {
												result = num2;
										} else {
												if (Mathf.Abs (num2 - x) > Mathf.Abs (num3 - x) && flag4) {
														result = num3;
												}
										}
								} else {
										if (flag && !flag2 && flag4) {
												result = num3;
										} else {
												if (flag2 && !flag && flag3) {
														result = num2;
												} else {
														if (flag && flag2) {
																Debug.Log (num2 + "  " + num3);
																y -= 1f;
																FindObjectOfType<Bubble> ().positionY -= 1f;
														}
												}
										}
								}
						} else {
								float num = 1f;
								float num4 = (float)((int)x) + num;
								int column3 = (int)(num4 + 9f);
								float num5 = (float)((int)x) - num;
								int column4 = (int)(num5 + 9f);
								float num6 = (float)((int)x);
								int column5 = (int)(num6 + 9f);
								bool flag5 = this.IsSpotOccupied (row, column3);
								bool flag6 = this.IsSpotOccupied (row, column4);
								bool flag7 = this.IsSpotOccupied (row, column5);
								bool flag8 = this.IsConnectedOnTop (row, column3);
								bool flag9 = this.IsConnectedOnTop (row, column4);
								bool flag10 = this.IsConnectedOnTop (row, column5);
								Debug.Log ("top1" + flag8);
								Debug.Log ("top2" + flag9);
								Debug.Log ("top3" + flag10);
								if (!flag7 && flag10) {
										result = num6;
								} else {
										if (flag7 && !flag5 && !flag6) {
												if (Mathf.Abs (num4 - x) < Mathf.Abs (num5 - x) && flag8) {
														result = num4;
												} else {
														if (Mathf.Abs (num4 - x) > Mathf.Abs (num5 - x) && flag9) {
																result = num5;
														}
												}
										} else {
												if (flag7 && flag5 && !flag6 && flag9) {
														result = num5;
												} else {
														if (flag7 && !flag5 && flag6 && flag8) {
																result = num4;
														} else {
																if (flag5 && flag6 && flag7) {
																		Debug.Log (string.Concat (new object[]
									{
										num4,
										"  ",
										num5,
										"   ",
										num6
									}));
																		y -= 1f;
																		FindObjectOfType<Bubble> ().positionY -= 1f;
																}
														}
												}
										}
								}
						}
				}
				return result;
		}
		public void DestroyBubbles (int row, int column)
		{
				List<GameObject> list = new List<GameObject> ();
				List<int> list2 = new List<int> ();
				List<int> list3 = new List<int> ();
				list.Add (BubbleMatrix.tableObjects [row, column]);
				list2.Add (row);
				list3.Add (column);
				for (int i = 0; i < list.Count; i++) {
						row = list2 [i];
						column = list3 [i];
						if (column + 1 <= BubbleMatrix.tableObjects.GetLength (1) - 1 && BubbleMatrix.colors [row, column] == BubbleMatrix.colors [row, column + 1] && !list.Contains (BubbleMatrix.tableObjects [row, column + 1])) {
								list.Add (BubbleMatrix.tableObjects [row, column + 1]);
								list2.Add (row);
								list3.Add (column + 1);
						}
						if (column - 1 >= 0 && BubbleMatrix.colors [row, column] == BubbleMatrix.colors [row, column - 1] && !list.Contains (BubbleMatrix.tableObjects [row, column - 1])) {
								list.Add (BubbleMatrix.tableObjects [row, column - 1]);
								list2.Add (row);
								list3.Add (column - 1);
						}
						if (row + 1 <= BubbleMatrix.tableObjects.GetLength (0) - 1 && BubbleMatrix.colors [row, column] == BubbleMatrix.colors [row + 1, column] && !list.Contains (BubbleMatrix.tableObjects [row + 1, column])) {
								list.Add (BubbleMatrix.tableObjects [row + 1, column]);
								list2.Add (row + 1);
								list3.Add (column);
						}
						if ((this.firstRowIsSmaller && row % 2 == 0) || (!this.firstRowIsSmaller && row % 2 != 0)) {
								if (row + 1 <= BubbleMatrix.tableObjects.GetLength (0) - 1 && column + 1 <= BubbleMatrix.tableObjects.GetLength (1) - 1 && BubbleMatrix.colors [row, column] == BubbleMatrix.colors [row + 1, column + 1] && !list.Contains (BubbleMatrix.tableObjects [row + 1, column + 1])) {
										list.Add (BubbleMatrix.tableObjects [row + 1, column + 1]);
										list2.Add (row + 1);
										list3.Add (column + 1);
								}
						} else {
								if (row + 1 <= BubbleMatrix.tableObjects.GetLength (0) - 1 && column - 1 >= 0 && BubbleMatrix.colors [row, column] == BubbleMatrix.colors [row + 1, column - 1] && !list.Contains (BubbleMatrix.tableObjects [row + 1, column - 1])) {
										list.Add (BubbleMatrix.tableObjects [row + 1, column - 1]);
										list2.Add (row + 1);
										list3.Add (column - 1);
								}
						}
						if (row - 1 >= 0 && BubbleMatrix.colors [row, column] == BubbleMatrix.colors [row - 1, column] && !list.Contains (BubbleMatrix.tableObjects [row - 1, column])) {
								list.Add (BubbleMatrix.tableObjects [row - 1, column]);
								list2.Add (row - 1);
								list3.Add (column);
						}
						if ((this.firstRowIsSmaller && row % 2 == 0) || (!this.firstRowIsSmaller && row % 2 != 0)) {
								if (row - 1 >= 0 && column + 1 <= BubbleMatrix.tableObjects.GetLength (1) - 1 && BubbleMatrix.colors [row, column] == BubbleMatrix.colors [row - 1, column + 1] && !list.Contains (BubbleMatrix.tableObjects [row - 1, column + 1])) {
										list.Add (BubbleMatrix.tableObjects [row - 1, column + 1]);
										list2.Add (row - 1);
										list3.Add (column + 1);
								}
						} else {
								if (row - 1 >= 0 && column - 1 >= 0 && BubbleMatrix.colors [row, column] == BubbleMatrix.colors [row - 1, column - 1] && !list.Contains (BubbleMatrix.tableObjects [row - 1, column - 1])) {
										list.Add (BubbleMatrix.tableObjects [row - 1, column - 1]);
										list2.Add (row - 1);
										list3.Add (column - 1);
								}
						}
				}
				if (list.Count >= 3) {
						this.bubblesAmount = list.Count;
						for (int j = 0; j < list.Count; j++) {
								for (int k = 0; k < BubbleMatrix.tableObjects.GetLength(0); k++) {
										for (int l = 0; l < BubbleMatrix.tableObjects.GetLength(1); l++) {
												if (BubbleMatrix.tableObjects [k, l] == list [j]) {
														GameObject children = tableObjects [k, l].transform.FindChild ("explosion").gameObject;
														children.SetActive (true);
														BubbleMatrix.table [k, l] = false;
														BubbleMatrix.colors [k, l] = null;
														Destroy (BubbleMatrix.tableObjects [k, l], 0.5f);
												}
										}
								}
						}
						this.audio.clip = this.destroyAudio;
						this.audio.Play ();
				} else {
						this.ballsLeft--;
						this.bubblesAmount = 0;
						this.combo = false;
						this.pointsModifier = 1;
						if (this.ballsAmount - this.ballsLimit == this.ballsLeft) {
								this.NewRow ();
								this.ballsAmount -= this.ballsLimit;
						}
				}
				this.DestroyHangingBubbles ();
		}

		public void NewRow ()
		{
				bool[,] array = new bool[31, 20];
				Array.Copy (BubbleMatrix.table, 0, array, 0, 100);
				Array.Copy (array, 0, BubbleMatrix.table, 20, 100);
				GameObject[,] array2 = new GameObject[31, 20];
				Array.Copy (BubbleMatrix.tableObjects, 0, array2, 0, 100);
				Array.Copy (array2, 0, BubbleMatrix.tableObjects, 20, 100);
				string[,] array3 = new string[31, 20];
				Array.Copy (BubbleMatrix.colors, 0, array3, 0, 100);
				Array.Copy (array3, 0, BubbleMatrix.colors, 20, 100);
				for (int i = 0; i < 9; i++) {
						for (int j = 0; j < 20; j++) {
								if (!BubbleMatrix.table [i, j]) {
										Debug.Log (i + "\t" + j);
								}
						}
				}
				this.allBubbles = GameObject.FindGameObjectsWithTag ("Bubble");
				GameObject[] array4 = this.allBubbles;
				for (int k = 0; k < array4.Length; k++) {
						GameObject gameObject = array4 [k];
						gameObject.transform.position = (new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y - 1f));
				}
				this.position.y = 4.5f;
				if (!this.firstRowIsSmaller) {
						this.columns = 19;
						this.position.x = -9f;
				} else {
						this.columns = 20;
						this.position.x = -9.5f;
				}
				for (int l = 0; l < this.columns; l++) {
						int num = UnityEngine.Random.Range (0, this.bubblesVariety);
						GameObject gameObject2 = (GameObject)Instantiate (this.bubbles [num], this.position, Quaternion.identity);
						gameObject2.rigidbody2D.isKinematic = true;
						this.position.x = this.position.x + this.bubbleSize;
						BubbleMatrix.table [0, l] = true;
						BubbleMatrix.colors [0, l] = this.bubbles [num].name;
						BubbleMatrix.tableObjects [0, l] = gameObject2;
				}
				this.timesDown++;
		}
		public void Points (int bubblesAmount, int extraBubbles)
		{
				int num = bubblesAmount;
				this.points = 0;
				if (bubblesAmount >= 6) {
						this.combo = true;
				}
				while (bubblesAmount - 3 >= 0) {
						this.points += this.basePoints;
						bubblesAmount -= 3;
				}
				int num2 = bubblesAmount;
				this.points += num2 * 25;
				this.points += extraBubbles * 50;
				this.points *= this.pointsModifier;
				this.totalPoints += this.points;
				if (num >= 10 || this.points >= 2000) {
						BubbleMatrix.life++;
				}
				if (this.combo) {
						this.pointsModifier *= 2;
				}
				this.combo = false;
				if (this.pointsModifier >= 8) {
						this.challenge1Counter++;
						if (this.challenge1Counter >= 4) {
								this.challenge1 = true;
						}
				} else {
						this.challenge1Counter = 0;
				}
		}
		public void DestroyHangingBubbles ()
		{
				for (int i = 0; i < BubbleMatrix.table.GetLength(0); i++) {
						for (int j = 0; j < BubbleMatrix.table.GetLength(1); j++) {
								if (((this.firstRowIsSmaller && i % 2 == 0) || (!this.firstRowIsSmaller && i % 2 != 0)) && BubbleMatrix.table [i, j] && i - 1 >= 0 && j + 1 <= BubbleMatrix.table.GetLength (1) && !BubbleMatrix.table [i - 1, j] && !BubbleMatrix.table [i - 1, j + 1]) {
										this.IsBubbleConnected (i, j);
								} else {
										if (((!this.firstRowIsSmaller && i % 2 == 0) || (this.firstRowIsSmaller && i % 2 != 0)) && BubbleMatrix.table [i, j] && i - 1 >= 0 && j - 1 >= 0 && !BubbleMatrix.table [i - 1, j] && !BubbleMatrix.table [i - 1, j - 1]) {
												this.IsBubbleConnected (i, j);
										}
								}
						}
				}
				this.Points (this.bubblesAmount, this.fallingBubbles);
				this.fallingBubbles = 0;
		}
		public bool IsBubbleConnected (int row, int column)
		{
				List<GameObject> list = new List<GameObject> ();
				List<int> list2 = new List<int> ();
				List<int> list3 = new List<int> ();
				list.Add (BubbleMatrix.tableObjects [row, column]);
				list2.Add (row);
				list3.Add (column);
				bool flag = false;
				for (int i = 0; i < list.Count; i++) {
						row = list2 [i];
						column = list3 [i];
						if (row == 0) {
								flag = true;
						}
						if (column + 1 <= BubbleMatrix.tableObjects.GetLength (1) - 1 && BubbleMatrix.table [row, column + 1] && !list.Contains (BubbleMatrix.tableObjects [row, column + 1])) {
								list.Add (BubbleMatrix.tableObjects [row, column + 1]);
								list2.Add (row);
								list3.Add (column + 1);
						}
						if (column - 1 >= 0 && BubbleMatrix.table [row, column - 1] && !list.Contains (BubbleMatrix.tableObjects [row, column - 1])) {
								list.Add (BubbleMatrix.tableObjects [row, column - 1]);
								list2.Add (row);
								list3.Add (column - 1);
						}
						if (row + 1 <= BubbleMatrix.tableObjects.GetLength (0) - 1 && BubbleMatrix.table [row + 1, column] && !list.Contains (BubbleMatrix.tableObjects [row + 1, column])) {
								list.Add (BubbleMatrix.tableObjects [row + 1, column]);
								list2.Add (row + 1);
								list3.Add (column);
						}
						if ((this.firstRowIsSmaller && row % 2 == 0) || (!this.firstRowIsSmaller && row % 2 != 0)) {
								if (row + 1 <= BubbleMatrix.tableObjects.GetLength (0) - 1 && column + 1 <= BubbleMatrix.tableObjects.GetLength (1) - 1 && BubbleMatrix.table [row + 1, column + 1] && !list.Contains (BubbleMatrix.tableObjects [row + 1, column + 1])) {
										list.Add (BubbleMatrix.tableObjects [row + 1, column + 1]);
										list2.Add (row + 1);
										list3.Add (column + 1);
								}
						} else {
								if (row + 1 <= BubbleMatrix.tableObjects.GetLength (0) - 1 && column - 1 >= 0 && BubbleMatrix.table [row + 1, column - 1] && !list.Contains (BubbleMatrix.tableObjects [row + 1, column - 1])) {
										list.Add (BubbleMatrix.tableObjects [row + 1, column - 1]);
										list2.Add (row + 1);
										list3.Add (column - 1);
								}
						}
						if (row - 1 >= 0 && BubbleMatrix.table [row - 1, column] && !list.Contains (BubbleMatrix.tableObjects [row - 1, column])) {
								list.Add (BubbleMatrix.tableObjects [row - 1, column]);
								list2.Add (row - 1);
								list3.Add (column);
						}
						if ((this.firstRowIsSmaller && row % 2 == 0) || (!this.firstRowIsSmaller && row % 2 != 0)) {
								if (row - 1 >= 0 && column + 1 <= BubbleMatrix.tableObjects.GetLength (1) - 1 && BubbleMatrix.table [row - 1, column + 1] && !list.Contains (BubbleMatrix.tableObjects [row - 1, column + 1])) {
										list.Add (BubbleMatrix.tableObjects [row - 1, column + 1]);
										list2.Add (row - 1);
										list3.Add (column + 1);
								}
						} else {
								if (row - 1 >= 0 && column - 1 >= 0 && BubbleMatrix.table [row - 1, column - 1] && !list.Contains (BubbleMatrix.tableObjects [row - 1, column - 1])) {
										list.Add (BubbleMatrix.tableObjects [row - 1, column - 1]);
										list2.Add (row - 1);
										list3.Add (column - 1);
								}
						}
				}
				if (!flag) {
						this.fallingBubbles += list.Count;
						for (int j = 0; j < list.Count; j++) {
								for (int k = 0; k < BubbleMatrix.tableObjects.GetLength(0); k++) {
										for (int l = 0; l < BubbleMatrix.tableObjects.GetLength(1); l++) {
												if (BubbleMatrix.tableObjects [k, l] == list [j]) {
														BubbleMatrix.table [k, l] = false;
														BubbleMatrix.colors [k, l] = null;
														Destroy (BubbleMatrix.tableObjects [k, l], 1f);
												}
										}
								}
						}
						base.Invoke ("PlayFallingSound", 1f);
						return false;
				}
				return true;
		}
		private void PlayFallingSound ()
		{
				this.audio.clip = this.fallingAudio;
				this.audio.Play ();
		}
}
