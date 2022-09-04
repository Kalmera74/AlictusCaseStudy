using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MessageManager : MonoBehaviour
{
    [SerializeField] private Transform[] Messages;
    [SerializeField] private float PlayTime;

    private float _timeToPlayPerMessageAnimation;

    private void Awake()
    {
        if (Messages.Length > 0)
        {
            _timeToPlayPerMessageAnimation = PlayTime / Messages.Length;
        }
    }

    public float PlayAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        for (int i = 0; i < Messages.Length; i++)
        {
            var message = Messages[i];
            Tween t = message.DOScale(1, _timeToPlayPerMessageAnimation).SetEase(Ease.OutBounce);
            sequence.Append(t);
        }
        sequence.Play();
        return PlayTime;
    }

    internal void Hide()
    {
        gameObject.SetActive(false);
    }
}
