using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SLIDDES.Components.Enums;

namespace SLIDDES.Components
{
    /// <summary>
    /// Allows objects to be parallaxed scrolled
    /// </summary>
    [DisallowMultipleComponent]
    public class ParallaxScrolling : MonoBehaviour
    {
        // This script uses an custom editor (EditorParallaxScrolling)

        [Header("Values")]
        /// <summary>
        /// The axis direction for parallax scrolling
        /// </summary>
        public ParallaxScrollingAxis scrollingAxis;
        /// <summary>
        /// The global scrolling speed of the axis direction. -1 = left/down, 1 = right/up
        /// </summary>
        public float scrollingSpeed = -1;
                
        [Header("Static Scrolling")]        
        [Tooltip("Is parallax scrolling static, or does it move with an object?")]
        /// <summary>
        /// Is parallax scrolling static, or does it move with an object?
        /// </summary>
        public bool isStaticScrolling = true;
        [Tooltip("The target to follow while scrolling")]
        /// <summary>
        /// The target to follow while scrolling
        /// </summary>
        public Transform target;

        [Header("Parallax Scrolling Objects")]
        public ParallaxScrollingObject[] scrollingObjects;

        [Header("Debug")]       
        
        /// <summary>
        /// Use unity default editor or use custom editor for this script
        /// </summary>
        public bool debugUseCustomEditor = true;
        /// <summary>
        /// The name of limit A or left/down
        /// </summary>
        [HideInInspector] public string limitAName = "LimitLeft";
        /// <summary>
        /// The name of limit B or right/up
        /// </summary>
        [HideInInspector] public string limitBName = "LimitRight";

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //UpdateTransforms();
        }

        private void LateUpdate()
        {
            UpdateTransforms();
        }

        private void FixedUpdate()
        {
            //UpdateTransforms();
        }

        private void UpdateTransforms()
        {
            foreach(ParallaxScrollingObject item in scrollingObjects)
            {
                foreach(Transform t in item.transforms)
                {
                    t.Translate(Vector3.right * Time.deltaTime * item.scrollingSpeed * scrollingSpeed);
                    // Check limit
                    if(scrollingSpeed < 0)
                    {
                        // Moving left, check over left
                        if(t.position.x < item.limitLeft.x)
                        {
                            // Reset to right
                            t.position = new Vector3(item.limitRight.x, t.position.y, t.position.z);
                        }
                    }
                    else
                    {
                        // Move right
                        if(t.position.x > item.limitRight.x)
                        {
                            // Reset to left
                            t.position = new Vector3(item.limitLeft.x, t.position.y, t.position.z);
                        }
                    }
                }
            }
        }

    }

    [System.Serializable]
    public class ParallaxScrollingObject
    {
        /// <summary>
        /// Name of the object group
        /// </summary>
        [Tooltip("Name of the object (group)")]
        public string name;
        public float scrollingSpeed = 1;
        
        public Vector3 limitLeft;
        public Vector3 limitRight;
        public Transform[] transforms;

        private ParallaxScrolling parent;
    }    
}

namespace SLIDDES.Components.Enums
{
    [System.Serializable]
    public enum ParallaxScrollingAxis
    {
        horizontal,
        vertical
    }
}