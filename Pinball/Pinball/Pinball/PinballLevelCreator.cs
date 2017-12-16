using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Pinball
{
    public class PinballLevelCreator
    {
        private static PinballLevelCreator boardCreator;
        private Point boardSize;
        private Point offset;

        private Point brickSize;
        private Random random;

        private AbstractBrickFactory currentbrickFactory;
        private Bound bound;
        private int bonusPossibility;

        public int BonusPossibility
        {
            get
            {
                return bonusPossibility;
            }

            set
            {
                if (value > 100)
                {
                    bonusPossibility = 100;
                }
                else
                {
                    bonusPossibility = value < 0 ? 0 : value;
                }                
            }
        }

        public static PinballLevelCreator GetInstance()
        {
            return boardCreator ?? (boardCreator = new PinballLevelCreator());
        }

        public void Initialize(Point boardSize, Point offset, Point brickSize, Bound bound, int bonusPossibility)
        {
            random = new Random();
            this.boardSize = boardSize;
            this.offset = offset;
            this.brickSize = brickSize;
            this.bound = bound;
            BonusPossibility = bonusPossibility;
        }

        private PinballLevelCreator()
        {
        }

        public void SetFactory()
        {
            int possibility = random.Next(100) + 1;
            if (possibility <= bonusPossibility)
                currentbrickFactory = BonusBrickFactory.GetInstance();
            else
                currentbrickFactory = SimpleBrickFactory.GetInstance();
        }


        public List<PinballLevel> CreateFromBitmapList(List<System.Drawing.Bitmap> images)
        {
            return images.Select(CreateFromBitmap).ToList();
        }

        public PinballLevel CreateFromBitmap(System.Drawing.Bitmap image)
        {
           /* if ((image.Height < boardSize.Height) || (image.Width < boardSize.Width))
                throw new Exception("Размер изображения меньше размеров поля");*/

            Board<Brick> board = new Board<Brick>(boardSize, offset);
            List<Rectangle> colliders = new List<Rectangle>();
            board.Size = boardSize;
            board.Offset = offset;
            

            for (int i = 1; i <= Math.Min(image.Width, boardSize.X); i++)
            {
                for (int j = 1; j <= Math.Min(image.Height, boardSize.Y); j++)
                {

                    System.Drawing.Color currentColor = image.GetPixel(i - 1, j - 1);
                    if ((currentColor.R == 0) && (currentColor.B == 0) && (currentColor.G == 0))
                    {
                        board[i, j] = null;
                        continue;
                    }
                    AddNewBrick(board, colliders, i, j, currentColor);
                }
            }

            return new PinballLevel(board, bound, colliders);

        }

        private void AddNewBrick(Board<Brick> board, List<Rectangle> colliders, int i, int j, System.Drawing.Color currentColor)
        {
            SetFactory();
            Transform currentTransform = new Transform(new Vector2((i - 1) * brickSize.X + offset.X,
                (j - 1) * brickSize.Y + offset.Y), Vector2.Zero);
            Brick newBrick = currentbrickFactory.Create(currentColor, currentTransform, brickSize);
            board[i, j] = newBrick;
            colliders.Add(newBrick.Body.Collider);
        }
    }
}
