using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChestTrigger : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;

    public GameObject ChestUI;
    public GameObject wrongAnswer;
    public AudioClip openSound;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        ChestUI.SetActive(false);
        wrongAnswer.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animator.SetBool("isOpen", true);
            audioSource.PlayOneShot(openSound);
            StartCoroutine(ShowUI());
        }
    }

    private IEnumerator ShowUI()
    {
        yield return new WaitForSeconds(2f);
        ChestUI.SetActive(true);
    }

    public void AnswerA()
    {
        Wrong();
    }

    public void AnswerB()
    {
        Wrong();
    }

    public void AnswerC()
    {
        SceneManager.LoadScene("Level2");
    }

    void Wrong()
    {
        wrongAnswer.SetActive(true);
    }
}