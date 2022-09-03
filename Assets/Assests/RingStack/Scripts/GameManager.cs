using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public enum GameState
{
    Init,
    Idle,
    Selecting,
    Moving,
    Dropped
}
public class GameManager : MonoBehaviour
{
    [SerializeField] RingSelector RingSelector;
    [SerializeField] GameState GameState;
    [SerializeField] RingManager SelectedRing;
    [SerializeField] GhostManager[] GhostRings;

    private Vector3 _ringPositionAtSelection;
    private Vector3 _ringDropPosition;
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
            case GameState.Selecting:
                SelectionState();
                break;
            case GameState.Moving:
                MovingState();
                break;
            case GameState.Dropped:
                DroppedState();
                break;

            default:
                break;
        }
    }

    private void DroppedState()
    {

        if (_ringDropPosition.Equals(Vector3.zero))
        {
            _ringDropPosition = _ringPositionAtSelection;
        }

        SelectedRing.transform.DOLocalMove(_ringDropPosition, .25f);
        SetState(GameState.Idle);
    }

    private void MovingState()
    {


        var mousePos = Input.mousePosition;
        mousePos.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        var point = Camera.main.ScreenToWorldPoint(mousePos);

        var canMoveHorizontal = SelectedRing.GetCanMoveHorizontal();
        var canMoveVertical = SelectedRing.GetCanMoveVertical();

        if (!canMoveHorizontal && !canMoveVertical)
        {
            SetState(GameState.Selecting);
            return;
        }

        var ringPos = SelectedRing.transform.localPosition;
        if (canMoveVertical)
        {
            ringPos.y = point.y;
            ringPos.y = Mathf.Clamp(ringPos.y, 0, 1);
        }
        if (canMoveHorizontal)
        {
            ringPos.x = point.x;
            ringPos.x = Mathf.Clamp(ringPos.x, -.8f, .8f);
        }

        SelectedRing.transform.localPosition = ringPos;

        _ringDropPosition = SelectedRing.GetPositionToDrop();

        if (SelectedRing.GetCanPlaceAtTheSpot())
        {
            SetGhostRing(_ringDropPosition);
        }
        else
        {
            ResetGhostRings();
        }

        if (Input.GetMouseButtonUp(0))
        {
            ResetGhostRings();
            SetState(GameState.Dropped);
        }

    }

    private void ResetGhostRings()
    {
        for (int i = 0; i < GhostRings.Length; i++)
        {
            var currentGhost = GhostRings[i];
            currentGhost.gameObject.SetActive(false);
        }
    }
    private void SetGhostRing(Vector3 placeAt)
    {
        GhostManager ghostOfSelectedRing = null;
        for (int i = 0; i < GhostRings.Length; i++)
        {
            var currentGhost = GhostRings[i];
            currentGhost.gameObject.SetActive(false);
            if (currentGhost.CompareRingType(SelectedRing.GetRingType()))
            {
                ghostOfSelectedRing = currentGhost;
            }
        }

        if (ghostOfSelectedRing != null)
        {
            ghostOfSelectedRing.gameObject.SetActive(true);
            ghostOfSelectedRing.transform.localPosition = placeAt;
        }
    }

    private void SelectionState()
    {
        var ring = RingSelector.TryAndGetRing();

        if (ring == null)
        {
            return;
        }

        SelectedRing = ring.GetComponent<RingManager>();
        _ringPositionAtSelection = ring.transform.localPosition;


        SetState(GameState.Moving);

    }

    private void IdleState()
    {
        if (Input.GetMouseButton(0))
        {
            SetState(GameState.Selecting);
        }

    }

    private void InitState()
    {
        throw new NotImplementedException();
    }

    private void SetState(GameState state)
    {
        GameState = state;
    }
}
