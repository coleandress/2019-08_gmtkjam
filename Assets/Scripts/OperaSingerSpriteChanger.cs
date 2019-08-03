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
    [SerializeField] private Sprite glassCrackedSprite;
    [SerializeField] private Sprite glassBrokenSprite;
    [SerializeField] private Sprite postGlassBrokenSprite;
    [SerializeField] private MainLoop _mainLoop;

    private SpriteRenderer _spriteRenderer;

    private float cycleTimer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = singingSprites[0];

        cycleTimer = spriteCycleTime;
    }

    private void Update()
    {
        switch (_mainLoop.GlassStatus)
        {
            case GlassStatus.GlassNotPresent:
                _spriteRenderer.sprite = singingSprites[0];
                break;

            case GlassStatus.GlassWhole:
                _spriteRenderer.sprite = glassWholeSprite;
                break;

            case GlassStatus.GlassCracked:
                _spriteRenderer.sprite = glassCrackedSprite;
                break;

            case GlassStatus.GlassBroken:
                _spriteRenderer.sprite = glassBrokenSprite;
                break;

            case GlassStatus.PostGlassBroken:
                _spriteRenderer.sprite = postGlassBrokenSprite;
                break;
        }
    }
}