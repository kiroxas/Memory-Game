using UnityEngine;

// Class for the mainMenu scene
public class MainMenu : MonoBehaviour
{
   
    public void Awake()
    {
    }

    public void loadLeaderboard()
    {
        GameFlow.get().LoadLeaderBoard();
    }

    public void LoadMainGame()
    {
    	GameFlow.get().LoadMainGame();
    }
}