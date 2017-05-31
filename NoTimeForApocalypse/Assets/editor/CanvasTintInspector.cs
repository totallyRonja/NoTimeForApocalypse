using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CanvasFade))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI(){
        base.OnInspectorGUI();
        if (target.GetType() == typeof(CanvasFade)){
            CanvasFade fade = (CanvasFade)target;
            fade.fade = fade._fade;
        }
    }
}