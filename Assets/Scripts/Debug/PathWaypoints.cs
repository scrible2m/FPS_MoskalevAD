using System.Collections.Generic;
using UnityEngine;

public class PathWaypoints : MonoBehaviour
{
    public List<Transform> nodes = new List<Transform>();
    public Vector3 _currNode;
    public Vector3 _prevNode;
    public int _nodeCounter;

    private void OnDrawGizmos()
        
        
    {
        if (transform.childCount != _nodeCounter)      
        {
            nodes.Clear();
            _nodeCounter = 0;
        }

        if (transform.childCount > 0)
        {
            foreach (Transform item in transform)
            {
                if (!nodes.Contains(item))
                {
                    nodes.Add(item);
                }
                _nodeCounter++;
            }
        }

        if (nodes.Count > 1)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                _currNode = nodes[i].position;
                if (i > 0)
                {
                    _prevNode = nodes[i - 1].position;
                }
                else if (i == 0 && nodes.Count > 1)
                {
                    _prevNode = nodes[nodes.Count - 1].position;
                   
                }
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(_prevNode, _currNode);
                Gizmos.color = Color.white;
                Gizmos.DrawSphere(_currNode, 1f);
            }
        }
    }

   
}
