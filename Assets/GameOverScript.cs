using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {
	public Text txtScore;

	// Use this for initialization
	void Start () {
		txtScore.text = StartGameHandler.score.ToString();
	}

	public void RedirectMainMenu()
	{
		Application.LoadLevel ("MenuScene");
	}

	public void RedirectGame()
	{
		Application.LoadLevel ("GameScene");
	}
}
