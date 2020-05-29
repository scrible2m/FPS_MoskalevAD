using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;

public class LevelEditorWindow : EditorWindow
{

    List<Folders> _folders = new List<Folders>();
    bool _reset = true;
    
    int j = 0;


    [MenuItem("MyTools/LevelEditor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelEditorWindow), false, "LevelEditor");
    }



    private void OnGUI()
    {




        if (_reset)
        {
            
            _folders.Clear();
            string[] tempReset = AssetDatabase.GetSubFolders("Assets/LevelEditorPrefabs");





            foreach (string _folder in tempReset)

            { 


                DirectoryInfo dirInfo = new DirectoryInfo(_folder);
                FileInfo[] fileInf = dirInfo.GetFiles("*.prefab");
               
                
                foreach (FileInfo fileInfo in fileInf)
                {
                    
                    string fullPath = fileInfo.FullName.Replace(@"\", "/");
                    string assetPath = "Assets" + fullPath.Replace(Application.dataPath, "");
                    GameObject prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;

                    if (prefab != null)
                    {
                        _folders.Add(new Folders() { _GO = prefab, Name = _folder.Substring(26), Count = fileInf.Length }) ;

                    }

                   
                }
               
            }
        }



        j = 0;
        foreach (Folders _goList in _folders)

        {
            if (j==0)
            {
                EditorGUILayout.LabelField(_goList.Name + " Prefabs", EditorStyles.boldLabel);
                j = 0;
            }
           
           if (j!=_goList.Count)
            {
                
                EditorGUILayout.LabelField(_goList.Name + " Prefabs" , EditorStyles.boldLabel);
                j = 0;
                
            }
            j = _goList.Count;
            

            if (_goList._GO != null)
            {
                
                   
                        if (GUILayout.Button(_goList._GO.name))
                        {

                        }
                
            }

            
        }



        _reset = false;

        EditorGUILayout.LabelField("_RESET", EditorStyles.boldLabel);
        if (GUILayout.Button("Reset"))
        {
            _reset = true;
            j = 0;
        }

        
    }

    public class Folders
    {
        public GameObject _GO;
        public string Name;
        public int Count;
        
    }
   
}
