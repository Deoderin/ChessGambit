using Infrastructure.Factory;

namespace Infrastructure.Services{
  public class AllServices{
    private static AllServices _instance;
    public static AllServices Container => _instance ??= new AllServices();

    public void RegisterSingle<TService>(TService _implementation) where TService : IService{
      Implementation<TService>.ServiceInstance = _implementation;
    }

    public TService Single<TService>() where TService : IService{
      return Implementation<TService>.ServiceInstance;
    }
    
    private static class Implementation<TService> where TService : IService{
      public static TService ServiceInstance;
    }
  }
}