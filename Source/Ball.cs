using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Source {
    public class Ball {
        private readonly Texture2D _ballTexture;
        private readonly GraphicsDevice _graphics;
        private readonly SoundEffect _hitSoundEffect, _wallSoundEffect, _miss1SoundEffect, _miss2SoundEffect;
        private readonly SpriteFont _newGameText;
        private Vector2 _ballPosition;
        private bool _isNewGame;
        private float _ySpeed, _xSpeed;

        /* Sounds -> (0) hit, (1) wall, (2) miss1, (3) miss2 */
        public Ball(Texture2D ballTexture, IReadOnlyList<SoundEffect> soundEffectList, GraphicsDevice graphics,
            SpriteFont font) {
            _ballTexture = ballTexture;
            _graphics = graphics;
            _newGameText = font;
            _hitSoundEffect = soundEffectList[0];
            _wallSoundEffect = soundEffectList[1];
            _miss1SoundEffect = soundEffectList[2];
            _miss2SoundEffect = soundEffectList[3];
            _ballPosition = new Vector2(_graphics.PresentationParameters.BackBufferWidth / 2 - _ballTexture.Width / 2,
                _graphics.PresentationParameters.BackBufferHeight / 2 - _ballTexture.Height / 2);
        }

        public Rectangle BallBoundingBox => new Rectangle((int) _ballPosition.X, (int) _ballPosition.Y,
            _ballTexture.Width, _ballTexture.Height);

        private void NewRound(bool isPlayerLeft) {
            _ballPosition = new Vector2(_graphics.PresentationParameters.BackBufferWidth / 2 - _ballTexture.Width / 2,
                _graphics.PresentationParameters.BackBufferHeight / 2 - _ballTexture.Height / 2);

            var rand = new Random();
            if (isPlayerLeft) {
                if (rand.Next(0, 2) == 0) {
                    _xSpeed = -Reference.BallStartingSpeed;
                    _ySpeed = 2;
                } else {
                    _xSpeed = -Reference.BallStartingSpeed;
                    _ySpeed = -2;
                }
            } else {
                if (rand.Next(0, 2) == 0) {
                    _xSpeed = Reference.BallStartingSpeed;
                    _ySpeed = 2;
                } else {
                    _xSpeed = Reference.BallStartingSpeed;
                    _ySpeed = -2;
                }
            }
        }

        public void Update(Paddles paddles) {
            if (!_isNewGame) {
                if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                    var rand = new Random();
                    NewRound(rand.Next(0, 2) == 1);
                    _isNewGame = !_isNewGame;
                }
            } else {
                _ballPosition.Y += _ySpeed * Reference.globalTimeFactor;
                _ballPosition.X += _xSpeed * Reference.globalTimeFactor;

                if (_ballPosition.Y > _graphics.PresentationParameters.BackBufferHeight - _ballTexture.Height) {
                    _wallSoundEffect.Play();
                    _ballPosition.Y = _graphics.PresentationParameters.BackBufferHeight - _ballTexture.Height - 1;
                    _ySpeed *= -1;
                }

                if (_ballPosition.Y < 0) {
                    _wallSoundEffect.Play();
                    _ballPosition.Y = 1;
                    _ySpeed *= -1;
                }

                if (_ballPosition.X > _graphics.PresentationParameters.BackBufferWidth - _ballTexture.Width) {
                    _miss1SoundEffect.Play();
                    Score.lScore++;
                    NewRound(true);
                }

                if (_ballPosition.X < 0) {
                    _miss2SoundEffect.Play();
                    Score.rScore++;
                    NewRound(false);
                }

                if (BallBoundingBox.Intersects(paddles.LPaddleBoundingBox)) {
                    _hitSoundEffect.Play();
                    _ballPosition.X = paddles.LPaddleBoundingBox.Right + 1;

                    _xSpeed *= -1.1f;
                    /* Set speed limit */
                    if (_xSpeed > Reference.BallMaxSpeed) _xSpeed = Reference.BallMaxSpeed;

                    _ySpeed = _xSpeed * ((float) (BallBoundingBox.Center.Y - paddles.LPaddleBoundingBox.Center.Y)
                                         / paddles.LPaddleBoundingBox.Height * 2);
                }

                if (BallBoundingBox.Intersects(paddles.RPaddleBoundingBox)) {
                    _hitSoundEffect.Play();
                    _ballPosition.X = paddles.RPaddleBoundingBox.Left - _ballTexture.Width - 1;

                    _xSpeed *= -1.1f;
                    /* Set speed limit */
                    if (_xSpeed > Reference.BallMaxSpeed) _xSpeed = Reference.BallMaxSpeed;

                    _ySpeed = -_xSpeed * ((float) (BallBoundingBox.Center.Y - paddles.RPaddleBoundingBox.Center.Y)
                                          / paddles.RPaddleBoundingBox.Height * 2);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            if (!_isNewGame) {
                spriteBatch.DrawString(_newGameText, Reference.NewGame, new Vector2(
                    _graphics.PresentationParameters.BackBufferWidth / 2 + 0 -
                    _newGameText.MeasureString(Reference.NewGame).Length() / 2,
                    _graphics.PresentationParameters.BackBufferHeight / 2 + 0), Color.White);
            } else {
                spriteBatch.Draw(_ballTexture, _ballPosition, Color.White);

                if (Reference.enableDrawBoundingBox)
                    spriteBatch.Draw(_ballTexture,
                        new Rectangle(BallBoundingBox.X, BallBoundingBox.Y, BallBoundingBox.Width,
                            BallBoundingBox.Height), Color.Black);
            }
        }
    }
}