using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLIDDES.Attributes;
using SLIDDES.Components.Enums;

namespace SLIDDES.Components
{
    /// <summary>
    /// Script that gives an object a fov.
    /// </summary>
    public class FieldOfView : MonoBehaviour
    {
        [Header("Values To Edit")]
        [Tooltip("The dimension to check the FOV")]
        /// <summary>
        /// The dimension to check the FOV
        /// </summary>
        public Dimension dimension;
        [Range(0, 360)]
        [Tooltip("The fov direction in degrees. 3D = .forward + viewDirection, 2D = .right + viewDirection")]
        /// <summary>
        /// The fov direction in degrees. 3D = .forward + viewDirection, 2D = .right + viewDirection
        /// </summary>
        public float viewDirection = 0;
        [Tooltip("The view radius from the object orgin position to the outside.")]
        /// <summary>
        /// The view radius from the orgin position to the outside
        /// </summary>
        public float viewRadius = 10;
        [Range(0, 360)]
        [Tooltip("The angle inside the viewRadius that the object can see.")]
        /// <summary>
        /// The angle inside the viewRadius that the object can see
        /// </summary>
        public float viewAngle = 90;
        [Tooltip("The refresh rate in seconds in which the object looks for new targets. 0 = every frame, 1 = every 1 second.")]
        /// <summary>
        /// The refresh rate in seconds in which the object looks for new targets
        /// </summary>
        public float refreshRateFindTargets = 1;

        [Tooltip("The layermask where the targets are on.")]
        /// <summary>
        /// The layermask where the targets are on.
        /// </summary>
        public LayerMask maskTargets;
        [Tooltip("The angle inside the viewRadius that the object can see.")]
        /// <summary>
        /// The angle inside the viewRadius that the object can see.
        /// </summary>
        public LayerMask maskObstacles;

        [Header("Debug Values")]
        [Tooltip("Current list of targets that are visable to the object")]
        /// <summary>
        /// List of visable targets
        /// </summary>
        [ReadOnly] public List<Transform> visibleTargets = new List<Transform>();
        /// <summary>
        /// Should the EditorFieldOfView draw a 3d sphere?
        /// </summary>
        [HideInInspector] public bool debugDraw3DSphere = false;

        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(FindTargetsWithDelay());
        }      

        /// <summary>
        /// Get a direction from an angle
        /// </summary>
        /// <param name="angleInDegrees">The angle in degrees</param>
        /// <param name="angleIsGlobal">Is the angle global?</param>
        /// <returns>Vector 3 direction</returns>
        public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            angleInDegrees += viewDirection;

            if(dimension == Dimension.dimension3D)
            {
                if(!angleIsGlobal)
                {
                    // Convert to global angle by adding the transforms own rotation to it.
                    angleInDegrees += transform.eulerAngles.y;
                }
                return new Vector3(UnityEngine.Mathf.Sin(angleInDegrees * UnityEngine.Mathf.Deg2Rad), 0, UnityEngine.Mathf.Cos(angleInDegrees * UnityEngine.Mathf.Deg2Rad));
            }
            else
            {
                if(!angleIsGlobal)
                {
                    // Convert to global angle by adding the transforms own rotation to it.
                    angleInDegrees += transform.eulerAngles.y;
                }
                angleInDegrees += 90; // 2D forward is Vector3.right
                return new Vector3(UnityEngine.Mathf.Sin(angleInDegrees * UnityEngine.Mathf.Deg2Rad), UnityEngine.Mathf.Cos(angleInDegrees * UnityEngine.Mathf.Deg2Rad), 0);
            }
        }

        /// <summary>
        /// Get the closest target from the fov
        /// </summary>
        /// <returns></returns>
        public Transform GetClosestTarget(Transform orgin)
        {
            if(visibleTargets.Count == 0) return null;
            float distance = Vector3.Distance(orgin.position, visibleTargets[0].position);
            Transform closest = visibleTargets[0];
            for(int i = 1; i < visibleTargets.Count; i++)
            {
                if(Vector3.Distance(orgin.position, visibleTargets[i].position) < distance)
                {
                    distance = Vector3.Distance(orgin.position, visibleTargets[i].position);
                    closest = visibleTargets[i];
                }
            }
            return closest;
        }

        /// <summary>
        /// Finds targets that the object can see with given viewRadius and viewAngle
        /// </summary>
        private void FindVisableTargets()
        {
            visibleTargets.Clear();

            if(dimension == Dimension.dimension3D)
            {
                // Get all the targets in viewRadius
                Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, maskTargets);
                // Loop trough and check if target is in viewAngle
                for(int i = 0; i < targetsInViewRadius.Length; i++)
                {
                    Vector3 directionToTarget = (targetsInViewRadius[i].transform.position - transform.position).normalized;
                    // If target is in viewAngle
                    if(Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                    {
                        // Check if there is a obstacle between target and object
                        float distanceToTarget = Vector3.Distance(transform.position, targetsInViewRadius[i].transform.position);
                        if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, maskObstacles))
                        {
                            // No obstacles in the way, object can see target, add to list
                            if(!visibleTargets.Contains(targetsInViewRadius[i].transform)) // If an object has multiple colliders check if list doesnt already contain it
                            {
                                visibleTargets.Add(targetsInViewRadius[i].transform);
                            }
                        }
                    }
                }
            }
            else
            {
                // Get all the targets in viewRadius
                Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, maskTargets);
                // Loop trough and check if target is in viewAngle
                for(int i = 0; i < targetsInViewRadius.Length; i++)
                {
                    Vector3 directionToTarget = (targetsInViewRadius[i].transform.position - transform.position).normalized;
                    // If target is in viewAngle
                    if(Vector3.Angle(transform.right, directionToTarget) < viewAngle / 2)
                    {
                        // Check if there is a obstacle between target and object
                        float distanceToTarget = Vector3.Distance(transform.position, targetsInViewRadius[i].transform.position);
                        if(!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, maskObstacles))
                        {
                            // No obstacles in the way, object can see target, add to list
                            if(!visibleTargets.Contains(targetsInViewRadius[i].transform)) // If an object has multiple colliders check if list doesnt already contain it
                            {
                                visibleTargets.Add(targetsInViewRadius[i].transform);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Find the targets with a delay. (No need to search every frame)
        /// </summary>
        /// <param name="delay">Delay interval in seconds</param>
        /// <returns></returns>
        private IEnumerator FindTargetsWithDelay()
        {
            while(true)
            {
                yield return new WaitForSeconds(refreshRateFindTargets);
                FindVisableTargets();
            }
        }

        private void OnValidate()
        {
            // Prevent List visableTargets to be changed manually
            if(!Application.isPlaying && visibleTargets.Count != 0)
            {
                Debug.LogWarning("<color=yellow><b>[Editor]</b></color><color=aqua>[NotAllowed]</color>: Please do not change the list size in the editor.");
                visibleTargets = new List<Transform>(0);
            }

            refreshRateFindTargets = UnityEngine.Mathf.Clamp(refreshRateFindTargets, 0, UnityEngine.Mathf.Infinity);
        }
    }

}