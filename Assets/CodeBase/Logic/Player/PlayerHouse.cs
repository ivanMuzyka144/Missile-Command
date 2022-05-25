using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Logic.Player
{
  public class PlayerHouse : MonoBehaviour, IHittable
  {
    private IGameFactory _factory;
    public void Construct(IGameFactory factory) => 
      _factory = factory;

    public void Hit() => 
      _factory.DestroyPlayerHouse(this);
  }
}