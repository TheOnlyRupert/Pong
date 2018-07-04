using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Source.GameState {
    public class PongGameState : GameState {
        //private Texture2D _backgroundTexture;
        private Ball _ball;
        private Debug _debug;
        private Paddles _paddles;
        private Score _score;
        private SpriteBatch _spriteBatch;

        public PongGameState(GraphicsDevice graphicsDevice) : base(graphicsDevice) { }

        public override void Initialize() { }

        public override void LoadContent(ContentManager content) {
            _spriteBatch = new SpriteBatch(graphicsDevice);

            //_backgroundTexture = content.Load<Texture2D>("image/background");

            _paddles = new Paddles(new[] {
                content.Load<Texture2D>("tile/l_paddle"),
                content.Load<Texture2D>("tile/r_paddle")
            }, graphicsDevice);

            _ball = new Ball(content.Load<Texture2D>("tile/ball"), new[] {
                content.Load<SoundEffect>("sound/hit"), content.Load<SoundEffect>("sound/wall"),
                content.Load<SoundEffect>("sound/miss1"), content.Load<SoundEffect>("sound/miss2")
            }, graphicsDevice, content.Load<SpriteFont>("font/consolas_16"));

            _debug = new Debug(content.Load<SpriteFont>("font/consolas_16"), graphicsDevice);

            _score = new Score(content.Load<SpriteFont>("font/consolas_32"), new[] {
                content.Load<SoundEffect>("sound/background"), content.Load<SoundEffect>("sound/winner")
            }, graphicsDevice);
        }

        public override void UnloadContent() { }

        public override void Update(GameTime gameTime) {
            _debug.Update(gameTime);
            _paddles.Update(_ball);
            _score.Update();
            _ball.Update(_paddles);
        }

        public override void Draw(SpriteBatch spriteBatch) {
            graphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin();
            /*_spriteBatch.Draw(_backgroundTexture, new Rectangle(0, 0,
                graphicsDevice.PresentationParameters.BackBufferWidth,
                graphicsDevice.PresentationParameters.BackBufferHeight), Color.White);*/
            _paddles.Draw(_spriteBatch);
            _score.Draw(_spriteBatch);
            _ball.Draw(_spriteBatch);
            _debug.Draw(_spriteBatch);
            _spriteBatch.End();
        }
    }
}