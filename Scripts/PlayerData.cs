using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif


/*
   A Leaderboard entry
*/
public class LeaderBoardEntry
{
	public string name; // name of the player
	public int score; // score for this game

	public LeaderBoardEntry()
	{}

	public LeaderBoardEntry(string n, int s)
	{
		name = n;
		score = s; 
	}

	// Write this entry on disk
	public void write(BinaryWriter b)
	{
		b.Write(name);
		b.Write(score);
	}

	// Read a leaderboard entry from a BinaryReader
	public void read(BinaryReader b)
	{
		name = b.ReadString();
		score = b.ReadInt32();
	}
}

/*
  Save data for the game. Stored locally.
  The class is tested by PlayerDataTests.cs
*/
public class PlayerData
{
    // --------------------------------------- Player Data variables
    private List<LeaderBoardEntry> entries = new List<LeaderBoardEntry>(); // List that will store the entries of the past games, its max length will be entriesNumberMax
    private int entriesNumberMax = 10; // number max of entries we'll store

    private string saveFile; // full path for the save file

    // version of the playerData, to be able to load different versions, not used here
    static int version = 1; 


    // Will create a PlayerData instance if none is created, store the save path, and read it from memory if we already played
    public void Create()
    {
        saveFile = Application.persistentDataPath + "/save.bin";

        if (File.Exists(saveFile))
        {
            // If we have a save, we read it.
            Read();
        }
        else
        {
            // If not we create one with default data.
			NewSave();
        }
    }

    // Will create a new save
	public void NewSave()
	{
		entries.Clear();

		Save();
	}

	// Getter for entries
	public List<LeaderBoardEntry> getEntries()
	{
		return entries;
	}

	// Getter for max entries
	public int getMaxEntries()
	{
		return entriesNumberMax;
	}

	// Will read a playerData from saveFile
    public void Read()
    {
        BinaryReader r = new BinaryReader(new FileStream(saveFile, FileMode.Open));

        version = r.ReadInt32();

		// Could add specific operations based on version

        entries.Clear();
        int entriesNumber = r.ReadInt32();
        entriesNumber = Mathf.Min(entriesNumber, entriesNumberMax); //load only max

        for(int i = 0; i < entriesNumber; ++i)
        {
           LeaderBoardEntry entry = new LeaderBoardEntry();
           entry.read(r);
           entries.Add(entry);
        }

        r.Close();
    }

    // Will save the playerData in saveFile
    public void Save()
    {
        BinaryWriter w = new BinaryWriter(new FileStream(saveFile, FileMode.OpenOrCreate));

        w.Write(version);

        // Write characters.
        w.Write(entries.Count);
        foreach (LeaderBoardEntry entry in entries)
        {
           entry.write(w);
        }

        w.Close();
    }

    // Add a score entry in our list
    public void addEntry(LeaderBoardEntry entry)
    {
    	entries.Add(entry);
    	entries = entries.OrderBy(e => e.score).ToList(); // Create a second list in memory, so could write our own, but we will be sufficient for our test case
    	if(entries.Count > entriesNumberMax) 
    	{
    		entries.RemoveAt(entriesNumberMax); // if were above max, it's only by one, so remove it
    	}
    }

    // Would this score be the best score
    public bool isItBestScore(int score)
    {
    	if(entries.Count == 0)
    	{
    		return true;
    	}

    	return score < entries[0].score;
    }


}

// Helper class to cheat in the editor for test purpose
#if UNITY_EDITOR
public class PlayerDataEditor : Editor
{
	[MenuItem("Memory Debug/Clear Save")]
    static public void ClearSave()
    {
        File.Delete(Application.persistentDataPath + "/save.bin");
    }

    [MenuItem("Memory Debug/Add random entry")]
    static public void AddRandom()
    {
    	LeaderBoardEntry entry = new LeaderBoardEntry("test" , (int)Random.Range(6, 100));

        GameFlow.get().getPlayerData().addEntry(entry);
    }

    [MenuItem("Memory Debug/Fill entries")]
    static public void FillRandom()
    {
    	while(GameFlow.get().getPlayerData().getEntries().Count < GameFlow.get().getPlayerData().getMaxEntries())
    	{
    		LeaderBoardEntry entry = new LeaderBoardEntry("test" , (int)Random.Range(6, 100));

        	GameFlow.get().getPlayerData().addEntry(entry);
    	}
    }
}
#endif