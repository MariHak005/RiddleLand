using UnityEngine;
using System.Collections;
using TMPro;
using System.Collections.Generic;
using StarterAssets;

public class ChestTrigger : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public GameObject ChestUI;
    public GameObject wrongAnswer;
    public GameObject correctAnswer;
    public GameObject SpecialKeysPanel;

    public GameObject minimapUI;

    public GameObject keyInsideChest;

    public ThirdPersonController playerController;
    public Animator playerAnimator;
    public AudioSource footstepAudio;

    public AudioClip openSound;

    public TextMeshProUGUI RiddleText;
    public TextMeshProUGUI btnAText;
    public TextMeshProUGUI btnBText;
    public TextMeshProUGUI btnCText;
    public TextMeshProUGUI btnDText;
    public TextMeshProUGUI keyCountText;

    public static int specialKeyCount = 0;

    [Header("Portal Settings")]
    public int keysNeededToOpenPortal = 8;
    public GameObject portal;
    public GameObject portalOpenedPanel;
    public AudioClip portalOpeningSound;

    private static bool portalAlreadyOpened = false;

    private bool isOpening = false;
    private bool isCompleted = false;

    public List<Riddle> riddleList;
    private Riddle currentRiddle;

    private static ChestTrigger activeChest;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        if (ChestUI != null)
            ChestUI.SetActive(false);

        if (wrongAnswer != null)
            wrongAnswer.SetActive(false);

        if (correctAnswer != null)
            correctAnswer.SetActive(false);

        if (keyInsideChest != null)
            keyInsideChest.SetActive(false);

        if (keyCountText != null)
            keyCountText.text = "x " + specialKeyCount.ToString();

        if (portal != null && !portalAlreadyOpened)
            portal.SetActive(false);

        if (portalOpenedPanel != null && !portalAlreadyOpened)
            portalOpenedPanel.SetActive(false);

        if (minimapUI != null)
            minimapUI.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !isOpening && !isCompleted)
        {
            activeChest = this;

            if (playerController != null)
                playerController.enabled = false;

            if (playerAnimator != null)
            {
                playerAnimator.SetFloat("Speed", 0f);
                playerAnimator.SetFloat("MotionSpeed", 0f);
            }

            if (footstepAudio != null)
                footstepAudio.Stop();

            isOpening = true;

            if (animator != null)
                animator.SetBool("isOpen", true);

            if (audioSource != null && openSound != null)
                audioSource.PlayOneShot(openSound);

            if (keyInsideChest != null)
                keyInsideChest.SetActive(true);

            StartCoroutine(ShowUI());
        }
    }

    private IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(2f);

        if (ChestUI != null)
            ChestUI.SetActive(true);

        if (SpecialKeysPanel != null)
            SpecialKeysPanel.SetActive(false);

        if (wrongAnswer != null)
            wrongAnswer.SetActive(false);

        if (minimapUI != null)
            minimapUI.SetActive(false);

        DisplayRandomRiddle();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void DisplayRandomRiddle()
    {
        if (riddleList == null || riddleList.Count == 0)
        {
            Debug.LogWarning(gameObject.name + " has no riddles in Riddle List.");
            currentRiddle = null;
            return;
        }

        int randomIndex = Random.Range(0, riddleList.Count);
        currentRiddle = riddleList[randomIndex];

        if (RiddleText != null)
            RiddleText.text = currentRiddle.question;

        if (btnAText != null)
            btnAText.text = currentRiddle.optionA;

        if (btnBText != null)
            btnBText.text = currentRiddle.optionB;

        if (btnCText != null)
            btnCText.text = currentRiddle.optionC;

        if (btnDText != null)
            btnDText.text = currentRiddle.optionD;
    }

    public void AnswerA()
    {
        if (activeChest != null)
            activeChest.CheckAnswer(0);
        else
            CheckAnswer(0);
    }

    public void AnswerB()
    {
        if (activeChest != null)
            activeChest.CheckAnswer(1);
        else
            CheckAnswer(1);
    }

    public void AnswerC()
    {
        if (activeChest != null)
            activeChest.CheckAnswer(2);
        else
            CheckAnswer(2);
    }

    public void AnswerD()
    {
        if (activeChest != null)
            activeChest.CheckAnswer(3);
        else
            CheckAnswer(3);
    }

    void CheckAnswer(int answerIndex)
    {
        if (currentRiddle == null)
        {
            Debug.LogWarning(gameObject.name + ": No riddle selected. Check Riddle List.");
            return;
        }

        if (currentRiddle.correctAnswerIndex == answerIndex)
            Win();
        else
            Wrong();
    }

    void Win()
    {
        if (correctAnswer != null)
            correctAnswer.SetActive(true);

        specialKeyCount++;

        if (keyCountText != null)
            keyCountText.text = "x " + specialKeyCount.ToString();

        if (ChestUI != null)
            ChestUI.SetActive(false);

        if (SpecialKeysPanel != null)
            SpecialKeysPanel.SetActive(true);

        if (specialKeyCount >= keysNeededToOpenPortal)
        {
            OpenPortal();
        }

        if (minimapUI != null)
            minimapUI.SetActive(true);

        if (playerController != null)
            playerController.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        isCompleted = true;
        activeChest = null;

        StartCoroutine(HideWinPanel());

        enabled = false;
    }

    void OpenPortal()
    {
        if (portalAlreadyOpened)
            return;

        portalAlreadyOpened = true;

        if (portal != null)
            portal.SetActive(true);

        if (portalOpenedPanel != null)
            portalOpenedPanel.SetActive(true);

        if (audioSource != null && portalOpeningSound != null)
            audioSource.PlayOneShot(portalOpeningSound);
    }

    private IEnumerator HideWinPanel()
    {
        yield return new WaitForSeconds(3f);

        if (correctAnswer != null)
            correctAnswer.SetActive(false);
    }

    void Wrong()
    {
        if (wrongAnswer != null)
            wrongAnswer.SetActive(true);

        if (ChestUI != null)
            ChestUI.SetActive(false);

        if (minimapUI != null)
            minimapUI.SetActive(false);
    }

    public void TryAgain()
    {
        if (wrongAnswer != null)
            wrongAnswer.SetActive(false);

        if (ChestUI != null)
            ChestUI.SetActive(true);

        if (minimapUI != null)
            minimapUI.SetActive(false);
    }
}

[System.Serializable]
public class Riddle
{
    public string question;
    public string optionA;
    public string optionB;
    public string optionC;
    public string optionD;
    public int correctAnswerIndex;
}