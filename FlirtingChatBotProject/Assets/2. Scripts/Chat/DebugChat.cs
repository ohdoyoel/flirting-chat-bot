using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VisualChatManager))]
public class DebugChat : Editor
{
    VisualChatManager visualChatManager;
    string text;

    void OnEnable()
    {
        visualChatManager = target as VisualChatManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        text = EditorGUILayout.TextArea(text);

        if (GUILayout.Button("보내기", GUILayout.Width(60)) && text.Trim() != "")
        {
            visualChatManager.VisualizeChat(2, text, "유붕이", null);
            text = "";
            GUI.FocusControl(null);
        }

        if (GUILayout.Button("받기", GUILayout.Width(60)) && text.Trim() != "")
        {
            visualChatManager.VisualizeChat(1, text, "유순이", Resources.Load<Texture2D>("Assets/1. Resources/Images/profile_img.png"));
            text = "";
            GUI.FocusControl(null);
        }

        EditorGUILayout.EndHorizontal();
    }
}
