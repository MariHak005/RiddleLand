using UnityEngine;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    public int hintCount = 0;
    public int keys = 5;

    public TextMeshProUGUI hintText;
    public TextMeshProUGUI keyText;

    private Vector3 startPosition;
    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();

        if (hintText != null)
            hintText.text = "Hints: " + hintCount;

        if (keyText != null)
            keyText.text = "Keys: " + keys;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Found: " + other.tag);

        if (other.CompareTag("Hint"))
        {
            hintCount++;
            if (hintText != null)
                hintText.text = "Hints: " + hintCount;

            Debug.Log("Hint: " + hintCount);
            Destroy(other.gameObject);
        }
        
        else if (other.CompareTag("Keys"))
        {
            keys++;

            if (keyText != null)
                keyText.text = "Keys: " + keys;

            Debug.Log("You got a new key: " + keys);
            Destroy(other.gameObject);
        }
    }
}