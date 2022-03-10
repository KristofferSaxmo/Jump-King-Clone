using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Jump_King_Clone
{
    public class Text : Component
    {
        private static readonly int NewlineSpacing = 9;

        private Rectangle _rectangle;
        private Vector2 _currentLetterPosition;
        private readonly int _scale;
        private readonly List<Letter> _letters = new List<Letter>();
        private readonly static Dictionary<char, Rectangle> _rectangles = new Dictionary<char, Rectangle>()
            {
                {' ', new Rectangle(0, 0, 8, 8)},
                {'!', new Rectangle(8, 0, 8, 8)},
                {'"', new Rectangle(16, 0, 8, 8)},
                {'#', new Rectangle(24, 0, 8, 8)},
                {'$', new Rectangle(32, 0, 8, 8)},
                {'%', new Rectangle(40, 0, 8, 8)},
                {'&', new Rectangle(48, 0, 8, 8)},
                {'\'', new Rectangle(56, 0, 8, 8)},
                {'(', new Rectangle(64, 0, 8, 8)},
                {')', new Rectangle(72, 0, 8, 8)},
                {'*', new Rectangle(80, 0, 8, 8)},
                {'+', new Rectangle(88, 0, 8, 8)},
                {',', new Rectangle(96, 0, 8, 8)},
                {'-', new Rectangle(104, 0, 8, 8)},
                {'.', new Rectangle(112, 0, 8, 8)},
                {'/', new Rectangle(120, 0, 8, 8)},

                {'0', new Rectangle(0, 8, 8, 8)},
                {'1', new Rectangle(8, 8, 8, 8)},
                {'2', new Rectangle(16, 8, 8, 8)},
                {'3', new Rectangle(24, 8, 8, 8)},
                {'4', new Rectangle(32, 8, 8, 8)},
                {'5', new Rectangle(40, 8, 8, 8)},
                {'6', new Rectangle(48, 8, 8, 8)},
                {'7', new Rectangle(56, 8, 8, 8)},
                {'8', new Rectangle(64, 8, 8, 8)},
                {'9', new Rectangle(72, 8, 8, 8)},
                {':', new Rectangle(80, 8, 8, 8)},
                {';', new Rectangle(88, 8, 8, 8)},
                {'<', new Rectangle(96, 8, 8, 8)},
                {'=', new Rectangle(104, 8, 8, 8)},
                {'>', new Rectangle(112, 8, 8, 8)},
                {'?', new Rectangle(120, 8, 8, 8)},

                {'@', new Rectangle(0, 16, 8, 8)},
                {'A', new Rectangle(8, 16, 8, 8)},
                {'B', new Rectangle(16, 16, 8, 8)},
                {'C', new Rectangle(24, 16, 8, 8)},
                {'D', new Rectangle(32, 16, 8, 8)},
                {'E', new Rectangle(40, 16, 8, 8)},
                {'F', new Rectangle(48, 16, 8, 8)},
                {'G', new Rectangle(56, 16, 8, 8)},
                {'H', new Rectangle(64, 16, 8, 8)},
                {'I', new Rectangle(72, 16, 8, 8)},
                {'J', new Rectangle(80, 16, 8, 8)},
                {'K', new Rectangle(88, 16, 8, 8)},
                {'L', new Rectangle(96, 16, 8, 8)},
                {'M', new Rectangle(104, 16, 8, 8)},
                {'N', new Rectangle(112, 16, 8, 8)},
                {'O', new Rectangle(120, 16, 8, 8)},

                {'P', new Rectangle(0, 24, 8, 8)},
                {'Q', new Rectangle(8, 24, 8, 8)},
                {'R', new Rectangle(16, 24, 8, 8)},
                {'S', new Rectangle(24, 24, 8, 8)},
                {'T', new Rectangle(32, 24, 8, 8)},
                {'U', new Rectangle(40, 24, 8, 8)},
                {'V', new Rectangle(48, 24, 8, 8)},
                {'W', new Rectangle(56, 24, 8, 8)},
                {'X', new Rectangle(64, 24, 8, 8)},
                {'Y', new Rectangle(72, 24, 8, 8)},
                {'Z', new Rectangle(80, 24, 8, 8)},
                {'[', new Rectangle(88, 24, 8, 8)},
                {'\\', new Rectangle(96, 24, 8, 8)},
                {']', new Rectangle(104, 24, 8, 8)},
                {'^', new Rectangle(112, 24, 8, 8)},
                {'_', new Rectangle(120, 24, 8, 8)},

                {'`', new Rectangle(0, 32, 8, 8)},
                {'a', new Rectangle(8, 32, 8, 8)},
                {'b', new Rectangle(16, 32, 8, 8)},
                {'c', new Rectangle(24, 32, 8, 8)},
                {'d', new Rectangle(32, 32, 8, 8)},
                {'e', new Rectangle(40, 32, 8, 8)},
                {'f', new Rectangle(48, 32, 8, 8)},
                {'g', new Rectangle(56, 32, 8, 8)},
                {'h', new Rectangle(64, 32, 8, 8)},
                {'i', new Rectangle(72, 32, 8, 8)},
                {'j', new Rectangle(80, 32, 8, 8)},
                {'k', new Rectangle(88, 32, 8, 8)},
                {'l', new Rectangle(96, 32, 8, 8)},
                {'m', new Rectangle(104, 32, 8, 8)},
                {'n', new Rectangle(112, 32, 8, 8)},
                {'o', new Rectangle(120, 32, 8, 8)},

                {'p', new Rectangle(0, 40, 8, 8)},
                {'q', new Rectangle(8, 40, 8, 8)},
                {'r', new Rectangle(16, 40, 8, 8)},
                {'s', new Rectangle(24, 40, 8, 8)},
                {'t', new Rectangle(32, 40, 8, 8)},
                {'u', new Rectangle(40, 40, 8, 8)},
                {'v', new Rectangle(48, 40, 8, 8)},
                {'w', new Rectangle(56, 40, 8, 8)},
                {'x', new Rectangle(64, 40, 8, 8)},
                {'y', new Rectangle(72, 40, 8, 8)},
                {'z', new Rectangle(80, 40, 8, 8)},
                {'{', new Rectangle(88, 40, 8, 8)},
                {'|', new Rectangle(96, 40, 8, 8)},
                {'}', new Rectangle(104, 40, 8, 8)},
                {'~', new Rectangle(112, 40, 8, 8)}
            };

        public string Content { get; private set; }
        public Rectangle Rectangle
        {
            get { return _rectangle; }
        }
        public Color Color { get; set; }
        public Vector2 Position { get; private set; }

        public Text(string content, int scale, Vector2 position)
        {
            Color = Color.White;
            _scale = scale;
            Position = position;
            SetContent(content);
        }

        public void SetContent(string content)
        {
            if (Content == content) return;
            Content = string.Empty;
            _letters.Clear();
            _currentLetterPosition = Position;
            _rectangle = new Rectangle((int)Position.X, (int)Position.Y, 0, 0);
            foreach (char character in content)
            {
                AddLetter(character);
            }
        }

        public void AddLetter(char character)
        {
            if (character == '\n')
            {
                _currentLetterPosition.Y += NewlineSpacing * _scale;
                _currentLetterPosition.X = Position.X;
                return;
            }

            _letters.Add(new Letter(GetLetterYPos(character, _currentLetterPosition), _rectangles[character], _scale, character));
            Letter letter = _letters[_letters.Count - 1];

            _currentLetterPosition.X += letter.Rectangle.Width + _scale;

            _rectangle = new Rectangle(
                Math.Min(Rectangle.X, letter.Rectangle.X),
                Math.Min(Rectangle.Y, letter.Rectangle.Y),
                Math.Max(Rectangle.Width, letter.Rectangle.Width + letter.Rectangle.X - (int)Position.X),
                Math.Max(Rectangle.Height, letter.Rectangle.Height + letter.Rectangle.Y - (int)Position.Y));

            Content += character;
        }

        public void SetPosition(Vector2 position)
        {
            Position = position;
            _currentLetterPosition = position;
            _rectangle = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            foreach (Letter letter in _letters)
            {
                if (letter.Character == '\n')
                {
                    _currentLetterPosition.Y += NewlineSpacing * _scale;
                    _currentLetterPosition.X = Position.X;
                    return;
                }

                letter.SetPosition(GetLetterYPos(letter.Character, _currentLetterPosition));
                _currentLetterPosition.X += letter.Rectangle.Width + _scale;

                _rectangle = new Rectangle(
                    Math.Min(Rectangle.X, letter.Rectangle.X),
                    Math.Min(Rectangle.Y, letter.Rectangle.Y),
                    Math.Max(Rectangle.Width, letter.Rectangle.Width + letter.Rectangle.X - (int)Position.X),
                    Math.Max(Rectangle.Height, letter.Rectangle.Height + letter.Rectangle.Y - (int)Position.Y));
            }
        }

        private Vector2 GetLetterYPos(char character, Vector2 position)
        {
            foreach (char c in "'\"`")
            {
                if (character == c)
                {
                    position.Y -= 1 * _scale;
                    goto End;
                }
            }
            foreach (char c in ",_")
            {
                if (character == c)
                {
                    position.Y += 1 * _scale;
                    goto End;
                }
            }
        End:
            return position;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Game1.DefaultTex, Rectangle, Color.Red);
            foreach (Letter letter in _letters)
            {
                letter.Draw(gameTime, spriteBatch, Color);
            }
        }
    }
}
