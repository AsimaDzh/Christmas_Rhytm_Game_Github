using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int _currentScore;
    private int _scorePerNote = 100;
    private int _scorePerGoodNote = 125;
    private int _scorePerPerfectNote = 150;

    private int _currentMulti = 1;
    private int _multipierTracker;
    [SerializeField] private int[] multiThresholds;

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
        if (_currentMulti - 1 < multiThresholds.Length)
        {
            _multipierTracker++;

            if (multiThresholds[_currentMulti - 1] <= _multipierTracker)
            {
                _multipierTracker = 0;
                _currentMulti++;
            }
        }
        
        //_currentScore += _scorePerNote * _currentMulti;
        //scoreText.text = _currentScore.ToString();

        multiplierText.text = "x" + _currentMulti;
    }

    public void NormalHit()
    {
        _currentScore += _scorePerNote * _currentMulti;
        NoteHit();
    }

    public void GoodHit() 
    {
        _currentScore += _scorePerGoodNote * _currentMulti;
        NoteHit();
    }

    public void PerfectHit()
    {
        _currentScore += _scorePerPerfectNote * _currentMulti;
        NoteHit();
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        
        _currentMulti = 1;
        _multipierTracker = 0;
        multiplierText.text = "x" + _currentMulti;
    }
}
