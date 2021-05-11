// (c) Copyright Cleverous 2017. All rights reserved.

using UnityEngine;
using UnityEditor;

namespace Cleverous.Stats.Editors
{
    [CustomEditor(typeof(StatPreset))]
    public class EStatPreset : Editor
    {
        protected StatPreset X;
        public SerializedProperty StatAsset;

        public virtual void OnEnable()
        {
            X = (StatPreset) target;
            if (X.Stats == null) X.Stats = StatUtility.BaseCharacterStats();
            StatAsset = serializedObject.FindProperty("Stats");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.Space();
            if (GUILayout.Button("Reset")) X.Stats = StatUtility.BaseCharacterStats();
            EStatEditorTools.DrawStatGroup(StatAsset);

            EditorGUILayout.Space();
            serializedObject.ApplyModifiedProperties();
        }
    }
}