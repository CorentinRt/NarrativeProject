using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace NarrativeProject.Editor
{   
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SO_CharacterData))]
    public class SO_CharacterDataEditor : UnityEditor.Editor
    {
        bool _updating = false, init = true;
        SO_CharacterData _target;
   
        private void OnEnable()
        {
            _target = (SO_CharacterData)target;
        }

        private void OnDisable()
        {
            for(int i = 0; i < _target.DrinkType.Count; i++)
            {
                if (!_target.DrinkEffects.ContainsKey(_target.DrinkType[i]))
                {
                    _target.DrinkEffects.Add(_target.DrinkType[i], _target.DrinkEffect[i]);
                    Debug.Log("Added " + _target.DrinkType[i] + " to the dictionary at " + _target.DrinkEffect[i]);
                }
            }
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if(GUILayout.Button("Add Drinks Effects"))
            {
                _target.DrinkEffect.Add(DrunkState.Arrache);
                _target.DrinkType.Add(DrinkType.Biere);
            }
            for (int i = 0; i < _target.DrinkEffect.Count; i++)
            {
                Debug.Log(_target.DrinkEffect.Count);
                EditorGUILayout.BeginHorizontal();
                _target.DrinkType[i] = (DrinkType)EditorGUILayout.EnumPopup(_target.DrinkType[i]);
                _target.DrinkEffect[i] = (DrunkState)EditorGUILayout.EnumPopup(_target.DrinkEffect[i]);
                if (GUILayout.Button("Remove"))
                {
                    _target.DrinkEffect.RemoveAt(i);
                    _target.DrinkType.RemoveAt(i);
                    _target.DrinkEffects.Remove(_target.DrinkType[i]);
                }
                EditorGUILayout.EndHorizontal();
            }

        }
    }
}  
