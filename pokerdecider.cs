using System;
using static System.Console;
// using System.Collections.Generic;
using System.IO;
// using System.Linq;
using static System.Random;

// This is the Main Class
class MainClass {
  public static void Main( string [] args )
  {
    Deck LargeDeck = new Deck();
    LargeDeck.Shuffle();
    
    Deck myDeck = new Deck();
    for( int i = 0; i < 5; i++) myDeck.deck[i] = LargeDeck.deck[i];
    Display(5, myDeck);  

    PokerHands.HighCard(myDeck);
    PokerHands.OnePair(myDeck);
    PokerHands.TwoPair(myDeck);
    PokerHands.ThreeOfAKind(myDeck);
    PokerHands.Straight(myDeck);
    PokerHands.Flush(myDeck);
    PokerHands.FullHouse(myDeck);
    PokerHands.FourOfAKind(myDeck);
    PokerHands.StraightFlush(myDeck);
    PokerHands.RoyalFlush(myDeck);

    Output();
  }

  public static void Output ()
  {
    WriteLine("Strongest Hand: ");
    for( int card = 0; card < 5; card ++)
    {     
      WriteLine (PokerHands.strongestDeck.deck[card] + " ");
    }
    WriteLine();
    Write("The Hand Classification is: ");
    if(PokerHands.returnHandType() == 1) Write("High Card");
    if(PokerHands.returnHandType() == 2) Write("One Pair");
    if(PokerHands.returnHandType() == 3 ) Write("Two Pair");
    if(PokerHands.returnHandType() == 4 ) Write("Three Of A Kind");
    if(PokerHands.returnHandType() == 5 ) Write("Straight");
    if(PokerHands.returnHandType() == 6 ) Write("Flush");
    if(PokerHands.returnHandType() == 7 ) Write("Full House");
    if(PokerHands.returnHandType() == 8 ) Write("Four Of A Kind");
    if(PokerHands.returnHandType() == 9 ) Write("Straight Flush");
    if(PokerHands.returnHandType() == 10 ) Write("Royal Flush");
  }
  
  public static void Display(int numCards, Deck hand)
  {
    // Using the Deck method we are creating a new deck of cards
    //This for loop will generate the deck for us using the Deck method
    WriteLine();
    WriteLine("Player: ");
    for( int card = 0; card < 2; card ++)
    {     
      WriteLine (hand.deck[card] + " ");
    }
    WriteLine();
    WriteLine("Flop: ");
    for( int card = 2; card < numCards; card ++)
    {     
      WriteLine(hand.deck[card] + " ");
    }
  }
}

//This is the Cards class that will take in the actual value and the suit of the card
public class Cards
{
  public string value;
  public string suit;
  
  //This is the constructor that sets the private variable 
  public Cards(string cardFace, string cardSuit)
  {
    value = cardFace;
    suit = cardSuit;
  }
  
  public override string ToString()
  {
    return $"{value} of {suit}";
  }
  
} //End of Cards Class

// This is the deck class which will generate the deck we use in main
public class Deck
{
  const int numberOfCards = 52;
  private Random rGen;
  public Cards[] deck;
  public string [] value = {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
  public string [] suits = {"Clubs", "Diamond" , "Hearts", "Spades" };

  
public Deck( )
  {
      // string [] value = {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
      // string [] suits = {"Clubs", "Diamond" , "Hearts", "Spades" };
      deck = new Cards[numberOfCards];
      rGen = new Random();
      
      for(int iterator = 0; iterator < numberOfCards; iterator++)
      {
        deck[iterator] = new Cards( value[ iterator % 13 ], suits[ iterator / 13 ]);
        // Console.WriteLine(deck[iterator]);
      }
  }
 
public void Shuffle()
  {
    for ( int iterator = 0; iterator < deck.Length; iterator ++)
    {
      int swap = rGen.Next(numberOfCards); //Get a random index
      Cards placeHolder = deck[iterator]; //Get card at that index
      deck[iterator] = deck[swap]; //Swap two indexes
      deck[swap] = placeHolder;    
    }  
  }
}

public class PokerHands
{
  //bool[] doesHandExist = new bool[10];
  public static int strongestHandType = 0;
  public static Deck strongestDeck;
  static int decidingValue = 0; // this decides what the optimal/highest value in the strongest deck

  public static int returnHandType()
  {
    return strongestHandType;
  }

