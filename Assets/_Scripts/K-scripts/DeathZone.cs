using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Game Over");
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
