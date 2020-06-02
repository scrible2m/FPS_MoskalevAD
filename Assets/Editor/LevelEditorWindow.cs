using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;

public class LevelEditorWindow : EditorWindow
{

    List<Folders> _folders = new List<Folders>();
    bool _reset = true;
    public GameObject _go;
    public Vector3 _levelEditPoint;
    bool _instatiateFlag;
    int j = 0;
    float deltaX = 0f;
    float deltaY = 0f;
    bool _spaceFlag;
    int _step = 1;


    [MenuItem("MyTools/LevelEditor")]
    public static void ShowWindow()
    {
        GetWindow(typeof(LevelEditorWindow), false, "LevelEditor");
    }



    private void OnGUI()
    {




        if (_reset)

            _levelEditPoint = GameObject.FindGameObjectWithTag("LevelEditPoint").transform.position;
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
                        _folders.Add(new Folders() { _GO = prefab, Name = _folder.Substring(26), Count = fileInf.Length });

                    }


                }

            }
        }



        j = 0;
        foreach (Folders _goList in _folders)

        {
            if (j == 0)
            {
                EditorGUILayout.LabelField(_goList.Name + " Prefabs", EditorStyles.boldLabel);
                j = 0;
            }

            if (j != _goList.Count)
            {

                EditorGUILayout.LabelField(_goList.Name + " Prefabs", EditorStyles.boldLabel);
                j = 0;

            }
            j = _goList.Count;


            if (_goList._GO != null)
            {


                if (GUILayout.Button(_goList._GO.name))
                {
                    if (_go != null)
                    {
                        Transform tempPos = _go.transform;
                        Quaternion tempRot = _go.transform.rotation;
                        _go = Instantiate(_goList._GO, tempPos.position, tempRot);
                        _go.transform.localScale = tempPos.localScale;
                    }
                    else 
                    {
                        _go = Instantiate(_goList._GO, _levelEditPoint, Quaternion.identity);
                    }
                    _instatiateFlag = true;
                }


            }

        }

            _go = EditorGUILayout.ObjectField("Prefab to Move", _go, typeof(GameObject), true)  as GameObject;
        _spaceFlag = EditorGUILayout.Toggle(_spaceFlag);

        _reset = false;

        EditorGUILayout.LabelField("Change Step", EditorStyles.boldLabel);
        _step = EditorGUILayout.IntSlider(_step, 1, 4);

        EditorGUILayout.LabelField("_RESET", EditorStyles.boldLabel);
        if (GUILayout.Button("Reset"))
        {
            _reset = true;
            j = 0;
        }
        EditorGUILayout.LabelField("Rotate", EditorStyles.boldLabel);
        if (GUILayout.Button("+15"))
        {
           if (_go != null)
            {
                _go.transform.Rotate(0, 15f, 0);
            }
        }
        
        if (GUILayout.Button("-15"))
        {
            if (_go != null)
            {
                _go.transform.Rotate(0, -15f, 0);
            }
        }

        if (_go != null)
        {
            Event e = Event.current;

            if(e.keyCode == (KeyCode.Space))
            {
                _spaceFlag = !_spaceFlag;
            }
           

            if (e.keyCode == (KeyCode.A))
            {
                if (!_spaceFlag)
                {
                    _go.transform.Translate(0.01f * _step, 0, 0);
                }
                else
                {
                    _go.transform.Rotate(0.5f * _step, 0,0);
                }
            }

            else if (e.keyCode == (KeyCode.D))
                
                {
                    if (!_spaceFlag)
                    {
                        _go.transform.Translate(-0.01f * _step, 0, 0);
                    }
                    else
                    {
                        _go.transform.Rotate(-0.5f * _step, 0,0);
                    }
                }
               

            else if (e.keyCode == (KeyCode.S))
            {
                if (!_spaceFlag)
                {
                    _go.transform.Translate(0, 0, 0.01f * _step);
                }
                else 
                {
                    _go.transform.Rotate( 0, 0 ,0.5f * _step);
                }
            }

            else if (e.keyCode == (KeyCode.W))
            {
                if (!_spaceFlag)
                {
                    _go.transform.Translate(0, 0, -0.01f * _step);
                }
                else
                {
                    _go.transform.Rotate(0, 0, -0.5f * _step);
                }
            }
            else if (e.keyCode == (KeyCode.Q))
            {
                if (!_spaceFlag)
                {
                    _go.transform.Rotate(0, -0.5f * _step, 0);
                }
                else
                {
                    _go.transform.Translate(0, 0.01f * _step, 0);
                }
            }
            else if (e.keyCode == (KeyCode.E))
            {
                if (!_spaceFlag)
                {
                    _go.transform.Rotate(0, 0.5f * _step, 0);
                }
                else
                {
                    _go.transform.Translate(0, -0.01f * _step, 0);
                }
            }

            else if (e.keyCode == (KeyCode.UpArrow))
            {
                _go.transform.localScale = _go.transform.localScale + new Vector3(0.007f * _step, 0.007f * _step, 0.007f * _step);
            }
            else if (e.keyCode == (KeyCode.DownArrow))
            {
                _go.transform.localScale = _go.transform.localScale - new Vector3(0.007f * _step, 0.007f * _step, 0.007f * _step);
            }


        }
    }

    public void OnSceneGUI()
    {
        var temp = Event.current.mousePosition;
        deltaX = temp.x;
        deltaY = temp.y;

    }
    public class Folders
    {
        public GameObject _GO;
        public string Name;
        public int Count;
        
    }
   
}
