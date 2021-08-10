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
    [CustomEditor(typeof(OrbitCamera))]
    public class OrbitCameraEditor : Editor
    {
        OrbitCamera orbitCamera;

        private void OnEnable()
        {
            orbitCamera = (OrbitCamera)target;
        }

        public override void OnInspectorGUI()
        {
            ShowGeneralDetails();

            switch (orbitCamera.cameraType)
            {
                case CameraType.Orbiter:
                    ShowOrbiterDetails();
                    break;
                case CameraType.ConstantOffset:
                    ShowConstantOffset();
                    ShowTimerOptions();
                    break;
                case CameraType.PositionsOffset:
                    ShowPositionsOffset();
                    ShowTimerOptions();
                    break;
            }
        }

        void ShowGeneralDetails()
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("General Options", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Direction");
            orbitCamera.cameraRotation = (CameraRotationDirection)EditorGUILayout.EnumPopup(orbitCamera.cameraRotation);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Type");
            orbitCamera.cameraType = (CameraType)EditorGUILayout.EnumPopup(orbitCamera.cameraType);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Target Reference");
            orbitCamera.followTarget = (Transform)EditorGUILayout.ObjectField(orbitCamera.followTarget, typeof(Transform), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Camera Options", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Camera Reference");
            orbitCamera.camera = (Camera)EditorGUILayout.ObjectField(orbitCamera.camera, typeof(Camera), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Camera Distance");
            orbitCamera.cameraDistance = EditorGUILayout.FloatField(orbitCamera.cameraDistance);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Camera Height");
            orbitCamera.cameraHeight = EditorGUILayout.FloatField(orbitCamera.cameraHeight);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Camera Auto Rotation");
            orbitCamera.autoLook = EditorGUILayout.Toggle(orbitCamera.autoLook);
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginDisabledGroup(orbitCamera.autoLook);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Camera Rotation");
            orbitCamera.cameraAngle = EditorGUILayout.FloatField(orbitCamera.cameraAngle);
            EditorGUILayout.EndHorizontal();
            EditorGUI.EndDisabledGroup();


        }

        void ShowOrbiterDetails()
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Orbit Options", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Rotation Speed °/s");
            orbitCamera.degreePerSecond = EditorGUILayout.FloatField(orbitCamera.degreePerSecond);
            EditorGUILayout.EndHorizontal();

        }

        void ShowConstantOffset()
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Constant Offset Options", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Angle Offset");
            orbitCamera.angleOffset = EditorGUILayout.FloatField(orbitCamera.angleOffset);
            EditorGUILayout.EndHorizontal();
        }

        void ShowPositionsOffset()
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Positions Offset Options", EditorStyles.boldLabel);

            SerializedObject t = new SerializedObject(orbitCamera);
            EditorGUILayout.PropertyField(t.FindProperty("angleOffsets"));

            t.ApplyModifiedProperties();
        }

        void ShowTimerOptions()
        {
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Timer Options", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Should Rotate with Timer");
            orbitCamera.shouldTimerRotate = EditorGUILayout.Toggle(orbitCamera.shouldTimerRotate);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Rotation time (s)");
            orbitCamera.rotationTime = EditorGUILayout.FloatField(orbitCamera.rotationTime);
            EditorGUILayout.EndHorizontal();
        }
    }

}
