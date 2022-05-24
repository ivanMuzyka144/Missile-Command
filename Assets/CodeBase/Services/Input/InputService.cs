using System;
using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Services.Input
{
  public class InputService : IInputService
  {
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private Vector3 _borderPosition;

    public event Action<Vector2> OnMouseMoved;
    public event Action<Vector2> OnMouseClicked;

    private Vector2 _mousePosition => UnityEngine.Input.mousePosition;

    public void SetBorder(Vector3 borderPosition) =>
      _borderPosition = borderPosition;

    public void RecordMousePosition()
    {
      if (!IsKeyClicked() && InRecordArea(_mousePosition))
        OnMouseMoved?.Invoke(_mousePosition);
    }

    public void RecordMouseCLicked()
    {
      if (IsKeyClicked() && InRecordArea(_mousePosition))
        OnMouseClicked?.Invoke(_mousePosition);
    }

    private bool IsKeyClicked() =>
      UnityEngine.Input.GetKeyDown(KeyCode.Mouse0);

    private bool InRecordArea(Vector2 mousePosition) => 
      mousePosition.FromScreenToWorld().y > _borderPosition.y;
  }
}