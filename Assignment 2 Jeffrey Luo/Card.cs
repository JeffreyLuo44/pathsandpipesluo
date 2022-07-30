using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Assignment_2_Jeffrey_Luo
{
    abstract class Card
    {
        //Card's number value (1-13)
        protected int _number;
        //Card's cardtype
        protected Constants.CARD_TYPE _cardType;
        //Card's current x position
        protected int _xPos;
        //Card's current y position
        protected int _yPos;
        //Whether or not the card is selected
        protected bool _selected;
        /// <summary>
        /// Holds the cardtype and number, the current XPos and YPos of the card, 
        /// abstract draw method to be overriden by card subclasses & whether or not it is selected.
        /// Can pass in mouse x and y coordinates to return a bool value if clicked on.
        /// </summary>
        /// <param name="cardType">Can be either pipes or paths</param>
        /// <param name="number">Number value of card (1-13) or -1 if an obstacle</param>
        public Card(Constants.CARD_TYPE cardType, int number)
        {
            _number = number;
            _cardType = cardType;
            _yPos = Constants.CARD_GAP;
        }

        /// <summary>
        /// Number value of card
        /// </summary>
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }

        /// <summary>
        /// The card type of the card
        /// </summary>
        public Constants.CARD_TYPE CardType
        {
            get { return _cardType ; }
            set { _cardType = value; }
        }

        /// <summary>
        /// Current X position of card
        /// </summary>
        public int XPos
        {
            get { return _xPos; }
            set { _xPos = value; }
        }

        /// <summary>
        /// Current Y position of card
        /// </summary>
        public int YPos
        {
            get { return _yPos; }
            set { _yPos = value; }
        }

        /// <summary>
        /// Whether or not the card is clicked on in the user interface
        /// </summary>
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        /// <summary>
        /// Checks whether the x and y mouse click position is within certain x and y boundaries based on the card position
        /// </summary>
        /// <param name="x">x position of the mouse</param>
        /// <param name="y">y position of the mouse</param>
        /// <returns>Whether or not the mouse click position is on the card</returns>
        public virtual bool IsMouseOn(int x, int y)
        {
            //If the mouse click position is within the card's x and y boundaries, it returns back the bool value true
            if (_xPos <= x && x< _xPos + Constants.CARD_SIZE && _yPos <= y && y<_yPos + Constants.CARD_SIZE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public abstract void Draw(Graphics paper, int width, int height);
    }
}
