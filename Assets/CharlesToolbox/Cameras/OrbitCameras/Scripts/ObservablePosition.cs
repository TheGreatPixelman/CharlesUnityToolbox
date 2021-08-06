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
    public class ObservablePosition : MonoBehaviour
    {
        public float angle;
        public float distance;
        public float height;

        public Vector3 GetPosition()
        {
            return transform.localPosition + new Vector3(0, height, distance);
        }
    }
}
