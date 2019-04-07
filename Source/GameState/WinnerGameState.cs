using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Source.GameState {
    public class WinnerGameState : GameState {
        private readonly bool _isLeftWinner;
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont, _spriteFont2;

        public WinnerGameState(GraphicsDevice graphicsDevice, bool isLeftWinner) : base(graphicsDevice) {
            _isLeftWinner = isLeftWinner;
        }

        public override void Initialize() { }

        public override void LoadContent(ContentManager content) {
            _spriteBatch = new SpriteBatch(graphicsDevice);
            _spriteFont = content.Load<SpriteFont>("font/consolas_32");
            _spriteFont2 = content.Load<SpriteFont>("font/consolas_16");
        }

        public override void UnloadContent() { }

        public override void Update(GameTime gameTime) {
            if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                GameStateManager.Instance.ChangeScreen(new PongGameState(graphicsDevice));
            }
        }

        public override void Draw(SpriteBatch spriteBatch) {
            graphicsDevice.Clear(Color.Purple);

            _spriteBatch.Begin();
            if (_isLeftWinner) {
                _spriteBatch.DrawString(_spriteFont, Reference.PlayerLWins, new Vector2(
                        graphicsDevice.PresentationParameters.BackBufferWidth / 2 + 0 -
                        _spriteFont.MeasureString(Reference.PlayerLWins).Length() / 2,
                        graphicsDevice.PresentationParameters.BackBufferHeight / 3 + 0),
                    Color.White);
            } else {
                _spriteBatch.DrawString(_spriteFont, Reference.PlayerRWins, new Vector2(
                        graphicsDevice.PresentationParameters.BackBufferWidth / 2 + 0 -
                        _spriteFont.MeasureString(Reference.PlayerRWins).Length() / 2,
                        graphicsDevice.PresentationParameters.BackBufferHeight / 3 + 0),
                    Color.White);
            }

            _spriteBatch.DrawString(_spriteFont2, Reference.NewGame, new Vector2(
                graphicsDevice.PresentationParameters.BackBufferWidth / 2 + 0 -
                _spriteFont2.MeasureString(Reference.NewGame).Length() / 2,
                graphicsDevice.PresentationParameters.BackBufferHeight -
                graphicsDevice.PresentationParameters.BackBufferHeight / 4 + 0), Color.White);
            _spriteBatch.End();
        }
    }
}