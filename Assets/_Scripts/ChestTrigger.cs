using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class ChestTrigger : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public GameObject ChestUI;
    public GameObject wrongAnswer;
    public GameObject correctAnswer;
    public GameObject SpecialKeysPanel;
    public GameObject KeyCounterBar;

    public GameObject keyInsideChest;

    public GameObject portal;
    public int keysNeededToWin = 4;

    public AudioClip openSound;

    public TextMeshProUGUI RiddleText;
    public TextMeshProUGUI btnAText;
    public TextMeshProUGUI btnBText;
    public TextMeshProUGUI btnCText;
    public TextMeshProUGUI btnDText;
    public TextMeshProUGUI keyCountText;

    public static int specialKeyCount = 0;
    private bool isOpening = false;

    public List<Riddle> riddleList;
    private Riddle currentRiddle;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        ChestUI.SetActive(false);
        wrongAnswer.SetActive(false);

        if (correctAnswer != null)
            correctAnswer.SetActive(false);

        if (keyInsideChest != null)
            keyInsideChest.SetActive(false);

        if (portal != null)
            portal.SetActive(false);

        if (keyCountText != null)
            keyCountText.text = "x " + specialKeyCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isOpening)
        {
            isOpening = true;

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

        if (KeyCounterBar != null)
            KeyCounterBar.SetActive(false);

        DisplayRandomRiddle();

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void DisplayRandomRiddle()
    {
        if (riddleList.Count > 0)
        {
            int randomIndex = Random.Range(0, riddleList.Count);
            currentRiddle = riddleList[randomIndex];

            RiddleText.text = currentRiddle.question;
            btnAText.text = currentRiddle.optionA;
            btnBText.text = currentRiddle.optionB;
            btnCText.text = currentRiddle.optionC;
            btnDText.text = currentRiddle.optionD;
        }
    }

    public void AnswerA()
    {
        if (currentRiddle.correctAnswerIndex == 0) Win();
        else Wrong();
    }

    public void AnswerB()
    {
        if (currentRiddle.correctAnswerIndex == 1) Win();
        else Wrong();
    }

    public void AnswerC()
    {
        if (currentRiddle.correctAnswerIndex == 2) Win();
        else Wrong();
    }

    public void AnswerD()
    {
        if (currentRiddle.correctAnswerIndex == 3) Win();
        else Wrong();
    }

    void Win()
    {
        if (correctAnswer != null)
            correctAnswer.SetActive(true);

        specialKeyCount++;

        if (keyCountText != null)
            keyCountText.text = "x " + specialKeyCount.ToString();

        if (specialKeyCount >= keysNeededToWin)
        {
            if (portal != null)
                portal.SetActive(true);
        }

        ChestUI.SetActive(false);

        if (SpecialKeysPanel != null)
            SpecialKeysPanel.SetActive(true);

        if (KeyCounterBar != null)
            KeyCounterBar.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        StartCoroutine(HideWinPanel());
        enabled = false;
    }

    private IEnumerator HideWinPanel()
    {
        yield return new WaitForSeconds(3f);

        if (correctAnswer != null)
            correctAnswer.SetActive(false);
    }

    void Wrong()
    {
        wrongAnswer.SetActive(true);
    }

    public void TryAgain()
    {
        wrongAnswer.SetActive(false);
        ChestUI.SetActive(true);
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