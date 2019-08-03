using System.Collections;
using System.Reflection.Emit;
using UnityEngine;

public class MainLoop : MonoBehaviour
{
    [SerializeField] private float _musicTimer;
    [SerializeField] private float _fartLength;
    [SerializeField] private float _timeFartShouldHappen;
    [SerializeField] private float _durationOfFartWindow;
    [SerializeField] private AudioClip _song;
    [SerializeField] private AudioClip _fart;

    private float _earliestFartTime;
    private float _latestFartTime;
    private bool _songPlaying;
    private bool _canFart;
    private AudioSource _audioSource;

    private void Awake()
    {
        _musicTimer = _song.length;
        _fartLength = _fart.length;

        _earliestFartTime = _song.length - _timeFartShouldHappen;
        _latestFartTime = _earliestFartTime - 2;

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
            _musicTimer -= Time.deltaTime;
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
        if (timestamp > _earliestFartTime)
            print($"too early {timestamp}");
        else if (timestamp < _earliestFartTime && timestamp > _latestFartTime)
            print($"great! {timestamp}");
        else
            print($"too late {timestamp}");
    }
}
