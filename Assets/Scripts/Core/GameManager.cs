using Core.LevelConstructing;
using EasyButtons;
using System;
using UnityEngine;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnGamePaused;
        public static event Action OnGameResumed;
        public static event Action OnGameRestarted;

        public static bool IsGamePaused { get; private set; }

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
        }
    }
}