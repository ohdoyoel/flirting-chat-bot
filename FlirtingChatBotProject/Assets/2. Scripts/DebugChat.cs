using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChatManager))]
public class DebugChat : Editor
{
    ChatManager chatManager;
    string text;

    void OnEnable()
    {
        chatManager = target as ChatManager;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        EditorGUILayout.BeginHorizontal();
        text = EditorGUILayout.TextArea(text);

        if (GUILayout.Button("보내기", GUILayout.Width(60)) && text.Trim() != "")
        {
            chatManager.VisualizeChat(true, text, "유붕이", null);
            text = "";
            GUI.FocusControl(null);
        }

        if (GUILayout.Button("받기", GUILayout.Width(60)) && text.Trim() != "")
        {
            chatManager.VisualizeChat(false, text, "유순이", Resources.Load<Texture2D>("Assets/1. Resources/Images/profile_img.png"));
            text = "";
            GUI.FocusControl(null);
        }

        EditorGUILayout.EndHorizontal();
    }
}
