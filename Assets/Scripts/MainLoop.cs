using System.Collections;
using UnityEngine;

public class MainLoop : MonoBehaviour
{
    [SerializeField] private float _timeFartShouldHappen;
    [SerializeField] private float _durationOfFartWindow;
    [SerializeField] private AudioClip _song;
    [SerializeField] private AudioClip _fart;
    [SerializeField] private AudioClip _caughtClip;

    [SerializeField] private Feedback _feedback;

    private float _songLength;
    private float _fartLength;
    private float _musicTimer;
    private float _earliestFartTime;
    private float _latestFartTime;
    private bool _songPlaying;
    private bool _canFart;
    private AudioSource _audioSource;
    private SongTiming _songTiming;
    private FartStatus _fartStatus;
    private GlassStatus _glassStatus;

    public float EarliestFartTime => _earliestFartTime;
    public float LatestFartTime => _latestFartTime;
    public float MusicTime => _musicTimer;
    public bool SongPlaying => _songPlaying;
    public GlassStatus GlassStatus => _glassStatus;

    private void Awake()
    {
        _songLength = _song.length;
        _fartLength = _fart.length;

        _earliestFartTime = _timeFartShouldHappen;
        _latestFartTime = _timeFartShouldHappen + _durationOfFartWindow;

        _songPlaying = false;
        _canFart = false;

        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _song;

        _songTiming = SongTiming.NotStarted;
        _fartStatus = FartStatus.NeedToFart;
        _glassStatus = GlassStatus.GlassNotPresent;
    }

    private void Update()
    {
        if (!_songPlaying & Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.Play();
            _songPlaying = true;
            _songTiming = SongTiming.BeforeFartWindow;
            StartCoroutine(FartDelay());
        }

        if (_songPlaying)
        {
            _musicTimer += Time.deltaTime;
        }

        if (_musicTimer > _songLength)
        {
            _songTiming = SongTiming.SongOver;
        }
        else if (_musicTimer > EarliestFartTime && _musicTimer < LatestFartTime)
        {
            _songTiming = SongTiming.FartWindow;
        }
        else if (_musicTimer > LatestFartTime)
        {
            _songTiming = SongTiming.AfterFartWindow;
        }

        if (_canFart && Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource.PlayClipAtPoint(_fart, transform.position, 1);
            _canFart = false;
            float fartTimestamp = _musicTimer;
            EvaluateFartTiming(fartTimestamp);
        }

        if (_songTiming == SongTiming.SongOver && _canFart)
        {
            AudioSource.PlayClipAtPoint(_fart, transform.position, 1);
            _canFart = false;
            float fartTimestamp = _musicTimer;
            EvaluateFartTiming(fartTimestamp);
        }

        // Glass status changes
        if (_musicTimer < EarliestFartTime - 5)
        {
            _glassStatus = GlassStatus.GlassNotPresent;
        }
        else if (_musicTimer < EarliestFartTime - 3)
        {
            _glassStatus = GlassStatus.GlassWhole;
        }
        else if (_musicTimer < EarliestFartTime - 1)
        {
            _glassStatus = GlassStatus.GlassCracked;
        }
        else if (_musicTimer > EarliestFartTime && _musicTimer < LatestFartTime)
        {
            _glassStatus = GlassStatus.GlassBroken;
        }
        else if (_musicTimer > LatestFartTime)
        {
            _glassStatus = GlassStatus.PostGlassBroken;
        }

        print($"{_songTiming}     {_glassStatus}");
    }

    private IEnumerator FartDelay()
    {
        yield return new WaitForSeconds(1);
        _canFart = true;
    }

    private void EvaluateFartTiming(float timestamp)
    {
        if (timestamp < _earliestFartTime)
        {
            _feedback.UpdateText("Too Early");
            _fartStatus = FartStatus.FartedAndCaught;
            StartCoroutine(PlayCaughtSoundAfterDelay());
        }
        else if (timestamp > _earliestFartTime && timestamp < _latestFartTime)
        {
            _feedback.UpdateText("Great!");
            _fartStatus = FartStatus.FartedAndNotCaught;
        }
        else
        {
            _feedback.UpdateText("Too Late");
            _fartStatus = FartStatus.FartedAndCaught;
            StartCoroutine(PlayCaughtSoundAfterDelay());
        }
    }

    private IEnumerator PlayCaughtSoundAfterDelay()
    {
        float delay = 1;

        yield return new WaitForSeconds(delay);
        AudioSource.PlayClipAtPoint(_caughtClip, transform.position, 1);
    }
}
