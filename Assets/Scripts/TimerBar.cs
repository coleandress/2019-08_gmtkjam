using UnityEngine;

public class TimerBar : MonoBehaviour
{
    [SerializeField] private RectTransform _topImage;
    [SerializeField] private RectTransform _mask;
    [SerializeField] private MainLoop _mainLoop;

    private Vector3 _imageHoldPosition;
    private float _barWidth;

    private void Awake()
    {
        _barWidth = _topImage.rect.width;
        _imageHoldPosition = _mask.transform.localPosition;
    }

    private void Update()
    {
        float timeLeftPercentage = _mainLoop.MusicTime / _mainLoop.SongLength;

        float xPosition = _barWidth * -timeLeftPercentage;

        //_mask.transform.localPosition += Vector3.left * Time.deltaTime;
        _mask.transform.localPosition = new Vector3(xPosition, 0, 0);
        _topImage.transform.localPosition = new Vector3(-xPosition, 0, 0);
        //print($"{timeLeftPercentage}   {xPosition}");
    }
}
