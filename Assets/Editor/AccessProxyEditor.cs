using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AccessProxy))]
public class AccessProxyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AccessProxy obj = (AccessProxy)target;

        if (obj.names == null) obj.names = new();
        if (obj.objects == null) obj.objects = new();

        for (int i = 0; i < obj.names.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();

            obj.names[i] = EditorGUILayout.TextField(obj.names[i]);
            obj.objects[i] = (GameObject)EditorGUILayout.ObjectField(obj.objects[i], typeof(GameObject), true);

            if (GUILayout.Button("up") && i > 0)
            {
                string tempStr = obj.names[i];
                GameObject tempObj = obj.objects[i];

                obj.names.RemoveAt(i);
                obj.objects.RemoveAt(i);

                obj.names.Insert(i - 1, tempStr);
                obj.objects.Insert(i - 1, tempObj);
            }
            
            if (GUILayout.Button("down") && i < obj.names.Count - 1)
            {
                string tempStr = obj.names[i];
                GameObject tempObj = obj.objects[i];

                obj.names.RemoveAt(i);
                obj.objects.RemoveAt(i);

                obj.names.Insert(i + 1, tempStr);
                obj.objects.Insert(i + 1, tempObj);
            }
            
            if (GUILayout.Button("-"))
            {
                obj.names.RemoveAt(i);
                obj.objects.RemoveAt(i);
            }

            EditorGUILayout.EndHorizontal();
        }

        if (GUILayout.Button("+"))
        {
            obj.names.Add("");
            obj.objects.Add(null);
        }
    }
}
