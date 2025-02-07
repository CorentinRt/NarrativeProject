using UnityEngine;
using UnityEditor;
using CREMOT.DialogSystem;
using System.Collections.Generic;

namespace NarrativeProject.Editor
{
    [CustomEditor(typeof(SO_Characters))]
    public class SO_CharactersEditor : UnityEditor.Editor
    {
        SO_Characters _target;
        bool updating = true;
        private void OnEnable()
        {
            _target = (SO_Characters)target;
            foreach (SO_CharacterData character in _target.Characters)
            {
                for (int i = 0; i < character.DrinkType.Count; i++)
                {
                    if (!character.DrinkEffects.ContainsKey(character.DrinkType[i]))
                    {
                        character.DrinkEffects.Add(character.DrinkType[i], character.DrinkEffect[i]);
                        character.DrinkEffectsFriendShip.Add(character.DrinkType[i], character.DrinkEffectFriendShip[i]);
                        Debug.Log("Added " + character.DrinkType[i] + " to the dictionary at " + character.DrinkEffect[i]);
                    }
                }
                /*for (int i = 0; i < character.DaysComing.Count; i++)
                {
                    Debug.Log("checking");
                    if (!character.DaysComingData.ContainsKey(character.DaysComing[i]))
                    {
                        Debug.Log("add eleemtn");
                        character.DaysComingData.Add(character.DaysComing[i], character.InteractionsData[i]);
                    }
                }*/
            }
        }
        private void OnDisable()
        {
            _target = (SO_Characters)target;
            foreach (SO_CharacterData character in _target.Characters)
            {
                for (int i = 0; i < character.DrinkType.Count; i++)
                {
                    if (!character.DrinkEffects.ContainsKey(character.DrinkType[i]))
                    {
                        character.DrinkEffects.Add(character.DrinkType[i], character.DrinkEffect[i]);
                        character.DrinkEffectsFriendShip.Add(character.DrinkType[i], character.DrinkEffectFriendShip[i]);
                        Debug.Log("Added " + character.DrinkType[i] + " to the dictionary at " + character.DrinkEffect[i]);
                    }
                }
                /*for (int i = 0; i < character.DaysComing.Count; i++)
                {
                    Debug.Log("checking");
                    if (!character.DaysComingData.ContainsKey(character.DaysComing[i]))
                    {
                        Debug.Log("add eleemtn");
                        character.DaysComingData.Add(character.DaysComing[i], character.InteractionsData[i]);
                    }
                }*/
            }

        }

