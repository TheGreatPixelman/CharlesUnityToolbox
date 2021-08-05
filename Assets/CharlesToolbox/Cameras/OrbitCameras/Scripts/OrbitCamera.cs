using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* @author Charles Lemaire, 2021
* @date - 2021-08-04
* 
* @Github https://github.com/TheGreatPixelman
*/
namespace LemaireCharles.OrbiterCamera
{
    public enum CameraType { Orbiter, ConstantOffset, PositionsOffset }
    public enum CameraRotationDirection { Clockwise, CounterClockwise }

    public class OrbitCamera : MonoBehaviour
    {
        public CameraRotationDirection cameraRotation;
        public CameraType cameraType;
        public Transform followTarget;

        public float degreePerSecond;
        public float cameraDistance;
        public float cameraHeight;
        public Camera camera;


        public float angleOffset;

        public float[] angleOffsets;

        public bool shouldTimerRotate;
        public float rotationTime;

        float currentRotationTimer;
        float currentAngleOffset;
        // Start is called before the first frame update
        void Start()
        {
            if (CheckIfCameraIsReferenced())
                camera.transform.localPosition = new Vector3(0, cameraHeight, cameraDistance);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!CheckIfCameraIsReferenced()) 
                return;

            switch (cameraType)
            {
                case CameraType.Orbiter:
                    RotateAroundTarget();
                    break;
                case CameraType.ConstantOffset:
                    if (shouldTimerRotate)
                        RotateOffset();
                    break;
                case CameraType.PositionsOffset:
                    if (shouldTimerRotate)
                        RotatePositionOffset();
                    break;
            }

            MoveCamera();

            if (followTarget)
            {
                MoveTowardsTarget();
            }
        }

        bool CheckIfCameraIsReferenced()
        {
            if (camera == null)
            {
                Debug.LogWarning("A Camera is required! Reactivate this script when the camera has been added!");
                enabled = false;
                return false;
            }

            return true;
        }

        void RotateAroundTarget()
        {
            float rotationDirection = cameraRotation == CameraRotationDirection.Clockwise ? 1 : -1;
            transform.Rotate(degreePerSecond * rotationDirection * Vector3.up * Time.fixedDeltaTime);
        }

        public void RotateOffset()
        {

            //if timer is not done
            if (currentRotationTimer > 0)
            {
                currentRotationTimer -= Time.fixedDeltaTime;
                return;
            }

            if (coRotateToOffset != null)
            {
                StopCoroutine(coRotateToOffset);
                coRotateToOffset = null;
            }

            coRotateToOffset = StartCoroutine(CoRotateToOffset());

            currentRotationTimer = rotationTime;
        }

        public void RotatePositionOffset()
        {
            //if timer is not done
            if (currentRotationTimer > 0)
            {
                currentRotationTimer -= Time.fixedDeltaTime;
                return;
            }

            if (coRotatePositionOffset != null)
            {
                StopCoroutine(coRotatePositionOffset);
                coRotateToOffset = null;
            }

            coRotatePositionOffset = StartCoroutine(CoRotatePositionOffset());

            currentRotationTimer = rotationTime;
        }

        Coroutine coRotateToOffset;
        IEnumerator CoRotateToOffset()
        {
            float t = 0;

            float rotationDirection = cameraRotation == CameraRotationDirection.Clockwise ? 1 : -1;

            Vector3 currentRotation = transform.localEulerAngles;
            Quaternion rotation = Quaternion.Euler(new Vector3(currentRotation.x, currentRotation.y + angleOffset * rotationDirection, currentRotation.z));

            while (t < 1)
            {

                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, t);
                t += Time.fixedDeltaTime / rotationTime;

                yield return null;
            }
        }

        Coroutine coRotatePositionOffset;
        IEnumerator CoRotatePositionOffset()
        {
            float t = 0;

            float rotationDirection = cameraRotation == CameraRotationDirection.Clockwise ? 1 : -1;
            float currentAngle = Mathf.Round(transform.localEulerAngles.y * 100f) / 100f;

            float nextAngle = 0;


            if (cameraRotation == CameraRotationDirection.Clockwise)
            {
                for (int i = 0; i <= angleOffsets.Length; i++)
                {
                    if (i == angleOffsets.Length)
                    {
                        nextAngle = angleOffsets[0];
                        break;
                    }

                    if (currentAngle < angleOffsets[i])
                    {
                        nextAngle = angleOffsets[i];
                        break;
                    }
                }
            }
            else if (cameraRotation == CameraRotationDirection.CounterClockwise)
            {
                for (int i = angleOffsets.Length - 1; i >= -1; i--)
                {
                    if (i == -1)
                    {
                        nextAngle = angleOffsets[angleOffsets.Length - 1];
                        break;
                    }

                    if (currentAngle > angleOffsets[i])
                    {
                        nextAngle = angleOffsets[i];
                        break;
                    }
                }
            }



            Quaternion rotation = Quaternion.Euler(transform.rotation.x, nextAngle, transform.rotation.z);

            while (t < 1)
            {

                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, t);
                t += Time.fixedDeltaTime / rotationTime;

                yield return null;
            }
        }


        void MoveCamera()
        {
            camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, Vector3.forward * cameraDistance + Vector3.up * cameraHeight, Time.fixedDeltaTime);
            camera.transform.LookAt(transform);
        }

        void MoveTowardsTarget()
        {
            transform.position = Vector3.Lerp(transform.position, followTarget.position, Time.fixedDeltaTime * 10);
        }
    }
}