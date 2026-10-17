using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using System;
using System.Collections.Generic;

namespace Hundreds
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch spriteBatch;
        int x = 50;
        int y = 50;
        int xSpeed = 5;
        int ySpeed = 5;
        int height = 0;
        int width = 0;
        List<Circle> circles;
        Circle circle;
        SpriteFont font;

        Random rnd = new Random();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            font = Content.Load<SpriteFont>("File");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            width = GraphicsDevice.Viewport.Width;
            height = GraphicsDevice.Viewport.Height;
            // TODO: use this.Content to load your game content here
            circles = new List<Circle>();

           
            for (int i = 0; i < 2; i++)
            {
                int xvalue = rnd.Next(0, width);
                int yvalue = rnd.Next(0, height);
                circles.Add(new Circle(new Vector2(xvalue, yvalue), new Vector2(5, 5), 50, 500, Color.MediumPurple, 6));
            }
            
           


        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            MouseState ms = Mouse.GetState();


            for (int i = 0; i < circles.Count; i++)
            {
                circles[i].Update(width, height, circles, ms);
                if (circles[i].endGame())
                {
                    this.Exit();
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Level: "+(circles.Count - 1).ToString(), new Vector2(width - 200, 20), Color.Black);
            spriteBatch.DrawString(font, "Level: "+(circles.Count - 1).ToString(), new Vector2(width - 200, 20), Color.Black);

            for (int i = 0; i < circles.Count; i++)
            {
                circles[i].Draw(spriteBatch, font);

            }

            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
