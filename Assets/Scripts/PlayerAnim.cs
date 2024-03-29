﻿using System.Collections;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Sprite _needToFart1;
    [SerializeField] private Sprite _needToFart2;
    [SerializeField] private Sprite _needToFart3;

    [SerializeField] private Sprite _farting;

    [SerializeField] private Sprite _loseSprite;

    [SerializeField] private Sprite _winSprite;

    [SerializeField] private MainLoop _mainLoop;

    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private float _timeLeftPercentage;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        _timeLeftPercentage = _mainLoop.MusicTime / _mainLoop.SongLength;

        if (!_mainLoop.IsGameOver && _timeLeftPercentage < .40)
        {
            _spriteRenderer.sprite = _needToFart1;
        }
        else if (!_mainLoop.IsGameOver && _timeLeftPercentage < .75)
        {
            _spriteRenderer.sprite = _needToFart2;
            _animator.Play("playerslowshake");
        }
        else if (!_mainLoop.IsGameOver)
        {
            _spriteRenderer.sprite = _needToFart3;
            _animator.Play("playerfastshake");
        }
    }

    public void FartAnim()
    {
        StartCoroutine(PlayFartAnim());
    }

    private IEnumerator PlayFartAnim()
    {
        _animator.Play("playerneutral");
        _spriteRenderer.sprite = _farting;
        yield return new WaitForSeconds(2);

        if (_mainLoop.IsAWin)
        {
            _spriteRenderer.sprite = _winSprite;
        }
        else
        {
            _spriteRenderer.sprite = _loseSprite;
        }
    }
}
