using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public enum OptionType
{
    Up,
    Down,
    Left,
    Right,
    Undefined
}
public class Option : MonoBehaviour
{
    [SerializeField] private OptionType OptionType;
    [SerializeField] private Image ImageRenderer;

    private Tween _optionTween;

    public OptionType GetOptionType()
    {
        return OptionType;
    }
    public bool CompareOptionType(OptionType optionType)
    {
        return OptionType.Equals(optionType);
    }

    public void Move(float speed)
    {
        _optionTween = transform.DOMoveX(-500, speed);
    }
    public void MoveUpwards()
    {
        _optionTween.Kill();
        transform.DOMoveY(1000, .5f);
        DOVirtual.Float(1, 0, .5f, v =>
        {
            var color = ImageRenderer.color;
            color.a = v;
            ImageRenderer.color = color;
        });


    }
}
