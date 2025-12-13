using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _currentScore;
    private int _scorPerNote = 100;
    private int _currentMulti;
    private int _multipierTracker;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;

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

        _currentScore += _scorPerNote;
        scoreText.text = _currentScore.ToString();
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
    }
}
