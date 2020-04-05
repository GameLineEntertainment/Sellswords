using System.Collections.Generic;
using OldSellswords;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MissionGenerator))]
[CanEditMultipleObjects]
public class MissionGeneratorEditor : Editor
{
    private SerializedProperty _countEnemyProp;
    private SerializedProperty _arenasProp;
    private bool _isGeneratorMission;
    private bool _isClear;
    private MissionGenerator _mg;
    private void OnEnable()
    {
        _countEnemyProp = serializedObject.FindProperty("_countEnemy");
        _arenasProp = serializedObject.FindProperty("_arenas");
        _mg = target as MissionGenerator;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.HelpBox("Description", MessageType.Info);
        Space(5);

        Show(_arenasProp);
        Space(3);

        _isGeneratorMission = GUILayout.Button("Generator");

        if (_isGeneratorMission)
        {
            for (int i = 0; i < _arenasProp.arraySize; i++)
            {
                var arena = (Arena)_arenasProp.GetArrayElementAtIndex(i).objectReferenceValue;
                _mg.Generator(arena);
            }
        }

        Space(2);

        _isClear = GUILayout.Button("Clear");

        if (_isClear)
        {
            _mg.Clear();
        }

        serializedObject.ApplyModifiedProperties();

        void Space(int count)
        {
            for (int i = 0; i < count; i++)
            {
                EditorGUILayout.Space();
            }
        }
    }

    private void Show(SerializedProperty list, bool showListSize = true)
    {
        EditorGUILayout.PropertyField(list);
        EditorGUI.indentLevel += 1;
        if (list.isExpanded)
        {
            if (showListSize)
            {
                EditorGUILayout.PropertyField(list.FindPropertyRelative("Array.size"));
            }
            for (int i = 0; i < list.arraySize; i++)
            {
                EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
        }
        EditorGUI.indentLevel -= 1;
    }

    private void ProgressBar(float value, string label)
    {
        Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
        EditorGUI.ProgressBar(rect, value, label);
        EditorGUILayout.Space();
    }
}