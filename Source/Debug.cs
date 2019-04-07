using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Source {
    public class Debug {
        private readonly SpriteFont _debugFont;
        private readonly GraphicsDevice _graphics;
        private TimeSpan _elapsedTime = TimeSpan.Zero;
        private int _frameRate, _frameCounter;

        public Debug(SpriteFont font, GraphicsDevice graphics) {
            _debugFont = font;
            _graphics = graphics;
        }

        public void Update(GameTime gameTime) {
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime > TimeSpan.FromSeconds(1)) {
                _elapsedTime -= TimeSpan.FromSeconds(1);
                _frameRate = _frameCounter;
                _frameCounter = 0;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D1)) {
                Reference.globalTimeFactor = 1;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D2)) {
                Reference.globalTimeFactor = 2;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D3)) {
                Reference.globalTimeFactor = 3;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D4)) {
                Reference.globalTimeFactor = 0;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            _frameCounter++;
            var fps = $"fps: {_frameRate}\nmem: {GC.GetTotalMemory(false)}";
            var timeFactor = $"Time Factor: {Reference.globalTimeFactor}x ";

            spriteBatch.DrawString(_debugFont, fps, new Vector2(0, 0), Color.White);

            spriteBatch.DrawString(_debugFont, timeFactor, new Vector2(
                    _graphics.PresentationParameters.BackBufferWidth -
                    _debugFont.MeasureString(timeFactor).Length(),
                    _graphics.PresentationParameters.BackBufferHeight - _debugFont.LineSpacing),
                Color.White);
        }
    }
}