  public static bool HighCard(Deck hand)
  {   
    Deck temp = hand;
    int[] intArray = new int [5];
    Console.WriteLine();
    for( int iterator = 0; iterator < 5; iterator++ )
    {     
      string cardValue = hand.deck[iterator].value;
      int cardValueInt;
      if(cardValue == "A") cardValueInt = 1;
      else if(cardValue == "J") cardValueInt = 11;
      else if(cardValue == "Q") cardValueInt = 12;
      else if(cardValue == "K") cardValueInt = 13;
      else cardValueInt = Int32.Parse(cardValue);
      intArray[iterator] = cardValueInt;
    }
    Array.Sort(intArray);
    decidingValue = intArray[intArray.Length-1];
  
    if( strongestHandType == 0) //CHANGE THIS
    {
      strongestDeck = temp;
      strongestHandType = 1;
      return true;
    }   
    return false;
  }
  
  public static bool OnePair(Deck hand)
  {
    Deck temp = hand;
    int[] valueCounter = new int[14];
    for( int iterator = 0; iterator < 5; iterator++)
    {
      string cardValue = hand.deck[iterator].value;
      int cardValueInt;
      if(cardValue == "A") cardValueInt = 1;
      else if(cardValue == "J") cardValueInt = 11;
      else if(cardValue == "Q") cardValueInt = 12;
      else if(cardValue == "K") cardValueInt = 13;
      else cardValueInt = Int32.Parse(cardValue);
      valueCounter[cardValueInt]++; 
    }
    
    // for( int i = 1; i< valueCounter.Length; i++) Write (valueCounter[i] + " ");
    // WriteLine();

   int pairCounter = 0;
   //int pairValue = 0;
   for( int iterator = 0; iterator < 12; iterator++)
   {
     if(valueCounter[iterator] == 2) 
     {
      //  WriteLine("Found a pair!");
       pairCounter++;
       //pairValue = iterator;
     }
   }
  //  WriteLine(pairCounter + " pairs");
   if( 1 >= strongestHandType && pairCounter == 1)
   {     
       strongestHandType = 2;
       strongestDeck = temp;
       return true;
   }
   return false;
  } 
  
  public static bool TwoPair(Deck hand)
  {
    Deck temp = hand;
    int[] valueCounter = new int[14];
    for( int iterator = 0; iterator < 5; iterator++)
    {
      string cardValue = hand.deck[iterator].value;
      int cardValueInt;
      if(cardValue == "A") cardValueInt = 1;
      else if(cardValue == "J") cardValueInt = 11;
      else if(cardValue == "Q") cardValueInt = 12;
      else if(cardValue == "K") cardValueInt = 13;
      else cardValueInt = Int32.Parse(cardValue);
      valueCounter[cardValueInt] ++;
    }
    int pairCounter = 0;
    int pairValue = 0;
    for( int iterator = 0; iterator < 13; iterator ++)
    {
      if(valueCounter[iterator] == 2) 
      {
       pairCounter++;
       pairValue = iterator;
      }
    }
    if ( 3 > strongestHandType && pairCounter == 2)
    {
      strongestDeck = temp;
      strongestHandType = 3;
      return true;
    } 
    return false;
  }

  public static bool ThreeOfAKind(Deck hand)
  {
    Deck temp = hand;
    int[] valueCounter = new int[14];
    for( int iterator = 0; iterator < 5; iterator++)
    {
      string cardValue = hand.deck[iterator].value;
      int cardValueInt;
      if(cardValue == "A") cardValueInt = 1;
      else if(cardValue == "J") cardValueInt = 11;
      else if(cardValue == "Q") cardValueInt = 12;
      else if(cardValue == "K") cardValueInt = 13;
      else cardValueInt = Int32.Parse(cardValue);
      valueCounter[cardValueInt]++; 
   }
   for(int iterator = 0; iterator < 13; iterator++)
   {
     if( 4 > strongestHandType && valueCounter[iterator] == 3)
     {
       strongestDeck = temp;
       strongestHandType = 4;
       return true;
     }
   }
   return false;
  }

  public static bool Straight(Deck hand)
  {
    Deck temp = hand;
    if( 5 > strongestHandType && !CheckTests.CheckSuits(hand) && CheckTests.Sequential(hand))
    {
      strongestDeck = temp;
      strongestHandType = 5;
      return true;
    }
    return false;
  }

  public static bool Flush(Deck hand)
  {
    Deck temp = hand;
    if(6 > strongestHandType && CheckTests.CheckSuits(hand)) 
    {
      strongestDeck = temp;
      strongestHandType = 6;
      return true;
    }

    return false;
  }

  public static bool FullHouse(Deck hand)
  {
    Deck temp = hand;
    if(7 > strongestHandType && OnePair(hand) == true && ThreeOfAKind(hand) == true) 
    {
      strongestDeck = temp;
      strongestHandType = 7;
      return true;
    }
    return false;
  }

