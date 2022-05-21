using System;
using UnityEngine;

namespace CodeBase.Services.Input
{
  public class InputService : IInputService
  {
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public event Action<Vector2> OnMouseMoved;
    public event Action<Vector2> OnMouseClicked;

    public Vector2 Axis { get; }
    public void RecordMousePosition()
    {
      if (!IsKeyClicked()) 
        OnMouseMoved?.Invoke(UnityEngine.Input.mousePosition);
    }

    public void RecordMouseCLicked()
    {
      if (IsKeyClicked()) 
        OnMouseClicked?.Invoke(UnityEngine.Input.mousePosition);
    }

    private bool IsKeyClicked() => 
      UnityEngine.Input.GetKeyDown(KeyCode.Mouse0);
  }
}