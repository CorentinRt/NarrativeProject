#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine.Events;
using System.Reflection;

namespace CREMOT.DialogSystem
{
    public class DialogueNode : Node
    {
        private readonly Vector2 _defaultNodeSize = new Vector2(150, 200);


        public string DialogueText; // Match DialogueId -> DialogueNodeSO
        public string GIUD; // Match id -> DialogueNodeSO

        public bool EntryPoint = false;

        public List<string> OutputPorts = new List<string>();
        public List<string> OutputPortsChoiceId = new List<string>();

        public List<PortCondition> PortConditions = new List<PortCondition>();


        public List<NodeCallFunctionContainer> nodeEventsContainers = new List<NodeCallFunctionContainer>();


        // Synchronize with DialogueNodeSO
        public void InitializeFromSO(DialogueNodeSO nodeSO)
        {
            GIUD = nodeSO.id;
            DialogueText = nodeSO.dialogueId;
            this.SetPosition(new Rect(nodeSO.position, _defaultNodeSize));
            EntryPoint = nodeSO.entryPoint;
            OutputPorts = new List<string>(nodeSO.outputPorts);
            OutputPortsChoiceId = new List<string>(nodeSO.outputPortsChoiceId);

            PortConditions = new List<PortCondition>(nodeSO.portConditions);
        }

        public DialogueNodeSO ToSO()
        {
            return new DialogueNodeSO
            {
                id = GIUD,
                dialogueId = DialogueText,
                title = title,
                position = GetPosition().position,
                entryPoint = EntryPoint,
                outputPorts = outputContainer.Query<Port>().ToList().Select(port => port.name).ToList(),
                outputPortsChoiceId = outputContainer.Query<Port>().ToList().Select(port => port.portName).ToList(),

                portConditions = PortConditions,

                callFunctions = nodeEventsContainers.Select(container => new CallFunctionData
                {
                    gameObjectPersistantGUID = (container.CallFunctionField.value as GameObject).GetComponent<PersistentGUID>().GUID,

                    methodName = container.MethodPopupField.value,

                    parametersValues = container.MethodParametersValues,

                    parametersName = container.MethodParametersNames
                }).ToList()
            };
        }
    }

    public class NodeCallFunctionContainer : VisualElement
    {
        #region Fields
        public ObjectField CallFunctionField;

        public PopupField<string> MethodPopupField;
        public List<string> methodNames = new List<string>();

        public Button RemoveCallFunctionFieldBtn;

        public List<TextField> parameterFields = new List<TextField>();
        public List<string> MethodParametersValues = new List<string>();
        public List<string> MethodParametersNames = new List<string>();
        #endregion


        #region Construct
        public NodeCallFunctionContainer(DialogueNode node)
        {
            RemoveCallFunctionFieldBtn = new Button(() =>
            {
                this.RemoveCallFunctionField(node);
            });
            RemoveCallFunctionFieldBtn.text = "Remove Call Function Field";
            RemoveCallFunctionFieldBtn.style.backgroundColor = new Color(0.7f, 0.3f, 0.3f);

            CallFunctionField = new ObjectField("Event Field")
            {
                
            };
            CallFunctionField.RegisterValueChangedCallback(evt =>
            {
                Debug.Log("CallFunctionField changed");
                UpdateMethodPopup(evt.newValue as GameObject);
            });

            CallFunctionField.objectType = typeof(GameObject);

            MethodPopupField = new PopupField<string>("Selected Method", methodNames, 0);
            MethodPopupField.RegisterValueChangedCallback(evt =>
            {
                Debug.Log("MethodPopupField changed");
                UpdateParameterFields();
            });

            this.Add(RemoveCallFunctionFieldBtn);
            this.Add(CallFunctionField);
            this.Add(MethodPopupField);
            node.mainContainer.Add(this);

            //node.mainContainer.Add(RemoveCallFunctionFieldBtn);
            //node.mainContainer.Add(CallFunctionField);
            //node.mainContainer.Add(MethodPopupField);
        }
        #endregion

        #region Popup
        private void UpdateMethodPopup(GameObject selectedObject)
        {
            methodNames.Clear();

            if (selectedObject != null)
            {
                var components = selectedObject.GetComponents<MonoBehaviour>();
                foreach (var component in components)
                {
                    var methods = component.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                    foreach (var method in methods)
                    {
                        methodNames.Add($"{component.GetType().Name}.{method.Name}");
                    }
                }
            }
            MethodPopupField.choices = methodNames;
            MethodPopupField.index = 0;
        }
        #endregion

        #region parameterFields
        private void UpdateParameterFields()
        {
            foreach (var field in parameterFields)
            {
                this.Remove(field);
            }
            parameterFields.Clear();
            MethodParametersValues.Clear();
            MethodParametersNames.Clear();

            var selectedMethod = MethodPopupField.value;
            var parts = selectedMethod.Split('.');
            if (parts.Length != 2) return;

            var selectedObject = CallFunctionField.value as GameObject;
            if (selectedObject != null)
            {
                var component = selectedObject.GetComponent(parts[0]);
                if (component != null)
                {
                    var method = component.GetType().GetMethod(parts[1]);
                    if (method != null)
                    {
                        var parameters = method.GetParameters();
                        foreach (var param in parameters)
                        {
                            var paramField = new TextField(param.Name)
                            {
                                
                            };

                            paramField.RegisterValueChangedCallback(evt =>
                            {
                                var index = parameterFields.IndexOf(paramField);
                                MethodParametersValues[index] = evt.newValue;
                            });

                            parameterFields.Add(paramField);
                            MethodParametersValues.Add(string.Empty);
                            MethodParametersNames.Add(param.Name);

                            Debug.Log(paramField);

                            this.Add(paramField);
                            this.MarkDirtyRepaint();
                        }
                    }
                }
            }
        }
        #endregion

        #region Remove CallFunction

        private void RemoveCallFunctionField(DialogueNode node)
        {
            node.mainContainer.Remove(this);

            node.nodeEventsContainers.Remove(this);
        }

        #endregion
    }
}
#endif
