//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//
// The main interface of object registry
//
// @Author : Farore
// @Date   : 2018/03/24
//
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace Orizuka {
  public interface IObjectRegistry {
    // Get registered object
    // @Param[in] identifier : The object identifier (e.g. "someobj")
    // @Return : The specified object instance
    T Get<T> (string identifier) where T : UnityEngine.Object;

    // Try get registered object (Throws no exceptions)
    // @Param[in]  identifier : The object identifier (e.g. "someobj")
    // @Param[out] obj        : The object of result
    // @Return :
    //  true  - sucess, the obj will contain specified object instance
    //  false - failed, the obj will be null
    bool TryGet<T> (string identifier, out T obj) where T : UnityEngine.Object;
  }
}
