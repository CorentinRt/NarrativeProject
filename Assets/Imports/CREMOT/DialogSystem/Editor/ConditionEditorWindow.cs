#if UNITY_EDITOR
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CREMOT.DialogSystem
{
    public class ConditionEditorWindow : EditorWindow
    {
        private DialogueNode _node;
        private string _portId;

        private List<ConditionSO> _conditions;

        private Button _associatedEditCondBtn;

        public void Init(DialogueNode node, string portId, Button associatedBtn = null)
        {
            _associatedEditCondBtn = associatedBtn;

            _node = node;
            _portId = portId;

            var portCondition = _node.PortConditions.FirstOrDefault(cond => cond.portId == portId);
            if (portCondition == null)
            {
                portCondition = new PortCondition { portId = _portId };
                _node.PortConditions.Add(portCondition);
            }
            _conditions = portCondition.conditions;
        }

        private void OnGUI()
        {
            if (_conditions == null) return;

            GUILayout.Label("Conditions for port: " + _portId, EditorStyles.boldLabel);

            Debug.LogWarning("TestColor");

            if (_associatedEditCondBtn != null)
            {
                if (_conditions.Count <= 0)
                {
                    _associatedEditCondBtn.style.backgroundColor = new Color(0.3f, 0.7f, 0.5f);
                    _associatedEditCondBtn.style.color = Color.black;
                    _associatedEditCondBtn.text = "Add Conditions";
                }
                else
                {
                    _associatedEditCondBtn.style.backgroundColor = new Color(0.3f, 0.3f, 0.7f);
                    _associatedEditCondBtn.style.color = Color.white;
                    _associatedEditCondBtn.text = "Edit Conditions";
                }
            }

            for (int i = 0; i < _conditions.Count; i++)
            {
                GUILayout.BeginHorizontal();
                _conditions[i].requiredItem = EditorGUILayout.TextField("Item", _conditions[i].requiredItem);
                _conditions[i].requiredQuantity = EditorGUILayout.IntField("Quantity", _conditions[i].requiredQuantity);

                string[] conditionTypeList = new string[3];
                conditionTypeList[0] = "REACHOREQUAL";
                conditionTypeList[1] = "UNDER";
                conditionTypeList[2] = "STRICTLYEQUAL";
                _conditions[i].conditionType = (EConditionType)EditorGUILayout.Popup((int)_conditions[i].conditionType, conditionTypeList);

                if (GUILayout.Button("Remove"))
                {
                    _conditions.RemoveAt(i);
                }
                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Condition"))
            {
                _conditions.Add(new ConditionSO());
            }

            if (GUILayout.Button("Save"))
            {
                Close();
            }
        }
    }
}
#endif