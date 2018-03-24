//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//
// Custom editor for ObjectRegistryBehaviour
//
// @Author : Farore
// @Date   : 2018/03/24
//
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

using UnityEngine;
using UnityEditor;

namespace Orizuka {
  [CustomEditor(typeof(ObjectRegistryBehaviour)), CanEditMultipleObjects]
  public class ObjectRegistryBehaviourEditor : Editor {
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Field
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private SerializedProperty _entriesProp = null;

    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    // Unity Callback
    //::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

    private void OnEnable () {
      _entriesProp = serializedObject.FindProperty("_objectEntries");
    }

    public override void OnInspectorGUI () {
      // update object
      serializedObject.Update();

      // total entry num
      EditorGUILayout.LabelField("Object Num : " + _entriesProp.arraySize);

      // each entries
      EditorGUI.BeginDisabledGroup(true);
      EditorGUI.indentLevel++;

      for (int i = 0;i < _entriesProp.arraySize;i++) {
        var entryProp = _entriesProp.GetArrayElementAtIndex(i);
        EditorGUILayout.PropertyField(entryProp, true);
      }

      EditorGUI.indentLevel--;
      EditorGUI.BeginDisabledGroup(false);

      // apply changes
      serializedObject.ApplyModifiedProperties();
    }
  }
}
