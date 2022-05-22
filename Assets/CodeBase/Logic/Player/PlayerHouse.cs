using UnityEngine;

namespace CodeBase.Logic.Player
{
  public class PlayerHouse : MonoBehaviour, IHittable
  {
    public void Hit()
    {
      Destroy(gameObject);
    }
  }
}