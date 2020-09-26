using ScriptsForGameObjects.Car;
using UnityEngine;

public class PlayerCarCollision : MonoBehaviour
{
    [SerializeField]
    private PlayerCarMove playerCarMoveScript;
    [SerializeField]
    private float deceleration = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            playerCarMoveScript.ForwardSpeed -= deceleration;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Police"))
        {
            // GAME OVER
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }
}
