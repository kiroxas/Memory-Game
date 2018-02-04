using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// Argument for the cardTriggeredEvent, use class instead of card directly to allow adding more infos to be easier
public class CardTriggeredArgument
{
	public Card triggered;

	public CardTriggeredArgument(Card c)
	{
		triggered = c;
	}
}

// Event
public class CardTriggeredEvent : UnityEvent<CardTriggeredArgument>
{}

// Script that will handle a card on the board
public class Card : MonoBehaviour 
{
	static public Color transparent = new Color(0,0,0,0); // will be use when a card is matched

	public int id; // its id
	private Color color; // its color

	public bool triggered; // is it triggered this turn ?

	public CardTriggeredEvent cardEvent; // the event that LevelFlow will listen to

	// Init the card
	public void init(int i, Color col)
	{
		id = i;
		color = col;
		triggered = false;

		if(GetComponent<Image>() == null)
		{
			Debug.LogError("The card prefab should have an image component");
		}

		cardEvent = new CardTriggeredEvent();
		GetComponent<Button>().onClick.AddListener( clicked );
	} 

	// Make card disapear
	public void disapear()
	{
		GetComponent<Image>().color = transparent;
		GetComponent<Button>().enabled = false;
	}

	// Make card be blank again
	public void failed()
	{
		triggered = false;
		GetComponent<Image>().color = Color.white;
		GetComponent<Button>().transition = Selectable.Transition.ColorTint;
	}

	// Callback when the card is clicked
    public void clicked()
    {
    	if(triggered == false)
    	{
        	triggered = true;
       	    
       	    GetComponent<Button>().transition = Selectable.Transition.None;
       	    GetComponent<Image>().color = color;
       	    
       	    // Let everyone interested know
       	    cardEvent.Invoke(new CardTriggeredArgument(this));
       	}

    }
}