using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class that will display a leaderboardEntry
public class LeaderBoardEntryUI : MonoBehaviour
 {
	public Text nameText; // name of the player
    public Text scoreText; // scre of the player
    public Text rankText; // rank of this entry in leaderboard

    // fill the entry
    public void Fill(LeaderBoardEntry entry, int rank)
    {
        nameText.text = entry.name;
        scoreText.text = entry.score.ToString();
        rankText.text = rank.ToString();
    }
}
