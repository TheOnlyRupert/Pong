using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Source.GameState {
    public abstract class GameState : IGameState {
        protected readonly GraphicsDevice graphicsDevice;

        protected GameState(GraphicsDevice graphicsDevice) {
            this.graphicsDevice = graphicsDevice;
        }

        public abstract void Initialize();
        public abstract void LoadContent(ContentManager content);
        public abstract void UnloadContent();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}