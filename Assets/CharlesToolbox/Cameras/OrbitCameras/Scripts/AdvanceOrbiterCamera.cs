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
    public class AdvanceOrbiterCamera : MonoBehaviour
    {
        [Header("Options")]
        public Camera camera;
        public float speed;
        public bool startAtFirstPosition;
        public ObservablePosition[] observablePositions;

        int currentObservedIndex;
        ObservablePosition currentObservedPosition;

        // Start is called before the first frame update
        void Start()
        {
            if (observablePositions.Length > 0)
            {
                currentObservedIndex = 0;
                currentObservedPosition = observablePositions[currentObservedIndex];

                if (startAtFirstPosition)
                {
                    transform.rotation = Quaternion.Euler(0, currentObservedPosition.angle, 0);
                    camera.transform.localPosition = currentObservedPosition.GetPosition();
                    camera.transform.LookAt(currentObservedPosition.transform);
                }
            }
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            ObserveCurrentPosition();
        }

        public void NextObservedPosition()
        {
            if (observablePositions.Length > 1)
            {
                currentObservedIndex = currentObservedIndex + 1 >= observablePositions.Length ? 0 : currentObservedIndex + 1;
                currentObservedPosition = observablePositions[currentObservedIndex];
            }
        }

        public void PreviousObservedPosition()
        {
            if (observablePositions.Length > 1)
            {
                currentObservedIndex = currentObservedIndex - 1 < 0 ? observablePositions.Length - 1 : currentObservedIndex - 1;
                currentObservedPosition = observablePositions[currentObservedIndex];
            }
        }

        void ObserveCurrentPosition()
        {
            if (transform.eulerAngles.y - observablePositions[currentObservedIndex].angle != 0)
            {
                Quaternion rotation = Quaternion.Euler(0,currentObservedPosition.angle,0);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.fixedDeltaTime * speed);
                camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, currentObservedPosition.GetPosition(), Time.fixedDeltaTime * speed);

                Quaternion lookOnLook = Quaternion.LookRotation(currentObservedPosition.transform.position - camera.transform.position);
                camera.transform.rotation = Quaternion.Slerp(camera.transform.rotation, lookOnLook, Time.fixedDeltaTime * speed);
            }
        }
    }
}