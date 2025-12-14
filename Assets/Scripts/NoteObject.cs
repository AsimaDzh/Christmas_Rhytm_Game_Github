using UnityEngine;

public class NoteObject : MonoBehaviour
{
    private bool canBePressed;

    public KeyCode keyToPress;


    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);

                if (transform.position.y > -3.3)
                {
                    Debug.Log("Normal Hit");
                    GameManager.instance.NormalHit();
                }
                else if (transform.position.y > -3.7)
                {
                    Debug.Log("Good Hit");
                    GameManager.instance.GoodHit();
                }
                else
                {
                    Debug.Log("Perfect Hit");
                    GameManager.instance.PerfectHit();
                } 
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
