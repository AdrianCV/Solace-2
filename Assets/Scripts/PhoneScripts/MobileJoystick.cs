using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IPointerUpHandler, IDragHandler, IPointerDownHandler
{
    private RectTransform _joystickTransform;

    [SerializeField] float dragThreshold = 0.6f;
    [SerializeField] float dragMoveDistance = 30;
    [SerializeField] float dragOffsetDistance = 100;

    public event Action<Vector2> OnMove;

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 offset;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickTransform, eventData.position, null, out offset);

        offset = Vector2.ClampMagnitude(offset, dragOffsetDistance) / dragOffsetDistance;

        _joystickTransform.anchoredPosition = offset * dragMoveDistance;

        Vector2 inputVector = CalculateInputVector(offset);
        OnMove?.Invoke(inputVector);
    }

    private Vector2 CalculateInputVector(Vector2 offset)
    {
        float x = Mathf.Abs(offset.x) > dragThreshold ? offset.x : 0;
        float y = Mathf.Abs(offset.y) > dragThreshold ? offset.y : 0;

        return new Vector2(x, y);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // throw new NotImplementedException();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _joystickTransform.anchoredPosition = Vector2.zero;
        OnMove?.Invoke(Vector2.zero);
    }

    private void Awake()
    {
        _joystickTransform = (RectTransform)transform;
    }
}
