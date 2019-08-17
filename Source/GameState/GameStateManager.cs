using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Source.GameState {
    public class GameStateManager {
        private static GameStateManager _instance;
        private readonly Stack<GameState> _screens = new Stack<GameState>();
        private ContentManager _content;

        public static GameStateManager Instance => _instance ?? (_instance = new GameStateManager());

        public void SetContent(ContentManager content) {
            _content = content;
        }

        public void AddScreen(GameState screen) {
            try {
                _screens.Push(screen);
                _screens.Peek().Initialize();
                if (_content != null) {
                    _screens.Peek().LoadContent(_content);
                }
            } catch (Exception ex) { }
        }

        public void ChangeScreen(GameState screen) {
            try {
                ClearScreens();
                AddScreen(screen);
            } catch (Exception ex) { }
        }

        public void RemoveScreen() {
            if (_screens.Count > 0) {
                try {
                    GameState screen = _screens.Peek();

                    _screens.Pop();
                } catch (Exception ex) { }
            }
        }

        public void ClearScreens() {
            while (_screens.Count > 0) {
                GameState screen = _screens.Peek();

                _screens.Pop();
            }
        }

        public void UnloadContent() {
            foreach (GameState state in _screens) {
                state.UnloadContent();
            }
        }

        public void Update(GameTime gameTime) {
            try {
                if (_screens.Count > 0) {
                    _screens.Peek().Update(gameTime);
                }
            } catch (Exception ex) { }
        }

        public void Draw(SpriteBatch spriteBatch) {
            try {
                if (_screens.Count > 0) {
                    _screens.Peek().Draw(spriteBatch);
                }
            } catch (Exception ex) { }
        }
    }
}