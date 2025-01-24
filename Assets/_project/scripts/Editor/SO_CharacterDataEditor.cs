using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;

namespace NarrativeProject.Editor
{   
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SO_CharacterData))]
    public class SO_CharacterDataEditor : UnityEditor.Editor
    {
        bool _updating = false, init = true;
        SO_CharacterData _target;
        Texture t;

        private void OnEnable()
        {
            _target = (SO_CharacterData)target;
            t = AssetDatabase.LoadAssetAtPath<Texture>("Assets/_project/scripts/Editor/icon.png");
            Debug.Log(t);
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
            EditorGUILayout.BeginHorizontal();
            foreach (Sprite sprite in _target.Sprites)
            {

                if (sprite != null)
                {
                    EditorGUILayout.LabelField(sprite.name, GUILayout.Width(100));
                    var texture = AssetPreview.GetAssetPreview(sprite);
                    if (texture != null)
                    {
                        GUILayout.Label(texture.name, GUILayout.Width(100), GUILayout.Height(100));
                        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();
            Color baseColor = GUI.backgroundColor;
            GUIStyle background = new GUIStyle(GUI.skin.button);
            background.normal.background = MakeBackgroundTexture(1, 1, Color.green);
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add Drinks Effects", background))
            {
                _target.DrinkEffect.Add(DrunkState.Arrache);
                _target.DrinkType.Add(DrinkType.Vin_Puissance_1);
            }
            GUI.backgroundColor = baseColor;
            for (int i = 0; i < _target.DrinkEffect.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Drink Type : ", GUILayout.Width(75));
                _target.DrinkType[i] = (DrinkType)EditorGUILayout.EnumPopup(_target.DrinkType[i]);
                EditorGUILayout.Space(25);
                EditorGUILayout.LabelField("Drink Effect : ", GUILayout.Width(75));
                _target.DrinkEffect[i] = (DrunkState)EditorGUILayout.EnumPopup(_target.DrinkEffect[i]);
                EditorGUILayout.Space(40);

                background.normal.background = MakeBackgroundTexture(1, 1, Color.red);
                GUI.backgroundColor = Color.red;

                if (GUILayout.Button("Remove", background))
                {
                    _target.DrinkEffect.RemoveAt(i);
                    _target.DrinkType.RemoveAt(i);
                    _target.DrinkEffects.Remove(_target.DrinkType[i]);
                }
                GUI.backgroundColor = baseColor;
                EditorGUILayout.EndHorizontal();
            }

        }
        private Texture2D MakeBackgroundTexture(int width, int height, Color color)
        {
            Color[] pixels = new Color[width * height];

            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = color;
            }

            Texture2D backgroundTexture = new Texture2D(width, height);

            backgroundTexture.SetPixels(pixels);
            backgroundTexture.Apply();

            return backgroundTexture;
        }
    }
}  
