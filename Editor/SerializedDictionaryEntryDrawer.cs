
namespace TeaSpoons.Collections.Editor
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    [CustomPropertyDrawer(typeof(SerializableDictionary<,>.Entry))]
    public class SerializedDictionaryEntryDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var left = new Rect(position)
            {
                width = position.width * 0.5f - 1,
            };
            var right = new Rect(position)
            {
                width = left.width,
                x = left.xMax + 2
            };

            EditorGUI.PropertyField(left, property.FindPropertyRelative("Key"), GUIContent.none);
            EditorGUI.PropertyField(right, property.FindPropertyRelative("Value"), GUIContent.none);
        }
    }
}
