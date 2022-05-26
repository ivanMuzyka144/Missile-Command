using System.Collections.Generic;

namespace CodeBase.Services
{
  public class AllServices
  {
    private static AllServices _instance;
    public static AllServices Container => _instance ?? (_instance = new AllServices());

    public readonly List<IService> Services = new List<IService>();

    public void RegisterSingle<TService>(TService implementation) where TService : IService
    {
      Services.Add(implementation);
      Implementation<TService>.ServiceInstance = implementation;
    }

    public TService Single<TService>() where TService : IService =>
      Implementation<TService>.ServiceInstance;

    private class Implementation<TService> where TService : IService
    {
      public static TService ServiceInstance;
    }
  }
}