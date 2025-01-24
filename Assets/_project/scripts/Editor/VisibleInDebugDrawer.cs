using UnityEngine;
using UnityEditor;

namespace NarrativeProject.Editor
{
    [CustomPropertyDrawer(typeof(VisibleInDebug))]
    public class VisibleInDebugDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}

