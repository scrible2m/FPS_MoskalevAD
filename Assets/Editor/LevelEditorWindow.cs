using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.IO;

public class LevelEditorWindow : EditorWindow
{
    
    public List<Object> _sciFi = new List<Object>();
    

    [MenuItem("MyTools/LevelEditor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelEditorWindow), false, "LevelEditor");
    }

    private void OnGUI()
    {



        

        string[] temp = AssetDatabase.GetSubFolders("Assets/LevelEditorPrefabs");
        foreach (string _folder in temp)
        {
            EditorGUILayout.LabelField(_folder.Substring(26) + " Prefabs", EditorStyles.boldLabel);
            Object[] _prefBase = AssetDatabase.LoadAllAssetsAtPath(_folder);
            EditorGUILayout.LabelField(_folder + " Prefabs", EditorStyles.boldLabel);
            
            foreach(Object _pref in _prefBase)
            {
                Debug.Log("!!!");
            }





        }

        
    }
   
}