        public override void OnInspectorGUI()
        {
            EditorUtility.SetDirty(_target);
            Color baseColor = GUI.backgroundColor;
            GUIStyle background = new GUIStyle(GUI.skin.button);
            background.normal.background = MakeBackgroundTexture(1, 1, new Color(0.75f, 0.75f, 0.75f, 1));
            GUI.backgroundColor = Color.grey;
            if(GUILayout.Button("Get all Character Data", background))
            {
                string[] files = AssetDatabase.FindAssets("t:SO_CharacterData");
                foreach (string file in files)
                {
                    if (!_target.Characters.Contains((SO_CharacterData)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(file), typeof(SO_CharacterData))))
                    {
                        Debug.Log("Add new Character Data");
                        EditorUtility.SetDirty(_target);
                        _target.Characters.Add((SO_CharacterData)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(file), typeof(SO_CharacterData)));
                        AssetDatabase.SaveAssetIfDirty(_target);
                    }

                }
            }
            if (GUILayout.Button("Add Character Data", background))
            {
                PopupWindow.ShowWindow();
            }
            GUI.backgroundColor = baseColor;
            if(updating)
            foreach (SO_CharacterData character in _target.Characters)
            {
                using ( new GUILayout.VerticalScope(EditorStyles.helpBox))
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
                     for (int i = 0; i < character.Sprites.Count; i++)
                     {
                         Sprite sprite = character.Sprites[i];
                         EditorGUILayout.BeginHorizontal();

                         EditorGUILayout.LabelField("Sprite " + i);
                         character.Sprites[i] = (Sprite)EditorGUILayout.ObjectField(character.Sprites[i], typeof(Sprite), false);

                         EditorGUILayout.EndHorizontal();
                     }
                     /*background.normal.background = MakeBackgroundTexture(1, 1, Color.cyan);
                        GUI.backgroundColor = Color.cyan;
                        GUILayout.Space(25);
                        if (GUILayout.Button("Add Days Coming", background))
                        {
                            character.DaysComing.Add(0);
                            character.InteractionsData.Add(new DayInteractions());
                        }

                        for (int i = 0; i < character.DaysComing.Count; i++)
                        {
                            EditorGUILayout.Space(20);
                            GUILayout.BeginHorizontal();
                            GUI.backgroundColor = baseColor;
                            EditorGUILayout.LabelField("Day : ", GUILayout.Width(30));
                            character.DaysComing[i] = EditorGUILayout.IntSlider(character.DaysComing[i], 0, 7, GUILayout.Width(500));
                            background.normal.background = MakeBackgroundTexture(1, 1, Color.red);

                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.BeginHorizontal();

                            DayInteractions interactions = character.InteractionsData[i];
                            EditorGUILayout.LabelField("Interactions Before Coming : ", GUILayout.Width(175));
                            interactions.InteractionsBeforeComing = EditorGUILayout.IntField(interactions.InteractionsBeforeComing, GUILayout.Width(50));
                            GUILayout.Space(15);
                            EditorGUILayout.LabelField("Interactions Before Leaving : ", GUILayout.Width(175));
                            interactions.InteractionsBeforeLeaving = EditorGUILayout.IntField(interactions.InteractionsBeforeLeaving, GUILayout.Width(50));
                            character.InteractionsData[i] = interactions;

                            EditorGUILayout.EndHorizontal();
                            GUI.backgroundColor = Color.red;

                            if (GUILayout.Button("Remove Item", background, GUILayout.Width(125)))
                            {
                                character.DaysComing.RemoveAt(i);
                                character.InteractionsData.RemoveAt(i);
                                character.DaysComingData.Remove(character.DaysComing[i]);
                            }
                        }*/
                     background.normal.background = MakeBackgroundTexture(1, 1, Color.green);
                     GUI.backgroundColor = Color.green;
                     GUILayout.Space(25);
                     if (GUILayout.Button("Add Drinks Effects", background))
                     {
                         character.DrinkEffect.Add(0);
                         character.DrinkType.Add(DrinkType.Cofee);
                         character.DrinkEffectFriendShip.Add(0);
                     }
                     GUI.backgroundColor = baseColor;
                     GUILayout.Space(20);


                    using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                    {
                        for (int i = 0; i < character.DrinkEffect.Count; i++)
                        {
                            EditorGUILayout.BeginHorizontal();

                            EditorGUILayout.LabelField("Drink Type : ", GUILayout.Width(75));
                            character.DrinkType[i] = (DrinkType)EditorGUILayout.EnumPopup(character.DrinkType[i]);
                            EditorGUILayout.Space(25);

                            EditorGUILayout.LabelField("DrinkScale :", GUILayout.Width(75));
                            character.DrinkEffect[i] = EditorGUILayout.IntField(character.DrinkEffect[i], GUILayout.Width(50));
                            if (character.DrinkEffect[i] < 0) character.DrinkEffect[i] = 0;
                            else if (character.DrinkEffect[i] > 100) character.DrinkEffect[i] = 100;

                            EditorGUILayout.Space(25);
                            EditorGUILayout.LabelField("FriendScale :", GUILayout.Width(75));
                            character.DrinkEffectFriendShip[i] = EditorGUILayout.IntField(character.DrinkEffectFriendShip[i], GUILayout.Width(50));
                            if (character.DrinkEffectFriendShip[i] < 0) character.DrinkEffectFriendShip[i] = 0;
                            else if (character.DrinkEffectFriendShip[i] > 100) character.DrinkEffectFriendShip[i] = 100;
                            background.normal.background = MakeBackgroundTexture(1, 1, Color.red);
                            GUI.backgroundColor = Color.red;

                            EditorGUILayout.EndHorizontal();
                            if (GUILayout.Button("Remove Item", background, GUILayout.Width(125)))
                            {
                                //updating = false;
                                character.DrinkEffect.RemoveAt(i);
                                character.DrinkType.RemoveAt(i);
                                character.DrinkEffects.Remove(character.DrinkType[i]);
                                GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/_project/prefabs/Characters/" + character.Name + ".prefab", typeof(GameObject));
                                if (prefab != null) AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(character));
                            }
                            GUI.backgroundColor = baseColor;
                        }
                    }
                    
                     GUI.backgroundColor = Color.red;
                     GUILayout.Space(25);
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
            background.normal.background = MakeBackgroundTexture(1, 1, Color.blue);
            GUI.backgroundColor = Color.blue;
            if (GUILayout.Button("Generate", background, GUILayout.Height(50)))
            {
                GameObject prefab = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/_project/prefabs/Characters/Default.prefab", typeof(GameObject));
                foreach (SO_CharacterData c in _target.Characters)
                {
                    GenerateCharacters(prefab, c);
                }
            }
            GUI.backgroundColor = baseColor;
        }

        void GenerateCharacters(Object p, SO_CharacterData chara)
        {
            Debug.Log("Generating " + chara.Name);
            GameObject character = (GameObject)Instantiate(p);
            character.name = chara.Name;
            if(chara.Sprites[0] != null)
            {
                character.GetComponentInChildren<SpriteRenderer>().sprite = chara.Sprites[0];
            }
            if(chara.DialogueGraphData != null)
            {
                character.GetComponentInChildren<DialogueController>().DialogueGraphSO = chara.DialogueGraphData;
            }

            PrefabUtility.SaveAsPrefabAsset(character, "Assets/_project/prefabs/Characters/InGame/" + chara.Name + ".prefab");
            DestroyImmediate(character);
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
