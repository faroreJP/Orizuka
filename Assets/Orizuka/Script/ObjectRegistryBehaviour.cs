//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//
// Object registry implementation with MonoBehaviour
//
// @Author : Farore
// @Date   : 2018/03/24
//
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Orizuka {
  public class ObjectRegistryBehaviour : MonoBehaviour, IMutableObjectRegistry, ISerializationCallbackReceiver {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Field
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    [SerializeField, HideInInspector] private ObjectEntry[]                           _objectEntries = null;
                                      private Dictionary<string, UnityEngine.Object>  _objects       = new Dictionary<string, UnityEngine.Object>();

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Static Method
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public static ObjectRegistryBehaviour CreateInstance () {
      var obj = new GameObject("ObjectRegistryBehaviour", typeof(ObjectRegistryBehaviour));
      return obj.GetComponent<ObjectRegistryBehaviour>();
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // IMutableObjectRegistry
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

    // Register object to registry
    // @Param[in] identifier : The object identifier (e.g. "someobj")
    // @Param[in] obj        : The object instance
    // @Return :
    //  true  - the registering was succeeded
    //  false - the registering was failed
    public bool Register<T> (string identifier, T obj) where T : UnityEngine.Object {
      if (string.IsNullOrEmpty(identifier)) {
        return false;
      }

      _objects[identifier] = obj;
      return true;
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // ISerializationCallbackReceiver
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public void OnBeforeSerialize () {
      if (_objects != null) {
        _objectEntries = _objects
                          .Where(o => !string.IsNullOrEmpty(o.Key))
                          .Select(o => new ObjectEntry(o.Key, o.Value))
                          .ToArray();
      }
    }

    public void OnAfterDeserialize () {
      if (_objectEntries != null) {
        _objects = _objectEntries
                    .Where(e => !string.IsNullOrEmpty(e.Identifier))
                    .ToDictionary(e => e.Identifier, e => e.Object);
      }
    }
  }
}
