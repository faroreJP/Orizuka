//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
//
// Custom property drawer for ObjectEntry
//
// @Author : Farore
// @Date   : 2018/03/24
//
//::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::

using UnityEngine;
using UnityEditor;

namespace Orizuka {
  [CustomPropertyDrawer(typeof(ObjectEntry))]
  public class ObjectEntryPropertyDrawer : PropertyDrawer {
    public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
      // find field
      var identifierProp = property.FindPropertyRelative("_identifier");
      var objProp        = property.FindPropertyRelative("_object");

      // compute position
      var halfW         = position.width * 0.5f;
      var identifierPos = new Rect(position.x,         position.y, halfW, position.height);
      var objPos        = new Rect(position.x + halfW, position.y, halfW, position.height);

      // GUI
      identifierProp.stringValue   = EditorGUI.TextField(identifierPos, identifierProp.stringValue);
      objProp.objectReferenceValue = EditorGUI.ObjectField(objPos,      objProp.objectReferenceValue, typeof(UnityEngine.Object), false);
    }
  }
}