  public static bool FourOfAKind(Deck hand)
  {
    Deck temp = hand;
    int[] valueCounter = new int[14];
    for( int iterator = 0; iterator < 5; iterator++)
    {
      string cardValue = hand.deck[iterator].value;
      int cardValueInt;
      if(cardValue == "A") cardValueInt = 1;
      else if(cardValue == "J") cardValueInt = 11;
      else if(cardValue == "Q") cardValueInt = 12;
      else if(cardValue == "K") cardValueInt = 13;
      else cardValueInt = Int32.Parse(cardValue);
      valueCounter[cardValueInt]++; 
   }
   for( int iterator = 0; iterator < 13; iterator++)
   {
     if(8 > PokerHands.strongestHandType && valueCounter[iterator] == 4) 
     {
       strongestDeck = temp;
       strongestHandType = 8;
       return true;
     }
   }
   return false;
  }

  public static bool StraightFlush(Deck hand)
  {
    Deck temp = hand;
    int[] valueCounter = new int[14];
    for( int iterator = 0; iterator < 5; iterator++)
    {
      string cardValue = hand.deck[iterator].value;
      int cardValueInt;
      if(cardValue == "A") cardValueInt = 1;
      else if(cardValue == "J") cardValueInt = 11;
      else if(cardValue == "Q") cardValueInt = 12;
      else if(cardValue == "K") cardValueInt = 13;
      else cardValueInt = Int32.Parse(cardValue);
      valueCounter[cardValueInt]++; 
   }
    bool isRoyal = false;
    if(valueCounter[0] == 1 && valueCounter[9] == 1 && valueCounter[10] == 1 && valueCounter[11] == 1 && valueCounter[12] == 1) isRoyal = true;
    if(9 > strongestHandType && CheckTests.CheckSuits(hand) && CheckTests.Sequential(hand) && !isRoyal)
    {
      strongestDeck = temp;
      strongestHandType = 9;
      return true;
    }
    return false;
  }

  public static bool RoyalFlush(Deck hand)
  {
    Deck temp = hand;
    int[] valueCounter = new int[14];
    for( int iterator = 0; iterator < 5; iterator++)
    {
      string cardValue = hand.deck[iterator].value;
      int cardValueInt;
      if(cardValue == "A") cardValueInt = 1;
      else if(cardValue == "J") cardValueInt = 11;
      else if(cardValue == "Q") cardValueInt = 12;
      else if(cardValue == "K") cardValueInt = 13;
      else cardValueInt = Int32.Parse(cardValue);
      valueCounter[cardValueInt]++; 
   }
    bool isRoyal = false;
    if(valueCounter[0] == 1 && valueCounter[9] == 1 && valueCounter[10] == 1 && valueCounter[11] == 1 && valueCounter[12] == 1) isRoyal = true;
    if(CheckTests.CheckSuits(hand) && CheckTests.Sequential(hand) && isRoyal)
    {
      strongestDeck = temp;
      strongestHandType = 10;
      return true;
    }
    return false;
  }

}

public class CheckTests //Supplementary Classes
{
  public static bool CheckSuits(Deck hand) //Helps with Hands that require common suits (flushes)
  {
    int sumdiamonds = 0;
    int sumclubs = 0;
    int sumhearts = 0;
    int sumspades = 0;

    for(int iterator = 0; iterator < 5; iterator ++)
    {
      if(hand.deck[iterator].suit == "Clubs")    sumclubs++; 
      if(hand.deck[iterator].suit == "Diamonds") sumdiamonds++;
      if(hand.deck[iterator].suit == "Hearts")   sumhearts++;
      if(hand.deck[iterator].suit == "Spades")   sumspades++;
    }

    if(sumclubs == 5 || sumdiamonds == 5 || sumhearts == 5 || sumspades == 5)
    {
      return true;
    }
    return false;
  }
  public static bool Sequential(Deck hand) 
  {
    bool[] values = new bool[14];
    for( int iterator = 0; iterator < 5; iterator++)
    {
      string cardValue = hand.deck[iterator].value;
      int cardValueInt;
      if(cardValue == "A") cardValueInt = 1;
      else if(cardValue == "J") cardValueInt = 11;
      else if(cardValue == "Q") cardValueInt = 12;
      else if(cardValue == "K") cardValueInt = 13;
      else cardValueInt = Int32.Parse(cardValue);
      values[cardValueInt] = true; 
   }

    int counter = 0;
    for( int iterator = 0; iterator < 5; iterator++) 
    {
      if(values[iterator] == true) counter++;
      if(values[iterator] == false) counter = 0; // means its not in or
      if(counter == 5) return true;// 5 in a row   
    } 
    return false;
  } 
}









