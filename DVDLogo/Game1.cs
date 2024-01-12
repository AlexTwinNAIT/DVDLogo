using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;

namespace DVDLogo
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _dvdLogo;
        private Vector2 _moveDirection;// If i was a better man, I'd just make an class that has all of this functionality built in but whatever FWIW
        private Vector2 _logoPosition;
        private Vector2 _lastPosition;
        private Vector2 ScreenSize = new Vector2(840, 620);

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private void GetMoveDirection()
        {
             Random randomNum = new Random();
             _moveDirection = new Vector2((float)randomNum.NextDouble(), (float)randomNum.NextDouble());
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            _graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
            _graphics.ApplyChanges();
            GetMoveDirection();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _dvdLogo = Content.Load<Texture2D>("dvd-logo");
        }

        protected override void Update(GameTime gameTime)
        {
            

            _logoPosition = _logoPosition + _moveDirection * 4;

            if (_logoPosition.X > ScreenSize.X - _dvdLogo.Width || _logoPosition.X < 0 || _logoPosition.Y > ScreenSize.Y - _dvdLogo.Height || _logoPosition.Y < 0)
            {
                Vector2 hitPos = _logoPosition;
                Vector2 incidentVector = Vector2.Normalize(_lastPosition);
                Vector2 normalVector = -incidentVector;

                _moveDirection = Vector2.Normalize(Vector2.Reflect(_lastPosition, normalVector));
            }

            _lastPosition = _logoPosition;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_dvdLogo, _logoPosition,Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
