using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**
* @author Charles Lemaire, 2021
* @date - 2021-08-04
* 
* @Github https://github.com/TheGreatPixelman
*/
namespace LemaireCharles.OrbiterCamera
{
    [CustomEditor(typeof(AdvanceOrbiterCamera))]
    public class AdvancedOrbitCameraEditor : Editor
    {
        AdvanceOrbiterCamera orbitCamera;


        private void OnEnable()
        {
            orbitCamera = (AdvanceOrbiterCamera)target;
        }


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Positions", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Previous"))
            {
                orbitCamera.PreviousObservedPosition();
            }

            if (GUILayout.Button("Next"))
            {
                orbitCamera.NextObservedPosition();
            }
            EditorGUILayout.EndHorizontal();

        }
    }
}