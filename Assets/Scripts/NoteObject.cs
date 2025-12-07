using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
            canBePressed = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
            canBePressed = false;
    }
}
