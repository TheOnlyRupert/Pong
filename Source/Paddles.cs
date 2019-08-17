using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Source {
    public class Paddles {
        private readonly GraphicsDevice _graphics;
        private readonly Texture2D _lPaddleTexture, _rPaddleTexture;
        private bool _enableLai, _enableRai;
        private Vector2 _lPaddlePosition, _rPaddlePosition;

        /* Textures (in order -> lPaddle, rPaddle, boundingBox */
        public Paddles(IReadOnlyList<Texture2D> texture, GraphicsDevice graphics) {
            _lPaddleTexture = texture[0];
            _rPaddleTexture = texture[1];
            _graphics = graphics;

            _lPaddlePosition = new Vector2(
                _lPaddleTexture.Width / 2 + 64,
                _graphics.PresentationParameters.BackBufferHeight / 2 - _lPaddleTexture.Height / 2
            );

            _rPaddlePosition = new Vector2(
                graphics.PresentationParameters.BackBufferWidth - _rPaddleTexture.Width / 2 - _rPaddleTexture.Width -
                64, graphics.PresentationParameters.BackBufferHeight / 2 - _rPaddleTexture.Height / 2
            );
        }

        public Rectangle LPaddleBoundingBox =>
            new Rectangle(
                (int) _lPaddlePosition.X + _lPaddleTexture.Width - 16, (int) _lPaddlePosition.Y, 16,
                _lPaddleTexture.Height
            );

        public Rectangle RPaddleBoundingBox =>
            new Rectangle((int) _rPaddlePosition.X, (int) _rPaddlePosition.Y, 16, _rPaddleTexture.Height);

        public void Update(Ball ball) {
            int aiSpeed;
            _rPaddlePosition.X = _graphics.PresentationParameters.BackBufferWidth - _rPaddleTexture.Width / 2 -
                _rPaddleTexture.Width - 64;

            if (Keyboard.GetState().IsKeyDown(Keys.Insert)) {
                _enableLai = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Delete)) {
                _enableLai = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.PageUp)) {
                _enableRai = true;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.PageDown)) {
                _enableRai = false;
            }

            if (!_enableLai) {
                if (Keyboard.GetState().IsKeyDown(Keys.W) && _lPaddlePosition.Y > 0) {
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) {
                        _lPaddlePosition.Y -= Reference.PaddleSpeedFast;
                    } else {
                        _lPaddlePosition.Y -= Reference.PaddleSpeedNorm;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.S) && _lPaddlePosition.Y <
                    _graphics.PresentationParameters.BackBufferHeight - _lPaddleTexture.Height) {
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)) {
                        _lPaddlePosition.Y += Reference.PaddleSpeedFast;
                    } else {
                        _lPaddlePosition.Y += Reference.PaddleSpeedNorm;
                    }
                }
            } else {
                aiSpeed = (int) (ball.BallBoundingBox.Y + ball.BallBoundingBox.Height / 2 -
                    (_lPaddlePosition.Y + (_lPaddleTexture.Height / 2 + 0)));
                if (aiSpeed > Reference.PaddleMaxAISpeed * Reference.globalTimeFactor) {
                    aiSpeed = Reference.PaddleMaxAISpeed * Reference.globalTimeFactor;
                } else if (aiSpeed < -Reference.PaddleMaxAISpeed * Reference.globalTimeFactor) {
                    aiSpeed = -Reference.PaddleMaxAISpeed * Reference.globalTimeFactor;
                }

                _lPaddlePosition.Y += aiSpeed;

                if (_lPaddlePosition.Y < 0) {
                    _lPaddlePosition.Y = 0;
                } else if (_lPaddlePosition.Y >
                    _graphics.PresentationParameters.BackBufferHeight - _lPaddleTexture.Height) {
                    _lPaddlePosition.Y = _graphics.PresentationParameters.BackBufferHeight - _lPaddleTexture.Height;
                }
            }

            if (!_enableRai) {
                if (Keyboard.GetState().IsKeyDown(Keys.Up) && _rPaddlePosition.Y > 0) {
                    if (Keyboard.GetState().IsKeyDown(Keys.RightShift)) {
                        _rPaddlePosition.Y -= Reference.PaddleSpeedFast;
                    } else {
                        _rPaddlePosition.Y -= Reference.PaddleSpeedNorm;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Down) && _rPaddlePosition.Y <
                    _graphics.PresentationParameters.BackBufferHeight - _rPaddleTexture.Height) {
                    if (Keyboard.GetState().IsKeyDown(Keys.RightShift)) {
                        _rPaddlePosition.Y += Reference.PaddleSpeedFast;
                    } else {
                        _rPaddlePosition.Y += Reference.PaddleSpeedNorm;
                    }
                }
            } else {
                aiSpeed = (int) (ball.BallBoundingBox.Y + ball.BallBoundingBox.Height / 2 -
                    (_rPaddlePosition.Y + (_rPaddleTexture.Height / 2 + 0)));
                if (aiSpeed > Reference.PaddleMaxAISpeed * Reference.globalTimeFactor) {
                    aiSpeed = Reference.PaddleMaxAISpeed * Reference.globalTimeFactor;
                } else if (aiSpeed < -Reference.PaddleMaxAISpeed * Reference.globalTimeFactor) {
                    aiSpeed = -Reference.PaddleMaxAISpeed * Reference.globalTimeFactor;
                }

                _rPaddlePosition.Y += aiSpeed;

                if (_rPaddlePosition.Y < 0) {
                    _rPaddlePosition.Y = 0;
                } else if (_rPaddlePosition.Y >
                    _graphics.PresentationParameters.BackBufferHeight - _rPaddleTexture.Height) {
                    _rPaddlePosition.Y = _graphics.PresentationParameters.BackBufferHeight - _rPaddleTexture.Height;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(_lPaddleTexture, _lPaddlePosition, Color.White);
            spriteBatch.Draw(_rPaddleTexture, _rPaddlePosition, Color.White);

            if (Reference.enableDrawBoundingBox) {
                spriteBatch.Draw(
                    _lPaddleTexture,
                    new Rectangle(
                        LPaddleBoundingBox.X, LPaddleBoundingBox.Y, LPaddleBoundingBox.Width, LPaddleBoundingBox.Height
                    ), Color.Black
                );
                spriteBatch.Draw(
                    _lPaddleTexture,
                    new Rectangle(
                        RPaddleBoundingBox.X, RPaddleBoundingBox.Y, RPaddleBoundingBox.Width, RPaddleBoundingBox.Height
                    ), Color.Black
                );
            }
        }
    }
}