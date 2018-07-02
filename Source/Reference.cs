namespace Pong.Source {
    public static class Reference {
        public static int globalTimeFactor = 1;

        public static int BallStartingSpeed = 8;
        public static int BallMaxSpeed = 20;
        
        public static int PaddleMaxAISpeed = 8;
        public static int PaddleSpeedNorm = 16;
        public static int PaddleSpeedFast = 32;

        public static bool enableDrawBoundingBox;

        public static string PlayerLWins = "<--  Left Side Wins!";
        public static string PlayerRWins = "Right Side Wins!  -->";
        public static string NewGame = "Press SPACE to start a new game";
    }
}