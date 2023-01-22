using UnityEngine;
using UnityEditor;

using Caeca.TransformMovement;

namespace Caeca.CustomEditors
{
    /// <summary>
    /// Editor for followTransform. Shows bool arrays in understandable way.
    /// </summary>
    [CustomEditor(typeof(FollowTransform))]
    public class FollowTransformEditor : Editor
    {
        private FollowTransform followTransform;

        public override void OnInspectorGUI()
        {
            followTransform = (FollowTransform)target;

            SerializedObject serializedObject = new SerializedObject(followTransform);
            serializedObject.Update();

            DrawDefaultInspector();
            Draw(serializedObject);
        }

        private int toggleWidth = 20;
        private void Draw(SerializedObject sObject)
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.LabelField("Follow setings", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Follow Position", EditorStyles.boldLabel, GUILayout.Width(150));
            EditorGUILayout.LabelField("X", EditorStyles.boldLabel, GUILayout.Width(toggleWidth));
            followTransform.followPosition[0] = EditorGUILayout.Toggle(followTransform.followPosition[0], GUILayout.Width(toggleWidth));
            EditorGUILayout.LabelField("Y", EditorStyles.boldLabel, GUILayout.Width(toggleWidth));
            followTransform.followPosition[1] = EditorGUILayout.Toggle(followTransform.followPosition[1], GUILayout.Width(toggleWidth));
            EditorGUILayout.LabelField("Z", EditorStyles.boldLabel, GUILayout.Width(toggleWidth));
            followTransform.followPosition[2] = EditorGUILayout.Toggle(followTransform.followPosition[2], GUILayout.Width(toggleWidth));

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Follow Rotation", EditorStyles.boldLabel, GUILayout.Width(150));
            EditorGUILayout.LabelField("X", EditorStyles.boldLabel, GUILayout.Width(toggleWidth));
            followTransform.followRotation[0] = EditorGUILayout.Toggle(followTransform.followRotation[0], GUILayout.Width(toggleWidth));
            EditorGUILayout.LabelField("Y", EditorStyles.boldLabel, GUILayout.Width(toggleWidth));
            followTransform.followRotation[1] = EditorGUILayout.Toggle(followTransform.followRotation[1], GUILayout.Width(toggleWidth));
            EditorGUILayout.LabelField("Z", EditorStyles.boldLabel, GUILayout.Width(toggleWidth));
            followTransform.followRotation[2] = EditorGUILayout.Toggle(followTransform.followRotation[2], GUILayout.Width(toggleWidth));

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }
    }
}
