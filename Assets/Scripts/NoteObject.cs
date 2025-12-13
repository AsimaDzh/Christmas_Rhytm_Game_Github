using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;


    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                GameManager.instance.NoteHit();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
            canBePressed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;
            GameManager.instance.NoteMissed();
        }
    }
}
