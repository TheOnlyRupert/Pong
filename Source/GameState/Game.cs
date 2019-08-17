using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Source.GameState {
    public class GameMain : Game {
        private SpriteBatch _spriteBatch;

        public GameMain() {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height,
                SynchronizeWithVerticalRetrace = false
            };

            Window.AllowUserResizing = true;

            Window.Title = "Pong (TheOnlyRupert Version) - Dev";
            Window.Position = new Point(
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width / 2 - graphics.PreferredBackBufferWidth / 2,
                GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height / 2 - graphics.PreferredBackBufferHeight / 2
            );

            Content.RootDirectory = "Content";
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameStateManager.Instance.SetContent(Content);
            GameStateManager.Instance.AddScreen(new PongGameState(GraphicsDevice));
        }

        protected override void UnloadContent() {
            GameStateManager.Instance.UnloadContent();
        }

        protected override void Update(GameTime gameTime) {
            GameStateManager.Instance.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            GameStateManager.Instance.Draw(_spriteBatch);
            base.Draw(gameTime);
        }
    }
}