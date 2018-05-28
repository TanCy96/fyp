/******************************************************************************
 * Copyright (C) Leap Motion, Inc. 2011-2017.                                 *
 * Leap Motion proprietary and  confidential.                                 *
 *                                                                            *
 * Use subject to the terms of the Leap Motion SDK Agreement available at     *
 * https://developer.leapmotion.com/sdk_agreement, or another agreement       *
 * between Leap Motion and you, your company or other organization.           *
 ******************************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Leap.Unity.Examples
{

    [AddComponentMenu("")]
    public class FaceCameraScript : MonoBehaviour
    {

        public Transform toFaceCamera;
        public LineRenderer beam;

        private bool _initialized = false;
        private bool _isFacingCamera = false;
        bool isCharging = false;
        bool isClose = false;
        RaycastHit hit;
        Ray ray;

        public UnityEvent OnBeginFacingCamera;
        public UnityEvent OnEndFacingCamera;
        public UnityEvent OnBeginCharging;
        public UnityEvent OnEndCharging;

        void Start()
        {
            if (toFaceCamera != null) initialize();
            beam.gameObject.SetActive(false);
        }

        private void initialize()
        {
            // Set "_isFacingCamera" to be whatever the current state ISN'T, so that we are
            // guaranteed to fire a UnityEvent on the first initialized Update().
            _isFacingCamera = !GetIsFacingCamera(toFaceCamera, Camera.main);
            isCharging = !GetIsCharging(toFaceCamera, Camera.main);
            _initialized = true;
        }

        void Update()
        {
            if (toFaceCamera != null && !_initialized)
            {
                initialize();
            }
            if (!_initialized) return;

            if (GetIsFacingCamera(toFaceCamera, Camera.main, _isFacingCamera ? 0.77F : 0.96F) != _isFacingCamera)
            {
                _isFacingCamera = !_isFacingCamera;

                if (_isFacingCamera)
                {
                    OnBeginFacingCamera.Invoke();
                }
                else
                {
                    OnEndFacingCamera.Invoke();
                }
            }

            if (GetIsCharging(toFaceCamera, Camera.main) != isCharging)
            {
                isCharging = !isCharging;

                if (isCharging)
                {
                    OnBeginCharging.Invoke();
                }
                else
                {
                    OnEndCharging.Invoke();
                }
            }

            ray = new Ray(transform.position, transform.forward);
            if (Physics.SphereCast(ray, 0.05f, out hit, 1))
            {
                if (hit.collider.tag == "Bullet")
                {
                    if (hit.distance < 0.2)
                    {
                        isClose = true;
                    }
                    else
                    {
                        isClose = false;
                    }

                    hit.collider.gameObject.GetComponentInParent<CustomInteractionBehaviour>().magnet = true;
                    if (toFaceCamera.name == "Palm (Left) Forward Transform")
                    {
                        hit.collider.gameObject.GetComponentInParent<CustomInteractionBehaviour>().isLeftHand = true;
                    }
                    else
                    {
                        hit.collider.gameObject.GetComponentInParent<CustomInteractionBehaviour>().isLeftHand = false;
                    }
                }
            }

            if (transform.rotation.x < -0.2 && !isClose)
            {
                beam.gameObject.SetActive(true);
            }
            else
            {
                beam.gameObject.SetActive(false);
            }
        }

        public static bool GetIsFacingCamera(Transform facingTransform, Camera camera, float minAllowedDotProduct = 0.95F)
        {

            return Vector3.Dot((camera.transform.position - facingTransform.position).normalized, facingTransform.forward) > minAllowedDotProduct;
        }

        public static bool GetIsCharging(Transform facingTransform, Camera camera)
        {
            float direction = Vector3.Dot((camera.transform.position - facingTransform.position).normalized, facingTransform.forward);
            if (direction > -0.4 && direction < 0.7)
                return true;
            else
                return false;
        }

    }

}
