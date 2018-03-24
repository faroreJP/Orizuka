//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//
// IObjectRegistry implementation with scriptable object
//
// @Author : Farore
// @Date   : 2018/03/24
//
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

using System.Linq;
using System.Collections.Generic;

namespace Orizuka {
  [UnityEngine.CreateAssetMenu]
  public class ScriptableObjectRegistry : UnityEngine.ScriptableObject, IObjectRegistry {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Field
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    [UnityEngine.SerializeField]  private ObjectEntry[]                           _objectEntries = null;
                                  private Dictionary<string, UnityEngine.Object>  _objects       = null;

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Unity Callback
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private void OnEnable () {
      if (_objectEntries != null) {
        _objects = _objectEntries
                    .Where(o => !string.IsNullOrEmpty(o.Identifier))
                    .ToDictionary(o => o.Identifier, o => o.Object);
      }
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // IObjectRegistry
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    // Get registered object
    // @Param[in] identifier : The object identifier (e.g. "someobj")
    // @Return : The specified object instance
    public T Get<T> (string identifier) where T : UnityEngine.Object {
      return (T)_objects[identifier];
    }

    // Try get registered object (Throws no exceptions)
    // @Param[in]  identifier : The object identifier (e.g. "someobj")
    // @Param[out] obj        : The object of result
    // @Return :
    //  true  - sucess, the obj will contain specified object instance
    //  false - failed, the obj will be null
    public bool TryGet<T> (string identifier, out T obj) where T : UnityEngine.Object {
      UnityEngine.Object objInstance = null;

      if (_objects.TryGetValue(identifier, out objInstance)) {
        obj = objInstance as T;
        return obj != null;
      }

      obj = null;
      return false;
    }
  }
}
