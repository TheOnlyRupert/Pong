using System;
using Pong.Source.GameState;

namespace Pong {
    internal static class Program {
        [STAThread]
        private static void Main() {
            using (GameMain game = new GameMain()) {
                game.Run();
            }
        }
    }
}