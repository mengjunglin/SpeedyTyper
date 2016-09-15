using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public void RedirectMainMenu()
	{
		Application.LoadLevel ("MenuScene");
	}

	public void RedirectGame()
	{
		Application.LoadLevel ("GameScene");
	}
}
