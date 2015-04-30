using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Helper : MonoBehaviour
{
	private NewGame game;
	private GameController gameController;

	public bool hitBubbles = false;
	public int shotBubbles = 0, extraBubbles = 0;
	public AudioSource explosionAudioClip, fallingAudioClip;

	void Start ()
	{
		game = GetComponent<NewGame> ();
		gameController = GetComponent<GameController> ();
	}

	//Cheio de debugs
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.A)) {
			Debug.Log (IsConnectedToTop (3, 6));
		}
		if (Input.GetKeyDown (KeyCode.S)) {
			game.matrix.bubbleMatrix [2, 6] = null;
			game.matrix.bubbleMatrix [2, 7] = null;
			game.matrix.bubbleMatrix [3, 5] = null;
			game.matrix.bubbleMatrix [3, 7] = null;
			game.matrix.bubbleMatrix [4, 6] = null;
			game.matrix.bubbleMatrix [4, 7] = null;
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			NewRow ();
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			for (int i = 0; i < game.matrix.bubbleMatrix.GetLength(0); i++) {
				for (int j = 0; j < game.matrix.bubbleMatrix.GetLength(1); j++) {
					try {
						Debug.Log (game.matrix.bubbleMatrix [i, j].getColor ());
					} catch (System.NullReferenceException) {
						Debug.Log ("Null");
					}
				}
			}
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			ShootableColor ();
		}
	}

	//Controla as mecanicas da jogada
	public void TakeATurn (Bubbles bubble)
	{
		Vector2 bubblePosition = bubble.bubbleObject.transform.position;
		int row = PositionToRow (bubblePosition);
		int column = PositionToColumn (bubblePosition, row);
		//Debug.Log(row + "\t" + column);

		//Se a posição for válida
		if (IsPositionValid (row, column) && IsConnectedToTop (row, column)) {
			game.matrix.bubbleMatrix [row, column] = bubble;
			if (IsRowFull (row)) {
				bubble.bubbleObject.transform.position = new Vector2 (column - 9.5f, 4.5f - row);
			} else {
				bubble.bubbleObject.transform.position = new Vector2 (column - 9f, 4.5f - row);
			}

		} else {
			// Debug.Log(IsPositionValid(row, column) + "\t" + IsConnectedToTop(row, column));
			FindNewValidPosition (row, column, bubblePosition, bubble);
		}

		//Debug.Log(game.matrix.bubbleMatrix[row, column].getColor());
		DestroyTrios (row, column);
		DestroyHangingBubbles ();

		gameController.AfterATurn ();
	}

	//Diz se a linha é das maiores
	bool IsRowFull (int row)
	{
		if ((Matrix.isFirstRowFull && row % 2 == 0) || (!Matrix.isFirstRowFull && row % 2 != 0)) {
			return true;
		} else {
			return false;
		}
	}

	//Da a linha de acordo com a posição
	int PositionToRow (Vector2 bubblePosition)
	{
		int row = 0;

		if (bubblePosition.y - 0.2f > 0) {
			row = 4 - (int)(bubblePosition.y - 0.2f);
		} else {
			row = 4 - (int)(bubblePosition.y - 0.2f) + 1;
		}
		return row;
	}

	//Da a coluna de acordo com a posição
	int PositionToColumn (Vector2 bubblePosition, int row)
	{
		int column = 0;

		//Se for uma das colunas maiores
		if ((Matrix.isFirstRowFull && row % 2 == 0) || (!Matrix.isFirstRowFull && row % 2 != 0)) {
			//Antes do 0x
			if (bubblePosition.x < 0) {
				column = 9 + (int)bubblePosition.x;
			}
            //Depois do 0x
            else {
				column = 10 + (int)bubblePosition.x;
			}
		}
        //Se for uma das colunas menores
        else {
			float decimals = bubblePosition.x - (int)bubblePosition.x;
			//Antes do 0.5x
			if (bubblePosition.x < 0) {
				//Posiciona de acordo com os decimais
				if (decimals < -0.5) {
					column = 9 + (int)(bubblePosition.x - 1);
				} else {
					column = 9 + (int)bubblePosition.x;
				}
			}
            //Entre -0.5 e 0.5 no x
            else if (bubblePosition.x <= -0.5f && bubblePosition.x >= 0.5f) {
				column = 9;
			}
            //Depois de 0.5x
            else {
				if (decimals < 0.5) {
					column = 9 + (int)bubblePosition.x;
				} else {
					column = 9 + (int)(bubblePosition.x + 1);
				}
			}
		}

		return column;
	}

	//Da a posiçao de acordo com a linha
	float RowToPosition (int row)
	{
		float position = 0;
		position = 4.5f - row;
		return position;
	}

	//Da a posiçao de acordo com a coluna
	float ColumnToPosition (int column, int row)
	{
		float position = 0;
		if (IsRowFull (row)) {
			position = column - 9.5f;
		} else {
			position = column - 9f;
		}
		return position;
	}

	//Verifica se essa posiçao é valida
	bool IsPositionValid (int row, int column)
	{
		try {
			if (game.matrix.bubbleMatrix [row, column] != null) {
				return false;
			} else {
				return true;
			}
		} catch (System.IndexOutOfRangeException) {
			return false;
		}
	}

	//Verifica se essa bolha esta conectada ao topo (nao pendurada)
	bool IsConnectedToTop (int row, int column)
	{
		bool isConnected = false;
		List<Bubbles> bubbleHelperList = new List<Bubbles> ();
		List<int> rows = new List<int> ();
		List<int> columns = new List<int> ();

		bubbleHelperList.Add (game.matrix.bubbleMatrix [row, column]);
		rows.Add (row);
		columns.Add (column);

		// Debug.Log(row + "\t" + column);

		for (int i = 0; i < bubbleHelperList.Count; i++) {
			row = rows [i];
			column = columns [i];

			//Se a linha for a primeira ja da verdadeiro
			if (row == 0) {
				isConnected = true;
				break;
			}

			//Põe todas as bolhas que encostam numa lista e verifica a lista toda
			//Antes assim que achava uma bolha verificava ela, mas não funciona

			//Topo
			try {
				if (game.matrix.bubbleMatrix [row - 1, column] != null && !bubbleHelperList.Contains (game.matrix.bubbleMatrix [row - 1, column])) {
					bubbleHelperList.Add (game.matrix.bubbleMatrix [row - 1, column]);
					rows.Add (row - 1);
					columns.Add (column);
				}
			} catch (System.IndexOutOfRangeException) {
			}
			try {
				if (IsRowFull (row)) {
					if (game.matrix.bubbleMatrix [row - 1, column - 1] != null && !bubbleHelperList.Contains (game.matrix.bubbleMatrix [row - 1, column - 1])) {
						bubbleHelperList.Add (game.matrix.bubbleMatrix [row - 1, column - 1]);
						rows.Add (row - 1);
						columns.Add (column - 1);
					}
				} else {
					if (game.matrix.bubbleMatrix [row - 1, column + 1] != null && !bubbleHelperList.Contains (game.matrix.bubbleMatrix [row - 1, column + 1])) {
						bubbleHelperList.Add (game.matrix.bubbleMatrix [row - 1, column + 1]);
						rows.Add (row - 1);
						columns.Add (column + 1);
					}
				}
			} catch (System.IndexOutOfRangeException) {
			}

			//Lados
			try {
				if (game.matrix.bubbleMatrix [row, column - 1] != null && !bubbleHelperList.Contains (game.matrix.bubbleMatrix [row, column - 1])) {
					bubbleHelperList.Add (game.matrix.bubbleMatrix [row, column - 1]);
					rows.Add (row);
					columns.Add (column - 1);
				}
			} catch (System.IndexOutOfRangeException) {
			}
			try {
				if (game.matrix.bubbleMatrix [row, column + 1] != null && !bubbleHelperList.Contains (game.matrix.bubbleMatrix [row, column + 1])) {
					bubbleHelperList.Add (game.matrix.bubbleMatrix [row, column + 1]);
					rows.Add (row);
					columns.Add (column + 1);
				}
			} catch (System.IndexOutOfRangeException) {
			}

			//Embaixo
			try {
				if (game.matrix.bubbleMatrix [row + 1, column] != null && !bubbleHelperList.Contains (game.matrix.bubbleMatrix [row + 1, column])) {
					bubbleHelperList.Add (game.matrix.bubbleMatrix [row + 1, column]);
					rows.Add (row + 1);
					columns.Add (column);
				}
			} catch (System.IndexOutOfRangeException) {
			}
			try {
				if (IsRowFull (row)) {
					if (game.matrix.bubbleMatrix [row + 1, column - 1] != null && !bubbleHelperList.Contains (game.matrix.bubbleMatrix [row + 1, column - 1])) {
						bubbleHelperList.Add (game.matrix.bubbleMatrix [row + 1, column - 1]);
						rows.Add (row + 1);
						columns.Add (column - 1);
					}
				} else {
					if (game.matrix.bubbleMatrix [row + 1, column + 1] != null && !bubbleHelperList.Contains (game.matrix.bubbleMatrix [row + 1, column + 1])) {
						bubbleHelperList.Add (game.matrix.bubbleMatrix [row + 1, column + 1]);
						rows.Add (row + 1);
						columns.Add (column + 1);
					}
				}
			} catch (System.IndexOutOfRangeException) {
			}
		}

		/*if (isConnected == false)
        {
            Debug.Log(isConnected);
        }*/

		return isConnected;
	}

	//Acha uma nova posiçao valida
	void FindNewValidPosition (int firstRow, int firstColumn, Vector2 bubblePosition, Bubbles bubble)
	{

		int newRow = firstRow, newColumn = firstColumn;
		Vector2 newPosition = new Vector2 (ColumnToPosition (firstColumn, firstRow), RowToPosition (firstRow));

		//Verifica se a posiçao valida e conectada e para o lado, para o outro, e/ou para baixo
		//Se a nova posiçao existir na grid, se for valida (nao tem bolha la e esta conectada ao topo), e se for menor que a anterior, vira a nova posiçao

		//Direita
		if (((IsRowFull (firstRow) && firstColumn < 19) || (!IsRowFull (firstRow) && firstColumn < 18)) &&
			((IsPositionValid (firstRow, firstColumn + 1)) && IsConnectedToTop (firstRow, firstColumn + 1))) {
			newPosition = new Vector2 (ColumnToPosition (firstColumn + 1, firstRow), RowToPosition (firstRow));
			Debug.Log ("Direita");
		}

		//Esquerda
		if ((firstColumn > 0) &&
			((IsPositionValid (firstRow, firstColumn - 1)) && IsConnectedToTop (firstRow, firstColumn - 1))
			&& (Vector2.Distance (bubblePosition, new Vector2 (ColumnToPosition (firstColumn - 1, firstRow), RowToPosition (firstRow))) < Vector2.Distance (bubblePosition, newPosition))) {
			newPosition = new Vector2 (ColumnToPosition (firstColumn - 1, firstRow), RowToPosition (firstRow));
			Debug.Log ("Esquerda");
		}

		//Embaixo / direita
		if (((firstRow < 10) && (IsRowFull (firstRow + 1) && firstColumn < 19) || (!IsRowFull (firstRow + 1) && firstColumn < 18)) &&
			((IsPositionValid (firstRow + 1, firstColumn + 1)) && IsConnectedToTop (firstRow + 1, firstColumn + 1))
			&& (Vector2.Distance (bubblePosition, new Vector2 (ColumnToPosition (firstColumn + 1, firstRow + 1), RowToPosition (firstRow + 1))) < Vector2.Distance (bubblePosition, newPosition))) {
			newPosition = new Vector2 (ColumnToPosition (firstColumn + 1, firstRow + 1), RowToPosition (firstRow + 1));
			Debug.Log ("Embaixo / direita");
		}

		//Embaixo / esquerda
		if (((firstRow < 10) && (firstColumn > 0)) &&
			((IsPositionValid (firstRow + 1, firstColumn - 1)) && IsConnectedToTop (firstRow + 1, firstColumn - 1))
			&& (Vector2.Distance (bubblePosition, new Vector2 (ColumnToPosition (firstColumn - 1, firstRow + 1), RowToPosition (firstRow + 1))) < Vector2.Distance (bubblePosition, newPosition))) {
			newPosition = new Vector2 (ColumnToPosition (firstColumn - 1, firstRow + 1), RowToPosition (firstRow + 1));
			Debug.Log ("Embaixo / esquerda");
		}

		//Acima / direita
		if (((firstRow > 0) && (IsRowFull (firstRow - 1) && firstColumn < 19) || (!IsRowFull (firstRow - 1) && firstColumn < 18)) &&
			((IsPositionValid (firstRow - 1, firstColumn + 1)) && IsConnectedToTop (firstRow - 1, firstColumn + 1))
			&& (Vector2.Distance (bubblePosition, new Vector2 (ColumnToPosition (firstColumn + 1, firstRow - 1), RowToPosition (firstRow - 1))) < Vector2.Distance (bubblePosition, newPosition))) {
			newPosition = new Vector2 (ColumnToPosition (firstColumn + 1, firstRow - 1), RowToPosition (firstRow - 1));
			Debug.Log ("Acima / direita");
		}

		//Acima / esquerda
		if (((firstRow > 0) && (firstColumn > 0)) &&
			((IsPositionValid (firstRow - 1, firstColumn - 1)) && IsConnectedToTop (firstRow - 1, firstColumn - 1))
			&& (Vector2.Distance (bubblePosition, new Vector2 (ColumnToPosition (firstColumn - 1, firstRow - 1), RowToPosition (firstRow - 1))) < Vector2.Distance (bubblePosition, newPosition))) {
			newPosition = new Vector2 (ColumnToPosition (firstColumn - 1, firstRow - 1), RowToPosition (firstRow - 1));
			Debug.Log ("Acima / esquerda");
		}

		newRow = PositionToRow (newPosition);
		newColumn = PositionToColumn (newPosition, newRow);

		//Debug.Log(bubblePosition + "\t" + newPosition);
		//Debug.Log(newRow + "\t" + newColumn);

		game.matrix.bubbleMatrix [newRow, newColumn] = bubble;

		if (IsRowFull (newRow)) {
			bubble.bubbleObject.transform.position = new Vector2 (newColumn - 9.5f, 4.5f - newRow);
		} else {
			bubble.bubbleObject.transform.position = new Vector2 (newColumn - 9f, 4.5f - newRow);
		}

	}

	//Destroi as bolhas se formarem um trio
	//Mesma lógica do IsConnectedToTop
	void DestroyTrios (int row, int column)
	{

		List<int> rows = new List<int> ();
		List<int> columns = new List<int> ();
		List<Bubbles> bubblesToDestroy = new List<Bubbles> ();
		rows.Add (row);
		columns.Add (column);
		bubblesToDestroy.Add (game.matrix.bubbleMatrix [row, column]);

		for (int i = 0; i < bubblesToDestroy.Count; i++) {

			row = rows [i];
			column = columns [i];

			//Lados
			try {
				if (game.matrix.bubbleMatrix [row, column + 1] != null && game.matrix.bubbleMatrix [row, column].getColor () == game.matrix.bubbleMatrix [row, column + 1].getColor () &&
					!bubblesToDestroy.Contains (game.matrix.bubbleMatrix [row, column + 1])) {
					bubblesToDestroy.Add (game.matrix.bubbleMatrix [row, column + 1]);
					rows.Add (row);
					columns.Add (column + 1);
				}
			} catch (System.IndexOutOfRangeException) {
			}
			try {
				if (game.matrix.bubbleMatrix [row, column - 1] != null && game.matrix.bubbleMatrix [row, column].getColor () == game.matrix.bubbleMatrix [row, column - 1].getColor () &&
					!bubblesToDestroy.Contains (game.matrix.bubbleMatrix [row, column - 1])) {
					bubblesToDestroy.Add (game.matrix.bubbleMatrix [row, column - 1]);
					rows.Add (row);
					columns.Add (column - 1);
				}
			} catch (System.IndexOutOfRangeException) {
			}

			//Embaixo
			try {
				if (game.matrix.bubbleMatrix [row + 1, column] != null && game.matrix.bubbleMatrix [row, column].getColor () == game.matrix.bubbleMatrix [row + 1, column].getColor () &&
					!bubblesToDestroy.Contains (game.matrix.bubbleMatrix [row + 1, column])) {
					bubblesToDestroy.Add (game.matrix.bubbleMatrix [row + 1, column]);
					rows.Add (row + 1);
					columns.Add (column);
				}
			} catch (System.IndexOutOfRangeException) {
			}
			try {
				if (IsRowFull (row)) {
					if (game.matrix.bubbleMatrix [row + 1, column - 1] != null && game.matrix.bubbleMatrix [row, column].getColor () == game.matrix.bubbleMatrix [row + 1, column - 1].getColor () &&
						!bubblesToDestroy.Contains (game.matrix.bubbleMatrix [row + 1, column - 1])) {
						bubblesToDestroy.Add (game.matrix.bubbleMatrix [row + 1, column - 1]);
						rows.Add (row + 1);
						columns.Add (column - 1);
					}
				} else {
					if (game.matrix.bubbleMatrix [row + 1, column + 1] != null && game.matrix.bubbleMatrix [row, column].getColor () == game.matrix.bubbleMatrix [row + 1, column + 1].getColor () &&
						!bubblesToDestroy.Contains (game.matrix.bubbleMatrix [row + 1, column + 1])) {
						bubblesToDestroy.Add (game.matrix.bubbleMatrix [row + 1, column + 1]);
						rows.Add (row + 1);
						columns.Add (column + 1);
					}
				}
			} catch (System.IndexOutOfRangeException) {
			}

			//Em cima
			try {
				if (game.matrix.bubbleMatrix [row - 1, column] != null && game.matrix.bubbleMatrix [row, column].getColor () == game.matrix.bubbleMatrix [row - 1, column].getColor () &&
					!bubblesToDestroy.Contains (game.matrix.bubbleMatrix [row - 1, column])) {
					bubblesToDestroy.Add (game.matrix.bubbleMatrix [row - 1, column]);
					rows.Add (row - 1);
					columns.Add (column);
				}
			} catch (System.IndexOutOfRangeException) {
			}
			try {
				if (IsRowFull (row)) {
					if (game.matrix.bubbleMatrix [row - 1, column - 1] != null && game.matrix.bubbleMatrix [row, column].getColor () == game.matrix.bubbleMatrix [row - 1, column - 1].getColor () &&
						!bubblesToDestroy.Contains (game.matrix.bubbleMatrix [row - 1, column - 1])) {
						bubblesToDestroy.Add (game.matrix.bubbleMatrix [row - 1, column - 1]);
						rows.Add (row - 1);
						columns.Add (column - 1);
					}
				} else {
					if (game.matrix.bubbleMatrix [row - 1, column + 1] != null && game.matrix.bubbleMatrix [row, column].getColor () == game.matrix.bubbleMatrix [row - 1, column + 1].getColor () &&
						!bubblesToDestroy.Contains (game.matrix.bubbleMatrix [row - 1, column + 1])) {
						bubblesToDestroy.Add (game.matrix.bubbleMatrix [row - 1, column + 1]);
						rows.Add (row - 1);
						columns.Add (column + 1);
					}
				}
			} catch (System.IndexOutOfRangeException) {
			}
		}

		shotBubbles = bubblesToDestroy.Count;

		if (bubblesToDestroy.Count >= 3) {
			hitBubbles = true;
			explosionAudioClip.Play ();

			for (int i = 0; i < bubblesToDestroy.Count; i++) {
				//Debug.Log (bubblesToDestroy [i].getColor ());
				GameObject child = game.matrix.bubbleMatrix [rows [i], columns [i]].bubbleObject.transform.FindChild ("explosion").gameObject;
				child.SetActive (true);
				Collider2D bubbleCollider = game.matrix.bubbleMatrix [rows [i], columns [i]].bubbleObject.GetComponent<Collider2D> ();
				bubbleCollider.enabled = false;
				Destroy (game.matrix.bubbleMatrix [rows [i], columns [i]].bubbleObject, 0.4f);
				game.matrix.bubbleMatrix [rows [i], columns [i]] = null;
			}
		}

	}

	//Destroi as bolhas penduradas
	void DestroyHangingBubbles ()
	{
		// List<Bubbles> bubblesToDestroy = new List<Bubbles>();
		bool playSound = false;

		for (int i = game.matrix.bubbleMatrix.GetLength(0); i >= 0; i--) {
			for (int j = game.matrix.bubbleMatrix.GetLength(1); j >= 0; j--) {
				try {
					if (game.matrix.bubbleMatrix [i, j] != null && !IsConnectedToTop (i, j)) {
						// bubblesToDestroy.Add(game.matrix.bubbleMatrix[i, j]);
						GameObject child = game.matrix.bubbleMatrix [i, j].bubbleObject.transform.FindChild ("caindo").gameObject;
						child.SetActive (true);
						extraBubbles++;
						//Debug.Log (i + "\t" + j);
						Collider2D bubbleCollider = game.matrix.bubbleMatrix [i, j].bubbleObject.GetComponent<Collider2D> ();
						bubbleCollider.enabled = false;
						Destroy (game.matrix.bubbleMatrix [i, j].bubbleObject, 0.6f);
						game.matrix.bubbleMatrix [i, j] = null;
						playSound = true;
					}
				} catch (System.IndexOutOfRangeException) {
				}
			}
		}

		if (playSound) {
			fallingAudioClip.Play ();
		}
	}

	//Acrescenta uma nova linha na matriz
	public void NewRow ()
	{
		//Passa tudo pra baixo
		for (int i = game.matrix.bubbleMatrix.GetLength(0); i >= 0; i--) {
			for (int j = game.matrix.bubbleMatrix.GetLength(1); j >= 0; j--) {
				try {
					//Se for a primeira linha, tudo fica nulo
					if (i == 0) {
						game.matrix.bubbleMatrix [i, j] = null;
					}

					//Passa as informações pra baixo
					game.matrix.bubbleMatrix [i, j] = game.matrix.bubbleMatrix [i - 1, j];

					//Passa o gameObject pra baixo
					if (game.matrix.bubbleMatrix [i, j] != null) {
						game.matrix.bubbleMatrix [i, j].bubbleObject.transform.position = new Vector2 (game.matrix.bubbleMatrix [i, j].bubbleObject.transform.position.x, game.matrix.bubbleMatrix [i, j].bubbleObject.transform.position.y - 1);
					}
				} catch (System.IndexOutOfRangeException) {

				}
			}
		}

		Matrix.isFirstRowFull = !Matrix.isFirstRowFull;

		//Acrescenta a nova linha no topo
		float initialPosition = 0;
		int columns = 0;

		if (Matrix.isFirstRowFull) {
			initialPosition = -9.5f;
			columns = 20;
		} else {
			initialPosition = -9f;
			columns = 19;
		}
		for (int i = 0; i < columns; i++) {
			game.matrix.bubbleMatrix [0, i] = new Bubbles (initialPosition, 4.5f);
			initialPosition += 1;
		}
	}


	//Retorna uma cor aleatoria das que estão no campo
	public string ShootableColor ()
	{
		List<string> colors = new List<string> ();

		for (int i = 0; i < game.matrix.bubbleMatrix.GetLength(0); i++) {
			for (int j = 0; j < game.matrix.bubbleMatrix.GetLength(1); j++) {
				if (game.matrix.bubbleMatrix [i, j] != null && !colors.Contains (game.matrix.bubbleMatrix [i, j].getColor ())) {
					colors.Add (game.matrix.bubbleMatrix [i, j].getColor ());
				}
			}
		}

		int random = Random.Range (0, colors.Count);

		// Debug.Log(colors[random]);
		return colors [random];
	}
}
