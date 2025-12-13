using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource theXmasMusic;
    public BeatScroller theBS;
    public bool startPlaying;

    public static GameManager instance;


    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;

                theXmasMusic.Play();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit On Time");
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
    }
}
