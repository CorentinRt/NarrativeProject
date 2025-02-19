#if UNITY_EDITOR
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CREMOT.UIAnimatorDotween
{
    [CustomEditor(typeof(UIAnimator))]
    public class UIAnimatorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            UIAnimator animator = (UIAnimator)target;

            if (animator.Animations == null || animator.Animations.Length == 0)
            {
                if (GUILayout.Button("Add Animation"))
                {
                    animator.Animations = new UIAnimator.AnimationSettings[1];
                    animator.Animations[0] = new UIAnimator.AnimationSettings();
                }
            }

            if (animator.Animations != null)
            {
                for (int i = 0; i < animator.Animations.Length; i++)
                {
                    EditorGUILayout.LabelField($"Animation {i + 1}", EditorStyles.boldLabel);

                    animator.Animations[i].AnimationType = (UIAnimator.EAnimationType)EditorGUILayout.EnumPopup("Type", animator.Animations[i].AnimationType);
                    animator.Animations[i].Duration = EditorGUILayout.FloatField("Duration", animator.Animations[i].Duration);
                    animator.Animations[i].Ease = (Ease)EditorGUILayout.EnumPopup("Ease", animator.Animations[i].Ease);

                    if (animator.Animations[i].AnimationType == UIAnimator.EAnimationType.MOVETO_2)
                    {
                        animator.Animations[i].TargetMove = (Transform)EditorGUILayout.ObjectField("Target Move", animator.Animations[i].TargetMove, typeof(Transform), true);
                    }

                    if (animator.Animations[i].AnimationType == UIAnimator.EAnimationType.SCALETO_3)
                    {
                        animator.Animations[i].TargetScale = EditorGUILayout.Vector3Field("Target Scale", animator.Animations[i].TargetScale);
                    }

                    animator.Animations[i].PlayOnStart = EditorGUILayout.Toggle("Play On Start", animator.Animations[i].PlayOnStart);

                    if (animator.Animations[i].AnimationType == UIAnimator.EAnimationType.COLORTO_4)
                    {
                        animator.Animations[i].TargetColor = EditorGUILayout.ColorField("Target Color", animator.Animations[i].TargetColor);
                    }

                    if (animator.Animations[i].AnimationType == UIAnimator.EAnimationType.IDLE_INFINITE_5)
                    {
                        animator.Animations[i].IdleAmplitude = EditorGUILayout.FloatField("Idle Amplitude", animator.Animations[i].IdleAmplitude);
                        animator.Animations[i].IdleRandomOffsetDuration = EditorGUILayout.FloatField("Idle Random Offset Duration", animator.Animations[i].IdleRandomOffsetDuration);
                    }

                    if (animator.Animations[i].AnimationType == UIAnimator.EAnimationType.BOBBING_ONCE_6)
                    {
                        animator.Animations[i].BobbingScale = EditorGUILayout.Vector3Field("Bobbing Scale", animator.Animations[i].BobbingScale);
                    }

                    // ---------------- Event field ---------------------
                    SerializedObject serializedObjectStarted = new SerializedObject(animator);
                    SerializedProperty animationsArrayStarted = serializedObjectStarted.FindProperty("_animations");
                    SerializedProperty currentAnimationStarted = animationsArrayStarted.GetArrayElementAtIndex(i);
                    SerializedProperty onAnimationStarted = currentAnimationStarted.FindPropertyRelative("OnAnimationStarted");
                    EditorGUILayout.PropertyField(onAnimationStarted, new GUIContent("OnAnimationStarted"));
                    // ------------------------------------------------------
                    // ---------------- Event field ---------------------
                    SerializedObject serializedObject = new SerializedObject(animator);
                    SerializedProperty animationsArray = serializedObject.FindProperty("_animations");
                    SerializedProperty currentAnimation = animationsArray.GetArrayElementAtIndex(i);
                    SerializedProperty onAnimationFinished = currentAnimation.FindPropertyRelative("OnAnimationFinished");
                    EditorGUILayout.PropertyField(onAnimationFinished, new GUIContent("OnAnimationFinished"));
                    // ------------------------------------------------------


                    serializedObjectStarted.ApplyModifiedProperties();
                    serializedObject.ApplyModifiedProperties();

                    if (GUILayout.Button("Remove Animation"))
                    {
                        var tempList = new System.Collections.Generic.List<UIAnimator.AnimationSettings>(animator.Animations);
                        tempList.RemoveAt(i);
                        animator.Animations = tempList.ToArray();
                    }

                    EditorGUILayout.Space();
                }

                if (GUILayout.Button("Add Animation"))
                {
                    var tempList = new System.Collections.Generic.List<UIAnimator.AnimationSettings>(animator.Animations);
                    tempList.Add(new UIAnimator.AnimationSettings());
                    animator.Animations = tempList.ToArray();
                }
            }

            if (GUILayout.Button("Play All"))
            {
                animator.PlayAllAnimations();
            }

            EditorUtility.SetDirty(target);
        }
    }
}
#endif