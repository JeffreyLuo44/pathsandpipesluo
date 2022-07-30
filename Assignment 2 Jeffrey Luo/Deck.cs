using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_2_Jeffrey_Luo
{
    class Deck
    {
        //List of game cards in deck
        protected List<GameCard> _deckSetup;
        //List of temporary game cards in deck
        protected List<GameCard> _tempList;
        //Card type of cards in deck
        protected Constants.CARD_TYPE _cardType;
        //Random number generator object
        Random _random;
        /// <summary>
        /// Each player object has their own deck object. This creates two sets of gamecards from 1-13 randomly shuffled
        /// </summary>
        /// <param name="cardType">Can be either pipes or paths</param>
        public Deck(Constants.CARD_TYPE cardType)
        {
            _deckSetup = new List<GameCard>();
            _tempList = new List<GameCard>();
            _random = new Random();
            _cardType = cardType;
        }

        /// <summary>
        /// Can be either pipes or paths
        /// </summary>
        public Constants.CARD_TYPE CardType
        {
            get { return _cardType; }
        }

        /// <summary>
        /// The list of gamecards in the deck once the deck has been set up
        /// </summary>
        public List<GameCard> DeckSetup
        {
            get { return _deckSetup; }
        }

        /// <summary>
        /// Creates two sets of gamecards from 1-13 and calls a shuffle method
        /// </summary>
        public void SetupDeck()
        {
            //Creates two sets
            for (int j = 0; j < 2; j++)
            {
                //Makes each gamecard from 1-13
                for (int i = 0; i < Constants.DECK_SIZE; i++)
                {
                    GameCard newPathCard = new GameCard(CardType, i + 1);
                    _deckSetup.Add(newPathCard);
                }
            }
            //Calls the deck to be shuffled
            RampUpShuffle(_deckSetup);
            _deckSetup.Clear();
            //Puts the shuffled cards from the temporary list into actual deck setup list
            foreach(GameCard c in _tempList)
            {
                _deckSetup.Add(c);
            }
        }

        /// <summary>
        /// Ordered gamcards get placed into a temporary list to be shuffled by means of swapping cards in that list
        /// </summary>
        /// <param name="gameCards">The list of unshuffled and ordered cards</param>
        public void RampUpShuffle(List<GameCard> gameCards)
        {
            _tempList.Clear();
            foreach (GameCard c in gameCards)
            {
                _tempList.Add(c);
            }
            //Swap each card in the list with another card until the last card in the deck is swapped
            for (int i = 0; i < _tempList.Count; i++)
            {
                int cardPos1 = i;
                //Gets a random card to swap with
                int cardPos2 = _random.Next(0, _tempList.Count);

                SwapCards(cardPos1, cardPos2);
            }
        }

        /// <summary>
        /// Swaps the index position of two cards
        /// </summary>
        /// <param name="cardPos1">The "ramping" up first card</param>
        /// <param name="cardPos2">The random second card</param>
        protected void SwapCards(int cardPos1, int cardPos2)
        {
            GameCard tempCard = _tempList[cardPos1];
            _tempList[cardPos1] = _tempList[cardPos2];
            _tempList[cardPos2] = tempCard;
        }

        /// <summary>
        /// Takes the first card from the deck
        /// </summary>
        /// <returns>Card that is at the first position</returns>
        public Card DealCard()
        {
            //Takes the card that's first in that particular shuffled card list
            Card drawnCard = DeckSetup[0];
            DeckSetup.RemoveAt(0);
            return drawnCard;
        }
    }
}
