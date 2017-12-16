using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pinball
{
    public class Board<T> where T : IMyDrawable
    {
        private Point size;
        private Point offset;
        //private PinballLevelManager levelManager; 

        private T[,] values;
        private List<T> activeValues;

        public Point Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

        public T this[int i1, int i2]
        {
            get
            {
                return values[i1, i2];
            }
            set
            {
                values[i1, i2] = value;
                if (value != null)
                    activeValues.Add(value);
            }
        }

        public Point Offset
        {
            get
            {
                return offset;
            }

            set
            {
                offset = value;
            }
        }

        public List<T> ActiveValues
        {
            get
            {
                return activeValues;
            }

            set
            {
                activeValues = value;
            }
        }

        public Board(Point size, Point offset)
        {
            this.size = size;
            this.offset = offset;

            values = new T[size.X + 1, size.Y + 1];

            //levelManager = PinballLevelManager.GetInstance();
            activeValues = new List<T>();
        }

        public void Remove(T item)
        {
            activeValues.Remove(item);
            //values.
            //удалить из values!
        }

        public void Update()
        {
            
        }

        public void DrawAll(SpriteBatch spriteBatch)
        {
            foreach (T value in activeValues.Where(value => value != null))
            {
                value.Draw(spriteBatch);
            }
        }
    }
}
