using System;
using UnityEditor;
using UnityEngine;

namespace NarrativeProject.Editor
{
    public class PopupWindow : EditorWindow
    {
        string _name = "DefaultName";
        public event Action<SO_CharacterData> OnCharacterData;
        public static void ShowWindow()
        {
            PopupWindow window = (PopupWindow)GetWindow(typeof(PopupWindow));
            window.minSize = new Vector2(400, 200);
            window.maxSize = new Vector2(400, 200);
        }

        void OnGUI()
        {
            GUILayout.Label("Please Enter a name to create the Character", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Label("Name: ");
            _name = GUILayout.TextField(_name);
            GUILayout.EndHorizontal();
            if ((Event.current.type == EventType.KeyUp) && (Event.current.keyCode == KeyCode.Return))
            {
                var newDataObj = ScriptableObject.CreateInstance<SO_CharacterData>();
                newDataObj.Name = _name;
                string path = "Assets/_project/scripts/Character/CharacterData/" + _name + "Data" + ".asset";
                AssetDatabase.CreateAsset(newDataObj, path);
                AssetDatabase.SaveAssets();
                EditorUtility.SetDirty(newDataObj);
                AssetDatabase.Refresh();
                SO_Characters c = (SO_Characters)AssetDatabase.LoadAssetAtPath("Assets/_project/scripts/Character/CharactersDatabase.asset", typeof(SO_Characters));
                Debug.Log(c);
                SO_CharacterData d = (SO_CharacterData)AssetDatabase.LoadAssetAtPath(path, typeof(SO_CharacterData));
                c.Characters.Add(d);
                Close();
            }
        }
    }
}
