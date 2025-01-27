using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace NarrativeProject.Editor
{
    [CustomEditor(typeof(SO_Characters))]
    public class SO_CharactersEditor : UnityEditor.Editor
    {
        SO_Characters _target;

        private void OnEnable()
        {
            _target = (SO_Characters)target;
        }
        private void OnDisable()
        {
            
        }

        public override void OnInspectorGUI()
        {
            Color baseColor = GUI.backgroundColor;
            GUIStyle background = new GUIStyle(GUI.skin.button);
            background.normal.background = MakeBackgroundTexture(1, 1, Color.green);
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add Character Data", background))
            {
                PopupWindow.ShowWindow();
            }
            GUI.backgroundColor = baseColor;
            foreach (SO_CharacterData character in _target.Characters)
            {
                EditorGUILayout.BeginVertical();

                Sprite sp = character.Sprites[0];
                if (sp != null)
                {
                    var texture = AssetPreview.GetAssetPreview(sp);
                    if (texture != null)
                    {
                        GUILayout.Label(texture.name, GUILayout.Width(50), GUILayout.Height(50));
                        GUI.DrawTexture(GUILayoutUtility.GetLastRect(), texture);
                    }
                }

                character.Name = EditorGUILayout.TextField("Name:", character.Name);

                character.DefaultdrunkScale = EditorGUILayout.IntSlider("Default DrunkScale", character.DefaultdrunkScale, 0, 100);
                character.DefaultFriendShipScale = EditorGUILayout.IntSlider("Default FriendScale", character.DefaultFriendShipScale, 0, 100);
                EditorGUILayout.LabelField("Sprites");
                for(int i = 0; i < character.Sprites.Count; i++)
                {
                    Sprite sprite = character.Sprites[i];
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField("Sprite " + i);
                    character.Sprites[i] = (Sprite)EditorGUILayout.ObjectField(character.Sprites[i], typeof(Sprite), false);

                    EditorGUILayout.EndHorizontal();
                }
                GUI.backgroundColor = Color.green;
                if (GUILayout.Button("Add Drinks Effects", background))
                {
                    character.DrinkEffect.Add(0);
                    character.DrinkType.Add(DrinkType.Cofee);
                    character.DrinkEffectFriendShip.Add(0);
                }
                GUI.backgroundColor = baseColor;
                for (int i = 0; i < character.DrinkEffect.Count; i++)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField("Drink Type : ", GUILayout.Width(75));
                    character.DrinkType[i] = (DrinkType)EditorGUILayout.EnumPopup(character.DrinkType[i]);
                    EditorGUILayout.Space(25);
                    EditorGUILayout.LabelField("DrinkScale :", GUILayout.Width(75));
                    character.DrinkEffect[i] = EditorGUILayout.IntSlider(character.DrinkEffect[i], 0, 100);
                    EditorGUILayout.Space(25);
                    EditorGUILayout.LabelField("FriendScale :", GUILayout.Width(75));
                    character.DrinkEffectFriendShip[i] = EditorGUILayout.IntSlider(character.DrinkEffectFriendShip[i], 0, 100);

                    background.normal.background = MakeBackgroundTexture(1, 1, Color.red);
                    GUI.backgroundColor = Color.red;

                    EditorGUILayout.EndHorizontal();
                    if (GUILayout.Button("Remove", background, GUILayout.Width(100)))
                    {
                        character.DrinkEffect.RemoveAt(i);
                        character.DrinkType.RemoveAt(i);
                        character.DrinkEffects.Remove(character.DrinkType[i]);
                    }
                    GUI.backgroundColor = baseColor;
                }
                    GUI.backgroundColor = Color.red;
                    if (GUILayout.Button("Delete", background))
                    {
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(character));
                        _target.Characters.Remove(character);
                    }
                    GUI.backgroundColor = baseColor;
                    EditorGUILayout.Space(50);
                    EditorGUILayout.EndVertical();
                
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
