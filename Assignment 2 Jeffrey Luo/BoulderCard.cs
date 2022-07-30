using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Assignment_2_Jeffrey_Luo
{
    class BoulderCard:Card
    {
        /// <summary>
        /// Holds the x position of the boulder card in the player interface
        /// </summary>
        /// <param name="cardType">Can be either pipes or paths</param>
        /// <param name="number">Expected to be -1 as it is an obstacle</param>
        public BoulderCard(Constants.CARD_TYPE cardType, int number):base(cardType, number)
        {
            XPos = Constants.BOULDER_X_POSITION;
        }

        /// <summary>
        /// Draws the boulder card
        /// </summary>
        /// <param name="paper">Graphics to draw on</param>
        public override void Draw(Graphics paper, int width, int height)
        {
            int p = 0;
            Rectangle cardRect = new Rectangle(XPos, YPos, width, height);
            //Changes index of pens/brushes depending on cardtype
            if (CardType == Constants.CARD_TYPE.Pipe)
            {
                p = 1;
            }
            //Rectangle border selected or not
            if (Selected == true)
            {
                paper.DrawRectangle(Constants.BORDER_PENS[p], cardRect);
            }
            else
            {
                paper.DrawRectangle(Constants.BORDER_PENS[2], cardRect);
            }
            //Fills the card background
            paper.FillRectangle(Constants.CARD_BRUSH[p], cardRect);
            //Circle to represent boulder
            paper.FillEllipse(Constants.BOULDER_BRUSH, cardRect);
        }
    }
}
