//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//
// IObjectRegistry implementation with Assetdatabase
// @NOTE :
//  This class is valid on Unity editor only.
//
// @Author : Farore
// @Date   : 2018/03/27
//
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

using System.IO;
using System.Linq;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Orizuka {
  public class AssetdatabaseObjectRegistry : IObjectRegistry {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Field
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private string                                  _basePath      = null;
    private Dictionary<string, UnityEngine.Object>  _loadedObjects = new Dictionary<string, UnityEngine.Object>();

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    // Construct registry with specified base path
    // @Param[in] basePath : The base path to load asset in Assets/ (e.g. "Assets/Path/To")
    public AssetdatabaseObjectRegistry (string basePath) {
      _basePath = basePath;
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Public Method
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    // Create object entries to store assets into ScriptableObjectRegistry
    // @Return : The object entries (based on _loadedObjects)
    public IEnumerable<ObjectEntry> CreateObjectEntries () {
      return _loadedObjects.Select(p => new ObjectEntry(p.Key, p.Value));
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // IObjectRegistry
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    // Get registered object
    // @Param[in] identifier : The object identifier (e.g. "someobj")
    // @Return : The specified object instance
    public T Get<T> (string identifier) where T : UnityEngine.Object {
      return Load<T>(identifier);
    }

    // Try get registered object (Throws no exceptions)
    // @Param[in]  identifier : The object identifier (e.g. "someobj")
    // @Param[out] obj        : The object of result
    // @Return :
    //  true  - sucess, the obj will contain specified object instance
    //  false - failed, the obj will be null
    public bool TryGet<T> (string identifier, out T obj) where T : UnityEngine.Object {
      obj = Load<T>(identifier);
      return obj != null;
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Private Method
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private T Load<T> (string path) where T : UnityEngine.Object {
#if UNITY_EDITOR
      // use cache if exists
      if (_loadedObjects.ContainsKey(path)) {
        return (T)_loadedObjects[path];
      }

      // build path and check the asset exists
      var assetPath = Path.Combine(_basePath, path);
      if (string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(assetPath))) {
        UnityEngine.Debug.LogError("[AssetdatabaseObjectRegistry] the asset is not found! : " + assetPath);
        return null;
      }

      // load asset and create cache
      var asset            = AssetDatabase.LoadAssetAtPath<T>(assetPath);
      _loadedObjects[path] = asset;

      return asset;
#else
      throw new System.NotSupportedException();
#endif
    }
  }
}
