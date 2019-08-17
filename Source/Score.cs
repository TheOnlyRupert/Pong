using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Pong.Source.GameState;

namespace Pong.Source {
    public class Score {
        public static int lScore, rScore;
        private readonly SoundEffectInstance _backgroundSoundEffectInstance, _winnerSoundEffectInstance;
        private readonly GraphicsDevice _graphics;
        private readonly SpriteFont _scoreText;

        public Score(SpriteFont font, IReadOnlyList<SoundEffect> soundEffect, GraphicsDevice graphics) {
            _scoreText = font;
            _graphics = graphics;

            _backgroundSoundEffectInstance = soundEffect[0].CreateInstance();
            _winnerSoundEffectInstance = soundEffect[1].CreateInstance();

            _backgroundSoundEffectInstance.IsLooped = true;
            _backgroundSoundEffectInstance.Play();
        }

        public void Update() {
            if (lScore > 9 || rScore > 9) {
                GameStateManager.Instance.ChangeScreen(new WinnerGameState(_graphics, lScore > 9));
                _backgroundSoundEffectInstance.Stop();
                _winnerSoundEffectInstance.Play();
                lScore = 0;
                rScore = 0;
                Reference.globalTimeFactor = 1;
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            string score = lScore + "  -  " + rScore;

            spriteBatch.DrawString(
                _scoreText, score,
                new Vector2(
                    _graphics.PresentationParameters.BackBufferWidth / 2 + 0 -
                    _scoreText.MeasureString(score).Length() / 2,
                    _graphics.PresentationParameters.BackBufferHeight / 32 + 0
                ), Color.White
            );
        }
    }
}