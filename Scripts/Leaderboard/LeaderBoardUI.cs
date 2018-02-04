using UnityEngine;

// Class for the leaderboard scene
public class LeaderBoardUI : MonoBehaviour
{
    public RectTransform placement;
    public GameObject leaderboardEntryPrefab;

    public void Awake()
    {
        // display the leaderboard entries
        int rank = 1;
        foreach(LeaderBoardEntry entry in GameFlow.get().getPlayerData().getEntries())
        {
            GameObject instance = Instantiate(leaderboardEntryPrefab);

            instance.GetComponent<Transform>().SetParent(placement, false);
            LeaderBoardEntryUI entryUI = instance.GetComponent<LeaderBoardEntryUI>();

            entryUI.Fill(entry, rank);
            ++rank;
        }
    }

    // go back to start screen
    public void back()
    {
        GameFlow.get().LoadStartScreen();
    }
}
