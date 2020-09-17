using UnityEngine;
using UnityEngine.UI;

namespace GUI.Buttons
{
    public class ButtonStateHandler : MonoBehaviour
    {
        public Sprite FirstStateSprite;

        public Sprite SecondStateSprite;

        private Button button;

        private void Awake()
        {
            button = GetComponentInParent<Button>();
            if (button is null)
            {
                throw new MissingComponentException("Button state handler have to be attached to a button.");
            }
            button.onClick.AddListener(Click);
        }

        private void Click()
        {
            if (button.image.sprite == FirstStateSprite)
            {
                button.image.sprite = SecondStateSprite;
            }
            else
            {
                button.image.sprite = FirstStateSprite;
            }
        }
    }
}