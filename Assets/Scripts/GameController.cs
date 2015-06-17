using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

	public static bool playing = true;	//Se o jogo ta rodando
	public int turns = 0;	//rodadas
	public bool win, lose;
	public int totalPoints = 0;	//pontuacao total na fase
	public int bubblesLeft = 0;	//bolhas restantes na fase
	public GameObject victoryScreen, loseScreen;	//telas de vitoria e derrota
	public int powerUpExplodeCounter = 0;	//contador de rodadas pro power up
	public int adaptPowerUpTimes = 0;	//acho que o mesmo que o de cima pro outro power up

	private Shoot shoot;
	private Helper helper;
	private NewGame game;
	private int basePoints = 100;	//pontuacao base ao destruir um trio
	private int pointsModifier = 1;	//modificador do combo

	//minatto
	public GameObject GuideLine;

	void Start ()
	{
		shoot = GetComponent<Shoot> ();
		helper = GetComponent<Helper> ();
		game = GetComponent<NewGame> ();
		bubblesLeft = game.totalUsableBubbles;
		playing = true;

		GuideLine = FindObjectOfType<LineRenderer> ().gameObject;
	}

	void Update ()
	{
		if (Input.GetKey (KeyCode.J))
			win = true;
	}
	//Controla a jogada depois de lançar a bola
	public void AfterATurn ()
	{
		if (playing) {
			if (!helper.hitBubbles) {	//Se nao acertou trio
				turns++;
				Invoke ("CheckIfNewRow", 0.2f);
				pointsModifier = 1;
				bubblesLeft--;
				powerUpExplodeCounter = 0;
			} else {
				Points (helper.shotBubbles, helper.extraBubbles);
				powerUpExplodeCounter++;
				CheckIfWin ();
			}

			if (bubblesLeft == 2) {
				adaptPowerUpTimes ++;
			}

			helper.shotBubbles = 0;
			helper.extraBubbles = 0;
			helper.hitBubbles = false;
			shoot.canShoot = true;
		}
	}

	//Verifica se é o turno certo para acrescentar uma nova linha
	void CheckIfNewRow ()
	{
		//Se jogada = jogada limite
		if (turns % game.bubblesLimitToNewRow == 0) {
			helper.NewRow ();
		}

		CheckIfLose ();
	}

	//Verifica se o jogador perdeu (linha chegou no limite embaixo e/ou gastou todas as bolhas)
	void CheckIfLose ()
	{
		lose = false;

		if (turns >= game.totalUsableBubbles) {
			lose = true;
			//Debug.Log ("lose");
		}

		for (int i = 0; i < game.matrix.bubbleMatrix.GetLength(1); i++) {
			try {
				if (game.matrix.bubbleMatrix [10, i] != null) {
					lose = true;
					//Debug.Log ("lose");
				}
			} catch (System.IndexOutOfRangeException) {

			}
		}

		if (lose) {
			if (UserController.instance.Facebook) {
				UserController.instance.life--;
				UpdateUserController.instance.UpdateDatabase ();
			}
			playing = false;
			GuideLine.SetActive (false);
			loseScreen.SetActive (true);
		}
	}

	//Verifica se o jogador ganhou (destruiu todas as bolhas da tela)
	void CheckIfWin ()
	{
		win = true;

		for (int i = 0; i < game.matrix.bubbleMatrix.GetLength(0); i++) {
			for (int j = 0; j < game.matrix.bubbleMatrix.GetLength(1); j++) {
				if (game.matrix.bubbleMatrix [i, j] != null) {
					win = false;
					break;
				}
			}
		}
	
		if (win) {
			if (UserController.instance.Facebook)
				UpdateUserController.instance.UpdateDatabase ();
			//Debug.Log ("win");
			playing = false;
			GuideLine.SetActive (false);
			victoryScreen.SetActive (true);
		}
	}

	//Controla a pontuação
	void Points (int shotBubbles, int extraBubbles)
	{
		//Somente pontua se fez trio
		if (shotBubbles >= 3) {
			int turnPoints = 0; //Pontos do turno
			bool combo = false; //Combo

			//Se acertou mais de 6, combo
			if (shotBubbles >= 6) {
				combo = true;
			}

			//A cada trio ganha 100pts
			while (shotBubbles - 3 >= 0) {
				turnPoints += basePoints;
				shotBubbles -= 3;
			}

			int shotBubblesLeft = shotBubbles; //Bolhas restantes dos trios

			//Acrescenta 25 pts por bolha restante
			turnPoints += shotBubblesLeft * 25;
			//Acrescenta 50 pts por bolha solta
			turnPoints += extraBubbles * 50;
			//Multiplica pelo combo
			turnPoints *= pointsModifier;
			//Adiciona ao total de pontos
			totalPoints += turnPoints;

			//Aumenta o modifier do combo
			if (combo) {
				pointsModifier *= 2;
			}
			combo = false;
		}
	}
}
