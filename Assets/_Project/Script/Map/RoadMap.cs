using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class RoadMap : Image
{
    [SerializeField] private int a;
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        base.OnPopulateMesh(vh);

        for (int i = 0; i < vh.currentVertCount / 2; i++)
        {
            if (a < i) continue;
            UIVertex vert = UIVertex.simpleVert;
            UIVertex v = UIVertex.simpleVert;
            vh.PopulateUIVertex(ref vert, i);
            vh.PopulateUIVertex(ref v, vh.currentVertCount - i - 1);

            vert.color = Color.clear;
            v.color = Color.clear;
            vh.SetUIVertex(vert, i);
            vh.SetUIVertex(v, vh.currentVertCount - 1-i);
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(RoadMap))]
public class RoadMapEditor : Editor
{
    private RoadMap _roadMap;
    private void Awake()
    {
        _roadMap = (RoadMap)target;
        
    }
}
#endif
