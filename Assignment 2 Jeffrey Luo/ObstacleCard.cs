using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Assignment_2_Jeffrey_Luo
{
    class ObstacleCard:Card
    {
        //Obstacle card's number of tokens
        protected int _countDownTokens;
        /// <summary>
        /// Holds the x position of the obstacle card in the player interface & maximum number of countdown tokens
        /// </summary>
        /// <param name="cardType">Can be either pipes or paths</param>
        /// <param name="number">Expected to be -1 as it is an obstacle</param>
        public ObstacleCard(Constants.CARD_TYPE cardType, int number) : base(cardType, number)
        {
            XPos = Constants.OBSTACLE_X_POSITION;
            _countDownTokens = Constants.COUNT_DOWN_NUM;
        }

        /// <summary>
        /// Number of countdown tokens an obstacle card has
        /// </summary>
        public int CountDownTokens
        {
            get { return _countDownTokens; }
            set { _countDownTokens = value; }
        }

        /// <summary>
        /// Decrement the number of countdown tokens by 1 from the obstacle card
        /// </summary>
        /// <returns>The remaining number of countdown tokens</returns>
        public int CountDownAToken()
        {
            _countDownTokens -= 1;
            return _countDownTokens;
        }

        /// <summary>
        /// Draws the obstacle card including the countdown token indicators
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
            //Will draw a circle in the bottom right of the obstacle card if there are countdown tokens. Will not be drawn if there are no countdown tokens
            if (CountDownTokens != 0)
            {
                paper.FillEllipse(Constants.OBSTACLE_BRUSH[p], XPos + width / 2, YPos + height / 2, Constants.OBSTACLE_SYMBOL_SIZE, Constants.OBSTACLE_SYMBOL_SIZE);
            }
            //Number of countdown tokens drawn on obstacle card
            paper.DrawString("CD-" + CountDownTokens.ToString(), Constants.TEXT_FONT, Constants.TEXT_BRUSH, cardRect);
        }
    }
}
