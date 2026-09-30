using Toolkit;
using Toolkit.Rules.War;

var random = new Random();

var myDeck = Deck.CreateStandardDeck();

myDeck.Shuffle(random);

myDeck.PlayWar(random);

