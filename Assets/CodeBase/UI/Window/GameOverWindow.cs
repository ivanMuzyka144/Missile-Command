using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Window
{
  public class GameOverWindow : WindowBase
  {
    [SerializeField] private Button _restartButton;

    public Action OnRestartButtonClicked;

    public void Construct(Action restartAction) => 
      _restartButton.onClick.AddListener(restartAction.Invoke);

    private void OnDestroy() => 
      _restartButton.onClick.RemoveAllListeners();
  }
}
