using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Scripts with endscreen callbacks
public class EndScreen : MonoBehaviour 
{
	public Text scoreText; // Text to put the score
	public Text nameText; // Text where will be the name of the player
	public GameObject bestScore; // Best score gameObject, to show if best score

	private int score; // will hold the score value

	// Use this for initialization
	void Start () 
	{
		score = GameFlow.get().getLastScore();
		scoreText.text = score.ToString() + " moves";
		if(GameFlow.get().isItBestScore(score))
		{
			bestScore.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	// Will save the current score
	private void saveScore()
	{
		string name = System.String.IsNullOrEmpty(nameText.text) ? "Kiro" : nameText.text;

		GameFlow.get().AddEntry(new LeaderBoardEntry(name, score));
		GameFlow.get().Save();
	}

	// will save and go back to main menu
	public void backToMainMenu()
	{
		saveScore();
		GameFlow.get().LoadStartScreen();
	}
}
