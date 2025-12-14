using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool _startPlaying;
    [SerializeField] private AudioSource theXmasMusic;

    public BeatScroller theBS;
    public static GameManager instance;

    private int _currentScore;
    private int _scorePerNote = 100;
    private int _scorePerGoodNote = 125;
    private int _scorePerPerfectNote = 150;

    private int _currentMulti = 1;
    private int _multipierTracker;
    [SerializeField] private int[] multiThresholds;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;

    private float _totalNotes;
    private float _normalHits;
    private float _goodHits;
    private float _perfectHits;
    private float _missedHits;


    void Start()
    {
        instance = this;

        _totalNotes = FindObjectsOfType<NoteObject>().Length;
    }

    void Update()
    {
        if (!_startPlaying)
        {
            if (Input.anyKeyDown)
            {
                _startPlaying = true;
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

        multiplierText.text = "x" + _currentMulti;
    }

    public void NormalHit()
    {
        _currentScore += _scorePerNote * _currentMulti;
        scoreText.text = _currentScore.ToString();
        NoteHit();

        _normalHits++;
    }

    public void GoodHit() 
    {
        _currentScore += _scorePerGoodNote * _currentMulti;
        scoreText.text = _currentScore.ToString();
        NoteHit();

        _goodHits++;
    }

    public void PerfectHit()
    {
        _currentScore += _scorePerPerfectNote * _currentMulti;
        scoreText.text = _currentScore.ToString();
        NoteHit();

        _perfectHits++;
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");
        
        _currentMulti = 1;
        _multipierTracker = 0;
        multiplierText.text = "x" + _currentMulti;

        _missedHits++;
    }
}
