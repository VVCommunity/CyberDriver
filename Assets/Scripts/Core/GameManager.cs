using Core.LevelConstructing;
using System;
using Tools.EasyButtons;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnGamePaused;
        public static event Action OnGameResumed;
        public static event Action OnGameRestarted;

        public static bool IsGamePaused { get; private set; }
        public static float DistanceBetweenWalls { get; } = 20;
        public static float MinHeight { get; } = 10;
        public static float MaxHeight { get; } = 30;

        private static float timeScaleBeforePause;

        public static void PauseGame()
        {
            if (!IsGamePaused)
            {
                timeScaleBeforePause = Time.timeScale;
                Time.timeScale = 0;
                AudioListener.pause = true;

                IsGamePaused = true;
                OnGamePaused?.Invoke();
            }
        }

        public static void ResumeGame()
        {
            if (IsGamePaused)
            {
                Time.timeScale = timeScaleBeforePause;
                AudioListener.pause = false;

                IsGamePaused = false;
                OnGameResumed?.Invoke();
            }
        }

        [Button]
        public static void Restart()
        {
            LevelConstructManager.ClearLevel();
            LevelConstructManager.BuildLevel();
            OnGameRestarted?.Invoke();
        }
    }
}