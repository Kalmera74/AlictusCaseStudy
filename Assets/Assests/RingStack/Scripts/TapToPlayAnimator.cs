using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TapToPlayAnimator : MonoBehaviour
{

    void Start()
    {
        transform.DOScale(Vector3.one * .75f, .5f).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }

    internal void Hide()
    {
        gameObject.SetActive(false);
    }
}
