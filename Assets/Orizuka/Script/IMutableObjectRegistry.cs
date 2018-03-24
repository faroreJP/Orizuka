//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//
// Mutable object registry interface
//
// @Author : Farore
// @Date   : 2018/03/24
//
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace Orizuka {
  public interface IMutableObjectRegistry : IObjectRegistry {
    // Register object to registry
    // @Param[in] identifier : The object identifier (e.g. "someobj")
    // @Param[in] obj        : The object instance
    // @Return :
    //  true  - the registering was succeeded
    //  false - the registering was failed
    bool Register<T> (string identifier, T obj) where T : UnityEngine.Object;
  }
}
