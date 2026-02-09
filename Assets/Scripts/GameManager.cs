using TMPro;
using UnityEngine;

public enum Rank 
{ 
    F = 0, 
    D = 1, 
    C = 2, 
    B = 3, 
    A = 4, 
    S = 5, 
    SS = 6 
}


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("========== Music ==========")]
    [SerializeField] private AudioSource theXmasMusic;
    [SerializeField] private AudioSource resultSound;
    private bool _startPlaying;

    [Header("========== Beat Scroller ==========")]
    public BeatScroller theBS;

    private int _currentScore;
    private int _scorePerNote = 100;
    private int _scorePerGoodNote = 125;
    private int _scorePerPerfectNote = 150;

    private int _currentMulti = 1;
    private int _multipierTracker;

    [Header("========== Multipliers & Scores ==========")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplierText;
    [SerializeField] private int[] multiThresholds;

    private float 
        _totalNotes, 
        _normalHits, 
        _goodHits, 
        _perfectHits, 
        _missedHits;

    [Header("========== Results Screen ==========")]
    public GameObject resultScreen;
    public TextMeshProUGUI 
        missedHitText, 
        normalHitText, 
        goodHitText, 
        perfectHitText, 
        percentText, 
        rankText, 
        finalScoreText; 

    [Header("========== Background (optional) ==========")]
    [SerializeField] private SpriteRenderer backgroundRenderer;
    [SerializeField] private Sprite[] rankBackgrounds; //Order of sprites: F, D, C, B, A, S, SS
    [SerializeField] private bool updateRankTextDuringPlay = true;

    private Rank _currentRank = Rank.F;


    void Start()
    {
        instance = this;

        _totalNotes = FindObjectsOfType<NoteObject>().Length;
        // First background update to set the initial rank background (F)
        UpdateBackgroundByRank(_currentRank);
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
            // While the music is playing, update the rank based on the current hit percentage
            if (theXmasMusic.isPlaying && _totalNotes > 0f)
            {
                float percentHit = (_normalHits * 0.5f + _goodHits * 0.8f + _perfectHits * 1f) / _totalNotes * 100f;
                Rank newRank = GetRankFromPercent(percentHit);

                if (newRank != _currentRank)
                {
                    _currentRank = newRank;
                    UpdateBackgroundByRank(_currentRank);
                }

                if (updateRankTextDuringPlay && rankText != null)
                    rankText.text = RankToString(_currentRank);
            }

            // When the music stops, show the results screen with final stats
            if (!theXmasMusic.isPlaying && !resultScreen.activeInHierarchy)
            {
                resultScreen.SetActive(true);
                resultSound.Play();

                missedHitText.text = _missedHits.ToString();
                normalHitText.text = _normalHits.ToString();
                goodHitText.text = _goodHits.ToString();
                perfectHitText.text = _perfectHits.ToString();

                float percentHit = (_normalHits * 0.5f + _goodHits * 0.8f + _perfectHits * 1f) / _totalNotes * 100f;
                percentText.text = percentHit.ToString("F1") + "%";

                string rankVal = "F";
                switch (percentHit)
                {
                    case 100f:
                        rankVal = "SS";
                        break;

                    case > 95f:
                        rankVal = "S";
                        break;

                    case > 85f:
                        rankVal = "A";
                        break;

                    case > 70f:
                        rankVal = "B";
                        break;

                    case > 55f:
                        rankVal = "C";
                        break;

                    case > 40f:
                        rankVal = "D";
                        break;
                }
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
        _currentMulti = 1;
        _multipierTracker = 0;
        multiplierText.text = "x" + _currentMulti;

        _missedHits++;
    }


    private Rank GetRankFromPercent(float percent)
    {
        if (percent == 100f) return Rank.SS;
        if (percent > 95f) return Rank.S;
        if (percent > 85f) return Rank.A;
        if (percent > 70f) return Rank.B;
        if (percent > 55f) return Rank.C;
        if (percent > 40f) return Rank.D;
        return Rank.F;
    }


    private string RankToString(Rank r)
    {
        switch (r)
        {
            case Rank.F: return "F";
            case Rank.D: return "D";
            case Rank.C: return "C";
            case Rank.B: return "B";
            case Rank.A: return "A";
            case Rank.S: return "S";
            case Rank.SS: return "SS";
            default: return "F";
        }
    }


    private void UpdateBackgroundByRank(Rank rank)
    {
        int idx = (int)rank;
        if (rankBackgrounds != null && idx >= 0 && idx < rankBackgrounds.Length && rankBackgrounds[idx] != null)
        {
            if (backgroundRenderer != null)
                backgroundRenderer.sprite = rankBackgrounds[idx];
            return;
        }
    }
}
