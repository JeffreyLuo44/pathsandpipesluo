using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Assignment_2_Jeffrey_Luo
{
    class GameCard:Card
    {
        /// <summary>
        /// Passes in the cardtype and number of a card. 
        /// This is the only subclass of card where its objects will be placed into the hand
        /// </summary>
        /// <param name="cardType">Can be either pipes or paths</param>
        /// <param name="number">Number value of card (1-13)</param>
        public GameCard(Constants.CARD_TYPE cardType, int number):base(cardType, number)
        {

        }

        /// <summary>
        /// Draws the gamecard
        /// </summary>
        /// <param name="paper">Graphics to draw on</param>
        public override void Draw(Graphics paper, int width, int height)
        {
            int p = 0;
            Rectangle cardRect = new Rectangle(XPos, YPos, width, height);
            //Start and end points of vertical line, respectively
            Point topPoint = new Point((XPos + (width / 2)), YPos);
            Point bottomPoint = new Point(XPos + (width / 2), YPos + height);
            //Start and end points of horizontal line, respectively
            Point leftPoint = new Point(XPos, YPos + (height / 2));
            Point rightPoint = new Point(XPos + width, YPos + (height / 2));
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
            //Top to bottom line
            paper.DrawLine(Constants.CARD_PENS[p], topPoint, bottomPoint);
            ////Left to right line
            paper.DrawLine(Constants.CARD_PENS[p], leftPoint, rightPoint);
            //Display number on card
            paper.DrawString(Number.ToString(), Constants.TEXT_FONT, Constants.TEXT_BRUSH, cardRect);
        }
    }
}
