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

    private float 
        _totalNotes, 
        _normalHits, 
        _goodHits, 
        _perfectHits, 
        _missedHits;

    public GameObject resultScreen;
    public TextMeshProUGUI 
        missedHitText, 
        normalHitText, 
        goodHitText, 
        perfectHitText, 
        percentText, 
        rankText, 
        finalScoreText; 


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
        else
        {
            if (!theXmasMusic.isPlaying && !resultScreen.activeInHierarchy)
            {
                resultScreen.SetActive(true);

                missedHitText.text = _missedHits.ToString();
                normalHitText.text = _normalHits.ToString();
                goodHitText.text = _goodHits.ToString();
                perfectHitText.text = _perfectHits.ToString();

                float percentHit = (_normalHits + _goodHits + _perfectHits) / _totalNotes * 100f;
                percentText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";
                if (percentHit > 40f)
                    rankVal = "D";
                if (percentHit > 55f)
                    rankVal = "C";
                if (percentHit > 70f)
                    rankVal = "B";
                if (percentHit > 85f)
                    rankVal = "A";
                if (percentHit > 95f)
                    rankVal = "S";
                rankText.text = rankVal;

                finalScoreText.text = _currentScore.ToString();
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
