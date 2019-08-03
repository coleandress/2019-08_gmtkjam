using System.Collections;
using UnityEngine;

public class MainLoop : MonoBehaviour
{
    [SerializeField] private float _songLength;
    [SerializeField] private float _fartLength;
    [SerializeField] private float _timeFartShouldHappen;
    [SerializeField] private float _durationOfFartWindow;
    [SerializeField] private AudioClip _song;
    [SerializeField] private AudioClip _fart;

    [SerializeField] private Feedback _feedback;

    private float _musicTimer;
    private float _earliestFartTime;
    private float _latestFartTime;
    private bool _songPlaying;
    private bool _canFart;
    private AudioSource _audioSource;

    public float EarliestFartTime => _earliestFartTime;
    public float LatestFartTime => _latestFartTime;
    public float MusicTime => _musicTimer;
    public bool SongPlaying => _songPlaying;

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
    }

    private void Update()
    {
        if (!_songPlaying & Input.GetKeyDown(KeyCode.Space))
        {
            _audioSource.Play();
            _songPlaying = true;
            StartCoroutine(FartDelay());
        }

        if (_songPlaying)
        {
            _musicTimer += Time.deltaTime;
        }

        if (_canFart && Input.GetKeyDown(KeyCode.Space))
        {
            AudioSource.PlayClipAtPoint(_fart, transform.position, 1);
            _canFart = false;
            float fartTimestamp = _musicTimer;
            EvaluateFartTiming(fartTimestamp);
        }

        print($"{_earliestFartTime} {_latestFartTime}");
    }

    private IEnumerator FartDelay()
    {
        yield return new WaitForSeconds(1);
        _canFart = true;
    }

    private void EvaluateFartTiming(float timestamp)
    {
        if (timestamp < _earliestFartTime)
            _feedback.UpdateText("Too Early");
        else if (timestamp > _earliestFartTime && timestamp < _latestFartTime)
            _feedback.UpdateText("Great!");
        else
            _feedback.UpdateText("Too Late");
    }
}
