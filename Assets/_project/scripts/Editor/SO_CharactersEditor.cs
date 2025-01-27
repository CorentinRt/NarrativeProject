using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;

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
            base.OnInspectorGUI();
            Color baseColor = GUI.backgroundColor;
            GUIStyle background = new GUIStyle(GUI.skin.button);
            background.normal.background = MakeBackgroundTexture(1, 1, Color.green);
            GUI.backgroundColor = Color.green;
            if (GUILayout.Button("Add Character Data", background))
            {
                PopupWindow.ShowWindow();
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
