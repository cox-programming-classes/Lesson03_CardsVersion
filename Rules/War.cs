namespace Toolkit.Rules.War;

public static class WarRules
{
    extension(Deck deck)
    {
        /// <summary>
        /// Play a game of War
        /// <param name="random">Inject your RNG here!</param>
        /// </summary>
        public void PlayWar(Random random)
        {
            deck.Shuffle(random);
            Deck player1 = deck; 
            Deck player2 = deck.Split();

            while (player1.Count != 0 || player2.Count != 0)
            {
                var result = deck.PlayRound(player1, player2);
                if(result.winner is null)
                {
                    Console.WriteLine("DRAW!!!!");
                    return;
                }
                result.winner.AddCardsOnBottom(result.pile);

                if (player1.Count == 0)
                {
                    Console.WriteLine("Player 2 Wins!");
                    return;
                }

                if (player2.Count == 0)
                {
                    Console.WriteLine("Player 1 Wins!");
                    return;
                }

                Console.WriteLine(
                    result.winner == player1 ? 
                        "Player 1 Wins Round!" : 
                        "Player 2 Wins Round!");
                Thread.Sleep(500);
            }
            
            
        }

        private RoundResult PlayRound(Deck player1, Deck player2, List<Card> pile = null)
        {
            pile ??= [];
            var card1 = player1.DealOne();
            var card2 = player2.DealOne();
            Console.WriteLine($"Player 1 : {card1}  vs. {card2} : Player 2");
            if (card1 is null && card2 is null)
                return new(pile, null);
            if (card1 is null)
                return new RoundResult([..pile, card2!], player2);
            if(card2 is null)
                return new RoundResult([..pile, card1], player1);


            if (card1.Value.Rank > card2.Value.Rank)
                return new RoundResult(pile, player1);
            if (card2.Value.Rank > card1.Value.Rank)
                return new RoundResult(pile, player2);
            
            pile.AddRange(player1.Deal(3));
            pile.AddRange(player2.Deal(3));
            Console.WriteLine("WAAAARRRRR~!");
            return deck.PlayRound(player1, player2, pile);
        }
    }

    protected record RoundResult(List<Card> pile, Deck? winner);
}
