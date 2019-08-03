using TMPro;
using UnityEngine;

public class IndicatorText : MonoBehaviour
{
    private TextMeshProUGUI _text;
    [SerializeField] MainLoop _mainLoop;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
        _text.text = "Press Spacebar";
    }

    private void Update()
    {
        if (_mainLoop.SongPlaying)
        {
            if (_mainLoop.MusicTime < _mainLoop.EarliestFartTime)
                _text.text = "Not Yet";
            else if (_mainLoop.MusicTime > _mainLoop.EarliestFartTime && _mainLoop.MusicTime < _mainLoop.LatestFartTime)
                _text.text = "NOW!";
            else
                _text.text = "Too Late";
        }
    }
}
