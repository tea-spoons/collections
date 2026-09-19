
namespace TeaSpoons.Collections.Editor
{
    using UnityEngine;
    using UnityEditor;
    using System.Collections.Generic;

    [CustomPropertyDrawer(typeof(SerializableDictionary<,>))]
    public class SerializedDictionaryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("entries"), label);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property.FindPropertyRelative("entries"), label);
        }
    }
}
