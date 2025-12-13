using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioSource theXmasMusic;
    public BeatScroller theBS;
    public bool startPlaying;


    void Start()
    {
        
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
}
