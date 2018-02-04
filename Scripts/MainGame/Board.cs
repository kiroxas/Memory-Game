using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/*
  Class that will generate and hold the configuration of a board.
  The class is tested by BoardTests.cs
*/
public class Board 
{
	public int width = 4; // width of the board
	public int height = 3; // height of the board

	public int cardsToMatch = 2; // numbers of cards to match

	public List<int> board; // the actual board

	// Will fill a board respecting the rules
	// Note : not the most efficient way to do this in term of time and space, but it's only called once per game
	public void fill()
	{
		if((width * height) % cardsToMatch != 0)
		{
			Debug.LogError("The number of cards must be a multiple of cardsToMatch");
			return;
		}

		board = Enumerable.Repeat(-1, width * height).ToList(); // not the most efficient way, but a compact one
		
		// First let's fill all available positions
		List<Vector2> available = new List<Vector2>(width * height);

		for(int x = 0; x < width; ++x)
		{
			for(int y = 0; y < height; ++y)
			{
				available.Add(new Vector2(x, y));
			}
		}

		// Now list id Max
		int idMax = getIdMax();

		// Now let's fill our board
		for(int i = 0 ; i < idMax; ++i)
		{
			for(int occurences = 0; occurences < cardsToMatch; ++occurences)
			{
				Vector2 placement = available[(int)Random.Range(0, available.Count)];
				setId((int)placement.x, (int)placement.y, i);
				available.Remove(placement);
			}
		}
	}

    // Gives us the total number of tiles on this board
	public int totalTilesNumber()
	{
		return width * height;
	}

    // The max id in the board
	public int getIdMax()
	{
		return (width * height) / cardsToMatch;
	}

	// Gives us the id at position x,y
	public int getId(int x, int y)
	{
		if(board == null)
		{
			Debug.LogError("You must fill the board first");
			return -1;
		}

		return board[getIndex(x, y)];
	}

	// give the flat index for x,y
	public int getIndex(int x, int y)
	{
		return y * width + x;
	}

    // set the index at x,y to value
	private int setId(int x, int y, int value)
	{
		if(board == null)
		{
			Debug.LogError("You must fill the board first");
			return -1;
		}

		return board[getIndex(x, y)] = value;
	}
}