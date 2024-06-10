using System.IO;
using UnityEngine;

namespace Common
{
    public static class Consts
    {
        public static class Scenes
        {
            public const string MainMenuScene = "MainMenu";
            public const string GameScene = "GameScene";
            public const string LoadingScene = "LoadingScene";
            
        }
        
        public static class Audio
        {
            public const string AudioState = "AudioState"; 
            public const string Volume = "Volume"; 
        }
        
        public static class UserProgress
        {
            public static string ScoreValue = "ScoreValue";
            public static string MaxScoreValue = "MaxScoreValue";
        }
        
        public class SaveSystem
        {
            public static readonly string LevelProgress = Path.Combine(Application.persistentDataPath + "/LevelProgress.json");
        }
    }
}
