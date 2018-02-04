using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

/*
	Class that survives among the scenes.
	Used to load scenes, and handle playerData
*/
public class GameFlow : MonoBehaviour
{
	// Singleton
	static protected GameFlow s_Instance;
	static public GameFlow instance { get { return s_Instance; } }

	public int lastScore = 0; // store the score of the last game
	private PlayerData playerData; // store the playerData

 	void Awake()
    {
    	if (s_Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        s_Instance = this;
        
    	DontDestroyOnLoad(gameObject);
        Init();
    }

    // Init gameflow
    void Init()
    {
    	playerData = new PlayerData();
        playerData.Create();
    }

    // Playerdata getter
    public PlayerData getPlayerData()
    {
    	return playerData;
    }

    // get gameflow instance
    static public GameFlow get()
    {
        return instance;
    }

    // set the score for the last game
    public int setLevelScore(int score)
    {
    	return lastScore = score;
    }

    // lastscore getter
    public int getLastScore()
    {
    	return lastScore;
    }

    // Would this score be the best score ?
    public bool isItBestScore(int score)
    {
    	return playerData.isItBestScore(score);
    }

    // load level helper
    private void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadStartScreen()
    {
    	LoadLevel("StartScreen");
    }

    // Add a leaderboardentry in the player data
    public void AddEntry(LeaderBoardEntry entry)
    {
    	playerData.addEntry(entry);
    }

    // save the player data
    public void Save()
    {
    	playerData.Save();
    }

    public void LoadEndScreen()
    {  
        LoadLevel("EndScreen");
    }

    public void LoadLeaderBoard()
    {
        LoadLevel("Leaderboard");
    }

    public void LoadMainGame()
    {
        LoadLevel("MainGame");
    }
}