using UnityEngine;
using StarterAssets;

public class StartGoalPanel : MonoBehaviour
{
    public GameObject goalPanel;
    public ThirdPersonController playerController;
    public Animator playerAnimator;
    public AudioSource footstepAudio;

    void Start()
    {
        goalPanel.SetActive(true);

        if (playerController != null)
            playerController.enabled = false;

        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Speed", 0f);
            playerAnimator.SetFloat("MotionSpeed", 0f);
        }

        if (footstepAudio != null)
            footstepAudio.Stop();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseGoalPanel()
    {
        goalPanel.SetActive(false);

        if (playerController != null)
            playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}