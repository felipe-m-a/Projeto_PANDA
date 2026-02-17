namespace Project.Scripts.SceneManagementSystem
{
    public class GameScene
    {
        public enum SceneType
        {
            None = 0,
            Menu,
            Minigame,
            Adventure
        }

        public static readonly GameScene Initialization = new("Initialization", SceneType.None);
        public static readonly GameScene MainMenu = new("MainMenu", SceneType.Menu);

        public static readonly GameScene MinigameMemory = new("Minigame/Memory", SceneType.Minigame);
        public static readonly GameScene MinigameFlow = new("Minigame/Flow", SceneType.Minigame);
        public static readonly GameScene MinigamePuzzle = new("Minigame/Puzzle", SceneType.Minigame);
        public static readonly GameScene MinigamePipes = new("Minigame/Pipes", SceneType.Minigame);
        public static readonly GameScene MinigameSpaceship = new("Minigame/Spaceship", SceneType.Minigame);
        public static readonly GameScene MinigameWhack = new("Minigame/Whack", SceneType.Minigame);

        public static readonly GameScene AdventureLevel1 = new("Adventure/Level1", SceneType.Adventure);

        public readonly string Path;
        public readonly SceneType Type;

        private GameScene(string path, SceneType type)
        {
            Path = "Assets/Project/Scenes/" + path + ".unity";
            Type = type;
        }
    }
}