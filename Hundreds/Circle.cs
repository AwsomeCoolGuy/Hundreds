using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Hundreds
{
    internal class Circle
    {
        Vector2 Position;
        Vector2 Speed;
        float radius;
        int sides;
        Color color;
        int thickness;
        int startingradius;
        static bool isGrowing;
        static bool isColliding;
        public Circle(Vector2 position, Vector2 speed, int radius, int sides, Color color, int thickness)
        {
            Position = position;
            Speed = speed;
            this.radius = radius;
            this.sides = sides;
            this.color = color;
            this.thickness = thickness;
            totalSize = 0;
            startingradius = radius;
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawCircle(Position, radius, sides, color, thickness);
            spriteBatch.DrawString(font, ((int)radius - startingradius).ToString(), new Vector2(Position.X, Position.Y), Color.Black);
            spriteBatch.DrawCircle
        }
            
        public int totalSize;
        public void Update(int width, int height, List<Circle> circles, MouseState ms)
        {
            
            Position += Speed;
            //running into sides
            if (Position.X + radius >= width)
            {
                Speed.X = Math.Abs(Speed.X) * -1;
            }
            if (Position.X - radius <= 0)
            {
                Speed.X = Math.Abs(Speed.X);
            }
            if (Position.Y + radius >= height)
            {
                Speed.Y = Math.Abs(Speed.Y) * -1;
            }
            if (Position.Y - radius <= 0)
            {
                Speed.Y = Math.Abs(Speed.Y);
            }

            if ((Math.Sqrt((Math.Pow((Position.X - ms.X), 2) + Math.Pow((Position.Y - ms.Y), 2))) <= radius))
            {
                radius = radius + 0.5f;
                totalSize = ((int)radius - startingradius);

                isGrowing = true;               
            }
            else
            {
                isGrowing = false;
            }

                //running into each other
                for (int i = 0; i < circles.Count; i++)
                {
                    CircleColision(this, circles[i]);


                }
            int sumsize = 0;
            foreach (Circle c in circles)
            {
                sumsize += c.totalSize;
            }
            if (sumsize >= 100)
            {
                Random random = new Random();
                int newamount = circles.Count + 1;
                circles.Clear();
                for (int j = 0; j < newamount; j++)
                {
                    circles.Add(new Circle(new Vector2(random.Next(width),random.Next(height)), new Vector2(5, 5), startingradius, 500, Color.MediumPurple, 6));
                }

            }
        }
        static void CircleColision(Circle circle1, Circle circle2)
        {
            if (circle1 == circle2)return;

            else if ((Math.Sqrt((Math.Pow((circle1.Position.X - circle2.Position.X), 2) + Math.Pow((circle1.Position.Y - circle2.Position.Y), 2))) <= (circle1.radius + circle2.radius)))
            {
                //swap speeds
                Vector2 tempSpeed = circle1.Speed;

                circle1.Speed = circle2.Speed;

                circle2.Speed = tempSpeed;

                isColliding = true;
            }
            else
            {
                isColliding = false;
            }
        }

        public bool endGame()
        {
            if (isColliding && isGrowing)
            {
                return true;
            }
            return false; 
        }
    }
}
