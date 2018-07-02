using System;
using Pong.Source.GameState;

namespace Pong {
    internal static class Program {
        [STAThread]
        private static void Main() {
            using (var game = new GameMain()) {
                game.Run();
            }
        }
    }
}