using CodeBase.Infrastructure.Factory;
using UnityEngine;

namespace CodeBase.Logic.Player
{
  public class PlayerHouse : MonoBehaviour, IHittable
  {
    public int HouseId { get; private set; }
    
    private IGameFactory _factory;

    public void Construct( int houseId, IGameFactory factory)
    {
      HouseId = houseId;
      
      _factory = factory;
    }

    public void Hit() => 
      _factory.DestroyPlayerHouse(this);
  }
}