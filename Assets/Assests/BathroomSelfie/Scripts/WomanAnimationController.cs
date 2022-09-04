using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WomanAnimationController : MonoBehaviour
{
    [SerializeField] private Animator AnimationController;
    private int _poseZero;
    private int _poseOne;
    private int _poseTwo;
    private int _poseThree;

    private void Awake()
    {
        _poseZero = Animator.StringToHash("Pose_0");
        _poseOne = Animator.StringToHash("Pose_1");
        _poseTwo = Animator.StringToHash("Pose_2");
        _poseThree = Animator.StringToHash("Pose_3");
    }

    private void PlayPoseZero()
    {
        AnimationController.SetTrigger(_poseZero);
    }
    private void PlayPoseOne()
    {
        AnimationController.SetTrigger(_poseOne);
    }

    private void PlayPoseTwo()
    {
        AnimationController.SetTrigger(_poseTwo);
    }

    private void PlayPoseThree()
    {

        AnimationController.SetTrigger(_poseThree);
    }

    public void Play(OptionType animationType)
    {
        switch (animationType)
        {
            case OptionType.Up:
                PlayPoseZero();
                break;
            case OptionType.Down:
                PlayPoseOne();
                break;
            case OptionType.Left:
                PlayPoseTwo();
                break;
            case OptionType.Right:
                PlayPoseThree();
                break;

            default:
                break;
        }
    }
}
