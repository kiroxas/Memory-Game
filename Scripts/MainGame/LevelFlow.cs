using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/*
   Class that will handle the initialisation and gameLogic of a game
*/
public class LevelFlow : MonoBehaviour 
{
	int tries = 0; // number of moves
	
	public Board board; // the board

	public RectTransform container; // transform of the board
	public GameObject linePrefab; // Prefab for a line
	public GameObject cardPrefab; // prefab for a card
	public GraphicRaycaster canvasRaycaster; // the graphicRaycaster, to disable input for a short while

	public Color[] colors; // colors to use
	private List<Card> triggered; // cards that has been triggered this turn
	private int tilesFound = 0; // number of tiles we found so far

	private float timeWhenWrong = .5f; // timing to disable input nd wait between rounds

	// Is it the end of the turn ?
	private bool needAction()
	{
		return triggered.Count == board.cardsToMatch;
	}

	// Do we have a match or not ?
	private bool areTriggeredValid()
	{
		if(triggered.Count < 2)
			return true;

		int id = triggered[0].id;
		return triggered.All(c => c.id == id);
	}

	// Disable input on canvas
	private void disableInput()
	{
		canvasRaycaster.enabled = false;
	}

	// enable input on canvas
	private void enableInput()
	{
		canvasRaycaster.enabled = true;
	}

	// Use this for initialization
	void Start () 
	{
		if(cardPrefab.GetComponent<Card>() == null)
		{
			Debug.LogError("the Card prefab should have a Card script attached");
			return;
		}

		triggered = new List<Card>();
		tries = 0;
		board = new Board();

		// Hard config :)
		/*board.width = 7;
		board.height = 3;
		board.cardsToMatch = 3;*/

		board.fill(); // give us a valid configuration

		// Now draw the board
		for(int y = 0; y < board.height; ++y)
		{
			GameObject line = Instantiate(linePrefab);
			line.GetComponent<Transform>().SetParent(container, false);

			for(int x = 0; x < board.width; ++x)
			{
				GameObject card = Instantiate(cardPrefab);

				card.GetComponent<Transform>().SetParent(line.GetComponent<RectTransform>(), false);
				int id = board.getId(x,y);
				Color col = id > colors.Count() ? Color.blue : colors[id]; // maybe generate a given color for this id instead ?
				card.GetComponent<Card>().init(id, col); // give the card its id and color
				card.GetComponent<Card>().cardEvent.AddListener(cardTriggered); // listener that will be called if this card is clicked on
			}
		}
	}
	
	// Is the game finished ?
	public bool finishedGame()
	{
		return tilesFound == board.totalTilesNumber();
	}
	
	// Make cards that matched disapear
	public void makeCardsDiseapeared()
	{
		foreach(Card c in triggered)
		{
			c.disapear();
		}
	}

	// Make cards that did not match to not show color
	public void returnCards()
	{
		foreach(Card c in triggered)
		{
			c.failed();
		}
	}

	// clear our triggered buffer
	public void clearTriggered()
	{
		triggered.Clear();
	}

	// Actions to takes if this was a successful turn
	public void validTiles()
	{
		makeCardsDiseapeared();
		tilesFound += board.cardsToMatch;
				
		if(finishedGame())
		{
			finishGame();
		}

		clearTriggered();
	}

	// calbabck that is called when a card is triggered
	public void cardTriggered(CardTriggeredArgument arg)
	{
		if(arg == null)
		{
			Debug.LogError("Argumument is null");
			return;
		}

		if(arg.triggered == null)
		{
			Debug.LogError("card is null");
			return;
		}

		triggered.Add(arg.triggered);

		if(needAction())
		{
			disableInput();
			Invoke("enableInput", timeWhenWrong);

			tries++;
			if(areTriggeredValid())
			{
				Invoke("validTiles", timeWhenWrong);
			}
			else
			{
				Invoke("returnCards", timeWhenWrong);	
			}	

			Invoke("clearTriggered", timeWhenWrong);
		}
	}

	// quit to main menu
	public void quit()
	{
		GameFlow.get().LoadStartScreen();
	}

	// store the score and launch the endscreen
	public void finishGame()
	{
		GameFlow.get().setLevelScore(tries);
		GameFlow.get().LoadEndScreen();
	}
}
