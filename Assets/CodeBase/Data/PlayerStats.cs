using System;

namespace CodeBase.Data
{
  [Serializable]
  public class PlayerStats
  {
    private int _score;
    
    public int Score
    {
      get => _score;
      set
      {
        _score = value;
        OnScoreChanged.Invoke();
      }
    }

    public Action OnScoreChanged;
  }
}