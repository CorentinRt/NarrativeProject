using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.TextCore.Text;

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
            _updating = true;
            for (int i = 0; i < _target.DrinkType.Count; i++)
            {
                if (!_target.DrinkEffects.ContainsKey(_target.DrinkType[i]))
                {
                    _target.DrinkEffects.Add(_target.DrinkType[i], _target.DrinkEffect[i]);
                    _target.DrinkEffectsFriendShip.Add(_target.DrinkType[i], _target.DrinkEffectFriendShip[i]);
                    Debug.Log("Added " + _target.DrinkType[i] + " to the dictionary at " + _target.DrinkEffect[i]);
                }
            }
            for (int i = 0; i < _target.DaysComing.Count; i++)
            {
                Debug.Log("checking" + _target.DaysComing[i]);
                if (!_target.DaysComingData.ContainsKey(_target.DaysComing[i]))
                {
                    Debug.Log("add eleemtn at " + _target.DaysComing[i]);
                    _target.DaysComingData.Add(_target.DaysComing[i], _target.InteractionsData[i]);
                }
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _target.DrinkType.Count; i++)
            {
                if (!_target.DrinkEffects.ContainsKey(_target.DrinkType[i]))
                {
                    _target.DrinkEffects.Add(_target.DrinkType[i], _target.DrinkEffect[i]);
                    _target.DrinkEffectsFriendShip.Add(_target.DrinkType[i], _target.DrinkEffectFriendShip[i]);
                    Debug.Log("Added " + _target.DrinkType[i] + " to the dictionary at " + _target.DrinkEffect[i]);
                }
            }
            for (int i = 0; i < _target.DaysComing.Count; i++)
            {
                Debug.Log("checking");
                if (!_target.DaysComingData.ContainsKey(_target.DaysComing[i]))
                {
                    Debug.Log("add eleemtn");
                    _target.DaysComingData.Add(_target.DaysComing[i], _target.InteractionsData[i]);
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
            background.normal.background = MakeBackgroundTexture(1, 1, Color.cyan);
            GUI.backgroundColor = Color.cyan;
            GUILayout.Space(25);
            if (GUILayout.Button("Add Days Coming", background))
            {
                _target.DaysComing.Add(0);
                _target.InteractionsData.Add(new DayInteractions());
            }
            GUI.backgroundColor = baseColor;
            for(int i = 0; i < _target.DaysComing.Count; i++)
            {
                GUI.backgroundColor = baseColor;
                EditorGUILayout.Space(20);
                GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Day : ", GUILayout.Width(30));
                _target.DaysComing[i] = EditorGUILayout.IntSlider(_target.DaysComing[i], 0, 7, GUILayout.Width(500));
                background.normal.background = MakeBackgroundTexture(1, 1, Color.red);

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();

                DayInteractions interactions = _target.DaysComingData[_target.DaysComing[i]];
                EditorGUILayout.LabelField("Interactions Before Coming : ", GUILayout.Width(175));
                interactions.InteractionsBeforeComing = EditorGUILayout.IntField(interactions.InteractionsBeforeComing, GUILayout.Width(50));
                GUILayout.Space(15);
                EditorGUILayout.LabelField("Interactions Before Leaving : ", GUILayout.Width(175));
                interactions.InteractionsBeforeLeaving = EditorGUILayout.IntField(interactions.InteractionsBeforeLeaving, GUILayout.Width(50));
                _target.InteractionsData[i] = interactions;

                EditorGUILayout.EndHorizontal();
                GUI.backgroundColor = Color.red;
                if (GUILayout.Button("Remove Item", background, GUILayout.Width(125)))
                {
                    _target.DaysComing.RemoveAt(i);
                    _target.InteractionsData.RemoveAt(i);
                    _target.DaysComingData.Remove(_target.DaysComing[i]);
                }
            }
            background.normal.background = MakeBackgroundTexture(1, 1, Color.green);
            GUI.backgroundColor = Color.green;
            GUILayout.Space(25);
            if (GUILayout.Button("Add Drinks Effects", background))
            {
                _target.DrinkEffect.Add(0);
                _target.DrinkType.Add(DrinkType.Cofee);
                _target.DrinkEffectFriendShip.Add(0);
            }
            GUI.backgroundColor = baseColor;
            GUILayout.Space(20);
            for (int i = 0; i < _target.DrinkEffect.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Drink Type : ", GUILayout.Width(75));
                _target.DrinkType[i] = (DrinkType)EditorGUILayout.EnumPopup(_target.DrinkType[i]);
                EditorGUILayout.Space(25);

                EditorGUILayout.LabelField("DrinkScale :", GUILayout.Width(75));
                _target.DrinkEffect[i] = EditorGUILayout.IntField(_target.DrinkEffect[i], GUILayout.Width(50));
                if(_target.DrinkEffect[i] < 0) _target.DrinkEffect[i] = 0;
                else if(_target.DrinkEffect[i] > 100) _target.DrinkEffect[i] = 100;

                EditorGUILayout.Space(25);
                EditorGUILayout.LabelField("FriendScale :", GUILayout.Width(75));
                _target.DrinkEffectFriendShip[i] = EditorGUILayout.IntField(_target.DrinkEffectFriendShip[i], GUILayout.Width(50));
                if(_target.DrinkEffectFriendShip[i] < 0) _target.DrinkEffectFriendShip[i] = 0;
                else if (_target.DrinkEffectFriendShip[i] > 100) _target.DrinkEffectFriendShip[i] = 100;

                background.normal.background = MakeBackgroundTexture(1, 1, Color.red);
                GUI.backgroundColor = Color.red;

                EditorGUILayout.EndHorizontal();
                if (GUILayout.Button("Remove Item", background, GUILayout.Width(125)))
                {
                    _target.DrinkEffect.RemoveAt(i);
                    _target.DrinkType.RemoveAt(i);
                    _target.DrinkEffects.Remove(_target.DrinkType[i]);
                }
                GUI.backgroundColor = baseColor;
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
