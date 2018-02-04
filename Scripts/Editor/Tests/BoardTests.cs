using System.Collections.Generic; 
using NUnit.Framework; 
 
namespace BoardTest.Tests 
{ 
  public class BoardTest 
  { 

      [Test] 
      public void checkIndex() 
      { 
        Board b = new Board();

        Assert.That(b.getIndex(0, 2), Is.EqualTo(8));
        Assert.That(b.getIndex(3, 0), Is.EqualTo(3));
        Assert.That(b.getIndex(0, 0), Is.EqualTo(0));
        Assert.That(b.getIndex(3, 2), Is.EqualTo(11));
      } 

      [Test] 
      public void noNegativeValue() 
      { 
        Board b = new Board();

        b.fill();

        Assert.That(b.board.Count, Is.EqualTo(b.width * b.height));
        Assert.That(b.getIdMax(), Is.EqualTo(6));

        for(int x = 0; x < b.width; ++x)
        {
          for(int y = 0; y < b.height; ++y)
          {
             Assert.That(b.getId(x, y), Is.Not.EqualTo(-1)); 
          }
        }
      } 

      [Test] 
      public void checkIdOccurence() 
      { 
        Board b = new Board();

        b.fill();

        int max = b.getIdMax();
        
        for(int i = 0; i < max; ++i)
        {
           int cpt = 0;
           foreach(int val in b.board)
           {
              if(val == i)
                ++cpt;
           }

           Assert.That(cpt, Is.EqualTo(b.cardsToMatch));
        }
      } 

      [Test] 
      public void WorksWithDifferentOccurences() 
      { 
        Board b = new Board();

        b.cardsToMatch = 3;
        b.fill();

        int max = b.getIdMax();
        
        for(int i = 0; i < max; ++i)
        {
           int cpt = 0;
           foreach(int val in b.board)
           {
              if(val == i)
                ++cpt;
           }

           Assert.That(cpt, Is.EqualTo(b.cardsToMatch));
        }
      } 

      [Test] 
      public void WorksWithDifferentSize() 
      { 
        Board b = new Board();

        b.width = 40;
        b.height = 1;
        b.fill();

        int max = b.getIdMax();
        
        for(int i = 0; i < max; ++i)
        {
           int cpt = 0;
           foreach(int val in b.board)
           {
              if(val == i)
                ++cpt;
           }

           Assert.That(cpt, Is.EqualTo(b.cardsToMatch));
        }
      } 

      [Test] 
      public void hardExample() 
      { 
        Board b = new Board();

        b.width = 40;
        b.height = 10;
        b.cardsToMatch = 4;
        b.fill();

        int max = b.getIdMax();
        
        for(int i = 0; i < max; ++i)
        {
           int cpt = 0;
           foreach(int val in b.board)
           {
              if(val == i)
                ++cpt;
           }

           Assert.That(cpt, Is.EqualTo(b.cardsToMatch));
        }

        b.fill();
        
           for(int i = 0; i < max; ++i)
           {
              int cpt = 0;
              foreach(int val in b.board)
              {
                 if(val == i)
                   ++cpt;
              }
              Assert.That(cpt, Is.EqualTo(b.cardsToMatch));
          }
           
      } 
  }
}
 