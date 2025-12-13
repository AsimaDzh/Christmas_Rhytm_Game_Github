using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer _spriteRen;

    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;


    void Start()
    {
       _spriteRen = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            _spriteRen.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            _spriteRen.sprite = defaultImage;
        }
    }
}
