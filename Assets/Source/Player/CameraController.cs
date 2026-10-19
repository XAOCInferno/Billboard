using UnityEngine;
using UnityEngine.Assertions;

namespace Source
{
    /**
     * Controller for the player Camera
     */
    public static class CameraController
    {
        /**
         * Attached a camera to an object with a given offset
         * Will parent the camera under the object then offset it
         */
        public static void AttachCamera(Transform parent, Vector3 localOffset)
        {
            Assert.IsNotNull(Camera.main);

            Camera.main.transform.parent = parent;
            Camera.main.transform.localPosition = localOffset;
        }

        /**
         * Rotate the camera by a given angle. The Rotation will be done in the Y axis
         */
        public static void RotatePitch(float angle)
        {
            Assert.IsNotNull(Camera.main);

            // TODO: In player settings
            const float lookSensitivityY = 0.25f;
            Transform camTransform = Camera.main.transform;
            Vector3 currentAngles = camTransform.localRotation.eulerAngles;

            Vector3 rotation = Vector3.left * angle + currentAngles;
            Quaternion from = Quaternion.Euler(currentAngles);
            Quaternion to = Quaternion.Euler(rotation);
            camTransform.localRotation = Quaternion.RotateTowards(from, to, lookSensitivityY);
        }
    }
}