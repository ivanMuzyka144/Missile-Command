using System;
using CodeBase.UI.Window;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI
{
   public class LevelCompletedWindow : WindowBase
   {
      [SerializeField] private TextMeshProUGUI _scoreValueText;
      [SerializeField] private TextMeshProUGUI _ammoValueText;
      [SerializeField] private Button _nextButton;
      [SerializeField] private Button _restartButton;
      
      public void Construct(Action nextButtonAction, Action restartAction)
      {
         _nextButton.onClick.AddListener(nextButtonAction.Invoke);
         _restartButton.onClick.AddListener(restartAction.Invoke);
      }

      public void PlayLevelCompleteSequence()
      {
         
      }

      private void OnDestroy()
      {
         _nextButton.onClick.RemoveAllListeners();
         _restartButton.onClick.RemoveAllListeners();
      }
   }
}
