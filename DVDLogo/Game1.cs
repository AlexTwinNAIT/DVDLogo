using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace DVDLogo
{

    public struct Normals
    {
        public static readonly Vector2 top = new Vector2(0, -1);
        public static readonly Vector2 bottom = new Vector2(0, 1);
        public static readonly Vector2 left = new Vector2(1, 0);
        public static readonly Vector2 right = new Vector2(-1, 0);
    }


    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _dvdLogo;

        private Vector2 _moveDirection; // If i was a better man, I'd just make an class that has all of this functionality built in but whatever FWIW
        private Vector2 _logoPosition;
        private Vector2 _lastPosition;
        private Vector2 ScreenSize = new Vector2(840, 620);

        private Random _random = new Random();

        private Color _currentColor = Color.CadetBlue;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private Color GetColor()
        {
            return new Color((uint)_random.Next() % (0xffffff+1));// matthew told me to do this -- 
        }

        private Vector2 GetNormal() // if anyone knows how to do this a better way than 3 else if statements i'll be forever in your debt.
        {
            Vector2 selectedVector = Vector2.Zero;
            if (_logoPosition.Y < 0) // hit the top side of the screen!
            {
                selectedVector = Normals.top;
            }
            else if (_logoPosition.X > ScreenSize.X - _dvdLogo.Width) // hit the right side of the screen!
            {
                selectedVector = Normals.right;
            }
            else if (_logoPosition.Y > ScreenSize.Y - _dvdLogo.Height) // hit the bottom side of the screen!
            {
                selectedVector = Normals.bottom;
            }
            else if (_logoPosition.X < 0) // hit the left side of the screen!
            {
                selectedVector = Normals.left;
            }

            return selectedVector;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = (int)ScreenSize.X;
            _graphics.PreferredBackBufferHeight = (int)ScreenSize.Y;
            _graphics.ApplyChanges();

            _moveDirection = new Vector2((float)_random.NextDouble(), (float)_random.NextDouble());
            _moveDirection = Vector2.Normalize(_moveDirection);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _dvdLogo = Content.Load<Texture2D>("dvd-logo");
        }



        protected override void Update(GameTime gameTime)
        {
            _logoPosition = _logoPosition + _moveDirection * 5;
            Vector2 normalVector = GetNormal();
                            
            if (normalVector != Vector2.Zero) // if the logo actually hit something
            {
                _currentColor = GetColor();
                Debug.WriteLine(_moveDirection);
                Vector2 reflectedVector;

                // r=d−2(d⋅n)n

                reflectedVector = Vector2.Reflect(_logoPosition - _lastPosition, normalVector);

                
                _moveDirection = Vector2.Normalize(reflectedVector);
                Debug.WriteLine(_moveDirection);
                
            }


            _lastPosition = _logoPosition;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            _spriteBatch.Begin();
            _spriteBatch.Draw(_dvdLogo, _logoPosition,_currentColor);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
