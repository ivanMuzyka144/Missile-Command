using System;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Services.Input
{
  public interface IInputService : IService
  {
    Vector2 Axis { get; }

    event Action<Vector2> OnMouseMoved;
    event Action<Vector2> OnMouseClicked;
    void RecordMousePosition();
    void RecordMouseCLicked();
  }
}