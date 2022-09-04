using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
namespace BathroomSelfie.Scripts
{
    public class OptionController : MonoBehaviour
    {
        [SerializeField] private Image SelectionBox;
        [SerializeField] private GameObject OptionSliderParent;
        [SerializeField] private Option[] Options;
        [SerializeField] private float OptionSpeed;
        [SerializeField] private float DelayBetweenOptions;


        private Option _currentOption;
        private Color _selectionBoxColor;

        private void Awake()
        {
            _selectionBoxColor = SelectionBox.color;
        }
        public void StartPlay()
        {
            OptionSliderParent.SetActive(true);
            StartCoroutine(SlideOptions());
        }

        private IEnumerator SlideOptions()
        {
            foreach (var option in Options)
            {
                option.Move(OptionSpeed);
                _currentOption = option;
                yield return new WaitForSeconds(DelayBetweenOptions);
            }
        }

        public bool CompareMoveToOptionType(OptionType moveType)
        {
            return _currentOption.CompareOptionType(moveType);
        }

        internal void Success()
        {
            SelectionBox.color = Color.green;
            DOVirtual.DelayedCall(.5f, () =>
            {
                SelectionBox.color = _selectionBoxColor;
            });
            _currentOption.MoveUpwards();
        }

        internal void Fail()
        {

            SelectionBox.color = Color.red; ;

            SelectionBox.rectTransform.DOShakePosition(.5f).onComplete += () =>
            {
                SelectionBox.color = _selectionBoxColor;
            };
        }

        internal bool GetIsOptionInTheSelectionBox()
        {
            return (Mathf.Abs(_currentOption.transform.position.x) - Mathf.Abs(SelectionBox.transform.position.x)) <= 2f;
        }
    }
}
