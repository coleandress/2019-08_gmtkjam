using UnityEngine;

public enum SongTiming
{
    NotStarted,
    BeforeFartWindow,
    FartWindow,
    AfterFartWindow,
    SongOver
}

public enum GlassStatus
{
    GlassNotPresent,
    GlassWhole,
    GlassCracked,
    GlassBroken,
    PostGlassBroken
}

public class OperaSingerSpriteChanger : MonoBehaviour
{
    [SerializeField] private float spriteCycleTime;
    [SerializeField] private Sprite[] singingSprites;
    [SerializeField] private Sprite glassWholeSprite;
    [SerializeField] private Sprite[] glassCrackedSprite;
    [SerializeField] private Sprite glassBrokenSprite;
    [SerializeField] private Sprite postGlassBrokenSprite;
    [SerializeField] private MainLoop _mainLoop;

    private SpriteRenderer _spriteRenderer;

    private float cycleTimer;
    private int _randomSpriteIndex;
    private bool _timeToSwapSprite;
    private bool _crackedGlassSpriteActive;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = singingSprites[0];
        _randomSpriteIndex = 0;
        _crackedGlassSpriteActive = false;

        cycleTimer = spriteCycleTime;
    }

    private void Update()
    {
        if (_mainLoop.SongPlaying && cycleTimer > 0)
        {
            cycleTimer -= Time.deltaTime;
        }
        else if (_mainLoop.SongPlaying)
        {
            _timeToSwapSprite = true;
            cycleTimer = spriteCycleTime;
        }

        switch (_mainLoop.GlassStatus)
        {
            case GlassStatus.GlassNotPresent:
                if (_timeToSwapSprite)
                {
                    _randomSpriteIndex = Random.Range(0, singingSprites.Length);
                    _spriteRenderer.sprite = singingSprites[_randomSpriteIndex];
                    _timeToSwapSprite = false;
                }
                break;

            case GlassStatus.GlassWhole:
                _spriteRenderer.sprite = glassWholeSprite;
                break;

            case GlassStatus.GlassCracked:
                if (!_crackedGlassSpriteActive)
                {
                    _crackedGlassSpriteActive = true;
                    _randomSpriteIndex = Random.Range(0, glassCrackedSprite.Length);
                    _spriteRenderer.sprite = glassCrackedSprite[_randomSpriteIndex];
                }
                break;

            case GlassStatus.GlassBroken:
                _spriteRenderer.sprite = glassBrokenSprite;
                break;

            case GlassStatus.PostGlassBroken:
                _spriteRenderer.sprite = postGlassBrokenSprite;
                break;
        }

        //print(Random.Range(0, glassCrackedSprite.Length));
    }
}