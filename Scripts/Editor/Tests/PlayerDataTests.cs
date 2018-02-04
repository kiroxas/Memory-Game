using System.Collections.Generic; 
using NUnit.Framework; 
using UnityEngine;
 
namespace PlayerDataTests.Tests 
{ 
  public class PlayerDataTests 
  { 

      public LeaderBoardEntry giveOneRandom()
      {
       return new LeaderBoardEntry("test" , (int)Random.Range(6, 100));
      }

      [Test] 
      public void addEntries() 
      {
          PlayerData player = new PlayerData();

          Assert.That(player.getEntries().Count, Is.EqualTo(0));

          for(int i = 0; i < player.getMaxEntries(); ++i)
          {
              player.addEntry(giveOneRandom());

              Assert.That(player.getEntries().Count, Is.EqualTo(i+1));
          }

          player.addEntry(giveOneRandom());

          Assert.That(player.getEntries().Count, Is.EqualTo(player.getMaxEntries()));

          player.addEntry(giveOneRandom());

          Assert.That(player.getEntries().Count, Is.EqualTo(player.getMaxEntries()));
      }

      [Test] 
      public void sortedEntries() 
      {
          PlayerData player = new PlayerData();

          Assert.That(player.getEntries().Count, Is.EqualTo(0));

          for(int i = 0; i < player.getMaxEntries(); ++i)
          {
              player.addEntry(giveOneRandom());

              Assert.That(player.getEntries().Count, Is.EqualTo(i+1));
          }

          int initScore = 0;
          foreach(LeaderBoardEntry ent in player.getEntries())
          {
              Assert.That(ent.score, Is.GreaterThanOrEqualTo(initScore));
              initScore = ent.score;
          }
      }

      [Test] 
      public void addAfterFull() 
      {
          PlayerData player = new PlayerData();

          for(int i = 0; i < player.getMaxEntries(); ++i)
          {
              player.addEntry(new LeaderBoardEntry("t", i + 10));
          }

          Assert.That(player.getEntries()[0].score, Is.EqualTo(10));
          Assert.That(player.getEntries()[player.getEntries().Count - 1 ].score, Is.EqualTo(19));

          player.addEntry(new LeaderBoardEntry("t", 1));

          Assert.That(player.getEntries()[0].score, Is.EqualTo(1));

          for(int i = 1; i < player.getMaxEntries(); ++i)
          {
               Assert.That(player.getEntries()[i].score, Is.EqualTo(10 + i - 1));
          }
      }
  }
}
 