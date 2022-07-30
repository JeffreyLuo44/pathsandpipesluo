using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Assignment_2_Jeffrey_Luo
{
    class Hand
    {
        //List of game cards in hand
        protected List<GameCard> _handGameCards;
        /// <summary>
        /// Each player object has their own hand object. The hand stores gamecards only.
        /// </summary>
        public Hand()
        {
            _handGameCards = new List<GameCard>();
        }

        /// <summary>
        /// List of gamecards in the player's hand
        /// </summary>
        public List<GameCard> HandCards
        {
            get { return _handGameCards; }
        }

        /// <summary>
        /// Draws cards from the deck until the maximum hand size is reached
        /// </summary>
        /// <param name="deck"></param>
        public void UpdateHand(Deck deck)
        {
            GameCard drawnCard;
            //Until the maximum hand size is reached
            while (HandCards.Count < Constants.HAND_SIZE)
            {
                //If there are no more cards in deck to draw, a message box shows for the player (every turn)
                if (deck.DeckSetup.Count == 0)
                {
                    MessageBox.Show("Warning! Deck Depleted");
                    break;
                }
                //Gets a card from the deck to place into the hand card list
                drawnCard = (GameCard)deck.DealCard();
                _handGameCards.Add(drawnCard);
            }
        }

        /// <summary>
        /// Draws the cards in hand on the player interface
        /// </summary>
        /// <param name="paper"></param>
        public void DrawCardsInHand(Graphics paper)
        {
            //Start x Position is slightly right of left picturebox edge
            int x = Constants.CARD_GAP;
            foreach (GameCard gameCard in HandCards)
            {
                //Assign the x position to the gamecard's current x position
                gameCard.XPos = x;
                gameCard.Draw(paper, Constants.CARD_SIZE, Constants.CARD_SIZE);
                //Increment x postion by a card length and gap
                x += Constants.CARD_SIZE + Constants.CARD_GAP;
            }
        }
    }
}
