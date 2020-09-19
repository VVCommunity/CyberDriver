using UnityEngine;

namespace Core
{
    public class GuiManager : MonoBehaviour
    {
        public static void PauseClick()
        {
            if (GameManager.IsGamePaused)
            {
                GameManager.ResumeGame();
            }
            else
            {
                GameManager.PauseGame();
            }
        }
    }
}