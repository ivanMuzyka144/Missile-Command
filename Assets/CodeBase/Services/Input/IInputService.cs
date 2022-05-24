using System;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Services.Input
{
  public interface IInputService : IService
  {
    event Action<Vector2> OnMouseMoved;
    event Action<Vector2> OnMouseClicked;

    void SetBorder(Vector3 borderPosition);
    void RecordMousePosition();
    void RecordMouseCLicked();
    
  }
}