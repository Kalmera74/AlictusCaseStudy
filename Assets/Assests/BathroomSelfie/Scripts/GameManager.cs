using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using RingStack.Scripts;
namespace BathroomSelfie.Scripts
{
    public enum GameState
    {
        Init,
        Idle,
        Messaging,
        Selfie,
        End,
        Input
    }
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameState GameState;
        [SerializeField] private TapToPlayAnimator TapToPlayAnimator;
        [SerializeField] private MessageManager MessageManager;
        [SerializeField] private WomanAnimationController WomanAnimationController;
        [SerializeField] private OpitonController OptionController;
        [SerializeField] private PhotoController PhotoController;
        private Vector2 _startPos = Vector2.zero;
        private Vector2 _endPos = Vector2.zero;

        private void Update()
        {
            StateMachine();
        }
        private void StateMachine()
        {
            switch (GameState)
            {
                case GameState.Init:
                    InitState();
                    break;
                case GameState.Idle:
                    IdleState();
                    break;
                case GameState.Messaging:
                    MessagingState();
                    break;
                case GameState.Selfie:
                    SelfieState();
                    break;
                case GameState.End:
                    EndState();
                    break;
                case GameState.Input:
                    InputState();
                    break;
            }
        }

        private void InputState()
        {


            if (Input.GetMouseButtonDown(0))
            {
                _startPos = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                _endPos = Input.mousePosition;

                OptionType moveType = GetMoveType(_startPos, _endPos);

                if (OptionController.CompareMoveToOptionType(moveType) && CheckIsOptionInTheSelectionBox())
                {
                    Success();
                    WomanAnimationController.Play(moveType);
                    PhotoController.TakeAPhoto(moveType);
                }
                else
                {
                    Fail();
                }
            }
        }

        private bool CheckIsOptionInTheSelectionBox()
        {
            return OptionController.GetIsOptionInTheSelectionBox();
        }

        private void Fail()
        {
            OptionController.Fail();
        }

        private void Success()
        {
            OptionController.Success();
        }

        private OptionType GetMoveType(Vector2 startPos, Vector2 endPos)
        {
            Vector2 currentSwipe = (endPos - startPos).normalized;

            //swipe upwards
            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                return OptionType.Up;
            }
            //swipe down
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                return OptionType.Down;
            }
            //swipe left
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                return OptionType.Left;
            }
            //swipe right
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                return OptionType.Right;
            }

            return OptionType.Undefined;
        }


        private void EndState()
        {
            throw new NotImplementedException();
        }

        private void SelfieState()
        {
            OptionController.StartPlay();
            SetState(GameState.Input);
        }

        private void MessagingState()
        {
            float waitTime = MessageManager.PlayAnimation();
            DOVirtual.DelayedCall(waitTime, () =>
            {
                SetState(GameState.Selfie);
                MessageManager.Hide();
            });
            SetState(GameState.Input);
        }

        private void IdleState()
        {

        }

        private void InitState()
        {
            if (Input.GetMouseButtonUp(0))
            {
                TapToPlayAnimator.Hide();
                SetState(GameState.Messaging);
            }
        }
        private void SetState(GameState state)
        {
            GameState = state;
        }

    }

}