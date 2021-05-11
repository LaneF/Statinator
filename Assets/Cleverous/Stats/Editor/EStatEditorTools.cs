// (c) Copyright Cleverous 2017. All rights reserved.

using UnityEngine;
using UnityEditor;

namespace Cleverous.Stats.Editors
{
    public class EStatEditorTools : Editor
    {
        public static void DrawStatGroup(SerializedProperty obj)
        {
            // Header ------------------------------ //
            float fw = EditorGUIUtility.fieldWidth;
            EditorGUIUtility.fieldWidth = 1;
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 20;
            EditorGUILayout.LabelField("Property", EditorStyles.boldLabel);
            EditorGUIUtility.labelWidth = 1;

            EditorGUILayout.LabelField("", "Base");
            EditorGUILayout.LabelField("", "Min");
            EditorGUILayout.LabelField("", "Max");
            EditorGUILayout.LabelField("", "Aff(A)");
            EditorGUILayout.LabelField("", "Aff(M)");
            EditorGUILayout.LabelField("", "Actual");
            EditorGUILayout.EndHorizontal();

            EditorGUIUtility.fieldWidth = fw;
            // ------------------------------------- //

            // Draw Loop --------------------------- //
            for (int i = 0; i < obj.arraySize; i++)
            {
                EditorGUIUtility.fieldWidth = 1;
                EditorGUILayout.BeginHorizontal();
                EditorGUIUtility.labelWidth = 20;
                EditorGUILayout.LabelField(((StatType)i).ToString(), EditorStyles.boldLabel);
                EditorGUIUtility.labelWidth = 1;

                SerializedProperty thisStat = obj.GetArrayElementAtIndex(i);

                SerializedProperty sBase = thisStat.FindPropertyRelative("_rBase");
                SerializedProperty sMin = thisStat.FindPropertyRelative("_rMin");
                SerializedProperty sMax = thisStat.FindPropertyRelative("_rMax");
                SerializedProperty sAff = thisStat.FindPropertyRelative("_rAffinity");
                SerializedProperty sAffMax = thisStat.FindPropertyRelative("_rMaxAffinity");
                SerializedProperty sActual = thisStat.FindPropertyRelative("_rActual");

                EditorGUILayout.PropertyField(sBase);
                EditorGUILayout.PropertyField(sMin);
                EditorGUILayout.PropertyField(sMax);
                EditorGUILayout.PropertyField(sAff);
                EditorGUILayout.PropertyField(sAffMax);
                EditorGUILayout.PropertyField(sActual);

                EditorGUILayout.EndHorizontal();
            }
            // ------------------------------------- //
        }
    }
}