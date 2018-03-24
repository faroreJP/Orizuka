//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//
// Object registering entry
//
// @Author : Farore
// @Date   : 2018/03/24
//
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

namespace Orizuka {
  [System.Serializable]
  public class ObjectEntry {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Field
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    [UnityEngine.SerializeField]  private string             _identifier = null;
    [UnityEngine.SerializeField]  private UnityEngine.Object _object     = null;

    public string Identifier {
      get { return _identifier; }
    }

    public UnityEngine.Object Object {
      get { return _object; }
    }

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Constructor
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    public ObjectEntry (string identifier, UnityEngine.Object obj) {
      _identifier = identifier;
      _object     = obj;
    }
  }
}
