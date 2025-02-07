using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace CREMOT.DialogSystem
{

    public class DialogueController : MonoBehaviour
    {
        #region Fields
        [Header("Init Parameters")]
        [SerializeField] private bool _autoInit;

        [SerializeField] private bool _autoStartDialog;
        private bool _dialogStarted;

        [Space(20)]

        [Header("Associated Dialogue Graph")]
        [SerializeField] private DialogueGraphSO _dialogueGraphSO;

        public DialogueGraphSO DialogueGraphSO { get => _dialogueGraphSO; set => _dialogueGraphSO = value; }

        private string _currentNodeId = "";
        private string _currentDialogueId = "";

        private DialogueNodeSO _currentDialogueNodeSO;

        [SerializeField] private List<DialogueGraphSO> _allDialogueGraph;

        #endregion


        #region Delegates
        [Space(20)]

        [Header("Dialogue / Choices Events")]
        [Space(5)]
        public UnityEvent OnDialogueUpdatedUnity;
        public event Action<string> OnDialogueUpdated;

        [Space(5)]

        public UnityEvent OnChoiceUpdatedUnity;


        public event Action<List<string>, int> OnChoiceUpdated;

        #endregion


        private void Start()
        {
            if (_autoInit)
            {

                if (DialogueInventory.Instance != null)
                {
                    DialogueInventory.Instance.OnUpdatedDialogueInventory += RefreshChoicesButton;
                }

                Init();
            }
        }
        private void OnDestroy()
        {
            if (DialogueInventory.Instance != null)
            {
                DialogueInventory.Instance.OnUpdatedDialogueInventory -= RefreshChoicesButton;
            }
        }

        #region Init
        public void Init()
        {
            _currentNodeId = GetNodeIdEntryPoint();
            _currentDialogueId = GetDialogueIdEntryPoint();

            if (_autoStartDialog)
            {
                StartDialog();
            }
        }
        public void StartDialog()
        {
            if (_dialogStarted) return;

            _dialogStarted = true;
            SelectChoice(0);
        }

        public void SwitchDialogGraph(int dialogGraphId)
        {
            if (dialogGraphId >= _allDialogueGraph.Count) return;

            _dialogueGraphSO = _allDialogueGraph[dialogGraphId];

            Init();

            StartDialog();

            Debug.LogWarning("SwitchToOtherGraph");
        }

        private string GetNodeIdEntryPoint()
        {
            if (DialogueGraphSO == null) return "";
            if (DialogueGraphSO.Nodes == null) return "";

            foreach (DialogueNodeSO node in DialogueGraphSO.Nodes)
            {
                if (node == null) continue;

                if (node.entryPoint)
                {
                    return node.id;
                }
            }

            return "";
        }
        private string GetDialogueIdEntryPoint()
        {
            if (DialogueGraphSO == null) return "";
            if (DialogueGraphSO.Nodes == null) return "";

            foreach (DialogueNodeSO node in DialogueGraphSO.Nodes)
            {
                if (node == null) continue;

                if (node.entryPoint)
                {
                    return node.dialogueId;
                }
            }

            return "";
        }
        private void RefreshChoicesButton()
        {
            if (_currentDialogueNodeSO == null) return;

            NotifyChoiceChange(_currentDialogueNodeSO.outputPortsChoiceId, _currentDialogueNodeSO.outputPorts);
        }
        #endregion

        #region Continue
        public void Continue()
        {
            if (_currentDialogueNodeSO == null) return;

            if (_currentDialogueNodeSO.outputPorts == null) return;
            if (_currentDialogueNodeSO.outputPorts.Count > 1) return;

            SelectChoice(0);
        }

        #endregion

        #region Selection Choice
        public void SelectChoice(int choiceId)
        {
            DialogueNodeSO nextDialogueNode = GetNextDialogueNodeByChoiceId(choiceId);

            if (nextDialogueNode == null) return;

            string nextNodeId = nextDialogueNode.id;
            string nextDialogueId = nextDialogueNode.dialogueId;

            _currentDialogueNodeSO = nextDialogueNode;

            if (!string.IsNullOrEmpty(nextNodeId))
            {
                if (!string.IsNullOrEmpty(nextDialogueId))
                {
                    _currentDialogueId = nextDialogueId;
                }

                _currentNodeId = nextNodeId;
                //Debug.Log($"From ID {_currentDialogueId} to ID {nextDialogueId}");
            }
            else
            {
                Debug.LogWarning("No valid dialogue found for this choice : " + nextDialogueId);
            }


            NotifyDialogueChange(_currentDialogueId);

            NotifyChoiceChange(nextDialogueNode.outputPortsChoiceId, nextDialogueNode.outputPorts);

            ApplyNodeAssociatedFunctions(_currentDialogueNodeSO);
        }

        #endregion

        #region Notify Dialogue / Choices changes
        private void NotifyDialogueChange(string dialogueId)
        {
            OnDialogueUpdated?.Invoke(dialogueId);
            OnDialogueUpdatedUnity?.Invoke();
        }
        private void NotifyChoiceChange(List<string> choicesText, List<string> outputPortGuid)
        {
            int originalChoicesCount = choicesText.Count;

            var availableChoices = new List<string>();

            //foreach (var choiceText in choicesText)
            for (int i = 0; i < choicesText.Count; ++i)
            {
                if (i >= outputPortGuid.Count) continue;

                var choiceText = choicesText[i];
                var choiceGuid = outputPortGuid[i];

                if (IsChoiceAvailable(choiceGuid))
                {
                    availableChoices.Add(choiceText);
                    //Debug.Log("Choice availbable");
                }
                else
                {
                    availableChoices.Add(null);
                }
            }

            OnChoiceUpdated?.Invoke(availableChoices, originalChoicesCount);
            //OnChoiceUpdated?.Invoke(choicesText);
            OnChoiceUpdatedUnity?.Invoke();
        }

        #endregion

        #region Condition Check
        private bool IsChoiceAvailable(string choiceTextId)
        {
            DialogueNodeSO currentNode = DialogueGraphSO.Nodes.FirstOrDefault(node => node.id == _currentNodeId);
            if (currentNode == null)
            {
                //Debug.Log("Break at current node null");
                return false;
            }

            PortCondition condition = currentNode.portConditions.FirstOrDefault(cond => cond.portId == choiceTextId);
            if (condition == null)
            {
                //Debug.Log("Pas de condition -> Toujours dispo");
                return true; // Pas de condition = toujours disponible
            }
            // V�rifier si toutes les conditions sont remplies
            foreach (var cond in condition.conditions)
            {
                if (!CheckCondition(cond))
                {
                    //Debug.Log("ne verifie pas les conditions -> pas dispo");
                    return false; // Une condition �choue
                }
            }
            return true; // Toutes les conditions sont remplies
        }

        private List<int> GetFirstChoiceAvailableIndex(string choiceTextId)
        {
            List<int> result = new List<int>();
            DialogueNodeSO currentNode = DialogueGraphSO.Nodes.FirstOrDefault(node => node.id == _currentNodeId);
            if (currentNode == null)
            {
                Debug.LogWarning("ICI");
                //Debug.Log("Break at current node null");
                return result;
            }

            PortCondition condition = currentNode.portConditions.FirstOrDefault(cond => cond.portId == choiceTextId);
            if (condition == null)
            {
                Debug.LogWarning("ICI");
                //Debug.Log("Pas de condition -> Toujours dispo");
                result.Add(0);
                return result; // Pas de condition = toujours disponible
            }
            // V�rifier si toutes les conditions sont remplies
            for (int i = 0; i < condition.conditions.Count; ++i)
            {
                var tempCond = condition.conditions[i];

                if (CheckCondition(tempCond))
                {
                    result.Add(i);
                }
            }
            Debug.LogWarning("ICI");
            return result; // Toutes les conditions sont remplies
        }

        private bool CheckCondition(ConditionSO condition)
        {
            if (DialogueInventory.Instance == null) return false;

            switch (condition.conditionType)
            {
                case EConditionType.REACHOREQUAL:
                    return DialogueInventory.Instance.HasItem(condition.requiredItem, condition.requiredQuantity);
                case EConditionType.UNDER:
                    return DialogueInventory.Instance.IsUnderItem(condition.requiredItem, condition.requiredQuantity);
                case EConditionType.STRICTLYEQUAL:
                    return DialogueInventory.Instance.HasExactlyItem(condition.requiredItem, condition.requiredQuantity);
                default:
                    break;
            }

            return false;
        }

        #endregion

        #region Apply node associated Functions

        private void ApplyNodeAssociatedFunctions(DialogueNodeSO nodeData)
        {
            if (nodeData == null) return;

            foreach (var callFunData in nodeData.callFunctions)
            {
                if (callFunData == null) continue;

                if (string.IsNullOrEmpty(callFunData.gameObjectPersistantGUID) || string.IsNullOrEmpty(callFunData.methodName)) continue;

                GameObject tempGameObject = null;

                if (PersistentGUIDManager.Instance != null)
                {
                    tempGameObject = PersistentGUIDManager.Instance.GetObjectByGUID(callFunData.gameObjectPersistantGUID);
                }

                if (tempGameObject == null) continue;

                // Split the method name to get the component type and method name  "componentName.methodName"
                var methodParts = callFunData.methodName.Split('.');
                if (methodParts.Length != 2) continue;

                var componentName = methodParts[0];
                var methodName = methodParts[1];

                // Get the component
                var component = tempGameObject.GetComponent(componentName);
                if (component == null) continue;

                // Get the method
                var method = component.GetType().GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
                if (method == null) continue;

                // Prepare parameters
                var parameterInfos = method.GetParameters();
                var parameters = new object[parameterInfos.Length];
                for (int i = 0; i < parameterInfos.Length; i++)
                {
                    var paramType = parameterInfos[i].ParameterType;
                    var paramValue = callFunData.parametersValues[i];

                    // Convert the parameter to the correct type
                    parameters[i] = Convert.ChangeType(paramValue, paramType);
                }

                // Invoke the method
                method.Invoke(component, parameters);
            }
        }

        #endregion


        #region Parcours Graph Data
        private DialogueNodeSO GetNextDialogueNodeByChoiceId(int choiceId)
        {
            if (DialogueGraphSO.Nodes == null) return null;
            if (DialogueGraphSO.Edges == null) return null;
            if (string.IsNullOrEmpty(_currentNodeId)) return null;
            if (choiceId < 0) return null;



            // Trouve le n�ud actuel
            foreach (DialogueNodeSO node in DialogueGraphSO.Nodes)
            {
                if (node.id == _currentNodeId)
                {
                    // R�cup�re l'edge correspondant au choix
                    DialogueEdgeSO edge = GetNextEdgeByChoiceId(choiceId, node.id);
                    if (edge == null) return null; // Pas d'edge trouv� pour ce choix

                    // Trouve le n�ud cible
                    foreach (DialogueNodeSO targetNode in DialogueGraphSO.Nodes)
                    {
                        if (targetNode.id == edge.toNodeId)
                        {
                            return targetNode; // Retourne l'ID du dialogue suivant
                        }
                    }
                }
            }

            return null; // Aucun dialogue trouv�
        }

        private DialogueEdgeSO GetNextEdgeByChoiceId(int choiceId, string currentNodeId)    // Retourne l'edge qui fait le lien vers le node cible
        {
            foreach (DialogueEdgeSO edge in DialogueGraphSO.Edges)
            {
                if (edge.fromNodeId == currentNodeId && edge.fromPortIndex == choiceId) // Si depuis bon node && bon choix
                {
                    //Debug.Log($"Found edge: FromNodeId={edge.fromNodeId}, ToNodeId={edge.toNodeId}, ChoiceId={choiceId}");
                    return edge;
                }
            }

            Debug.LogWarning($"No edge found for CurrentNodeId={currentNodeId}, ChoiceId={choiceId}");
            return null;
        }
        #endregion
    }
}
