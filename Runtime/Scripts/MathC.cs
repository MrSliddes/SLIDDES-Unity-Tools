using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SLIDDES
{
    /// <summary>
    /// Contains math related functions
    /// </summary>
    public class MathC
    {
        //# Functions sorted on alfabetic order

        /// <summary>
        /// Converts a bool to int 
        /// </summary>
        /// <param name="value">The value to convert to int</param>
        /// <param name="falseIsZero">[True] If value is fasle, return 0 instead of -1.</param>
        /// <returns>-1 if value is false, 1 if value is true, or 0 if falseIsZero is set to true</returns>
        public static int BoolToInt(bool value, bool falseIsZero = false)
        {
            if(falseIsZero)
            {
                // If value is fasle, return 0 instead of -1
                if(value) return 1;
                return 0;
            }
            else
            {
                if(value) return 1;
                return -1;
            }
        }

        /// <summary>
        /// Calculates the velocity an object needs to hit a target with a curve
        /// </summary>
        /// <param name="origin">The position of the object that receives the velocity</param>
        /// <param name="target">The target position to hit</param>
        /// <param name="time">Time scale, default is 1</param>
        /// <returns>The velocity the rigidbody needs in order to hit the target</returns>
        public static Vector3 CalculateVelocityNeededToHitTarget(Vector3 origin, Vector3 target, float time = 1)
        {
            Vector3 distance = target - origin;
            Vector3 distanceXz = distance;
            distanceXz.y = 0;

            float sY = distance.y;
            float sXz = distanceXz.magnitude;

            float Vxz = sXz * time;
            float Vy = (sY / time) + (0.5f * UnityEngine.Mathf.Abs(Physics.gravity.y) * time);

            Vector3 result = distanceXz.normalized;
            result *= Vxz;
            result.y = Vy;
            return result;
        }

        /// <summary>
        /// Converts an int to bool (true or false)
        /// </summary>
        /// <param name="value">Value to convert to bool</param>
        /// <param name="zeroIsFalse">[True] the value 0 returns false. [False] the value 0 returns true (default)</param>
        /// <returns>True if value is less than 0, false if value is equal of greater then 0, or false if zeroIsFalse is set to true and value is 0</returns>
        public static bool IntToBool(int value, bool zeroIsFalse = false)
        {
            if(zeroIsFalse)
            {
                // 0 <= is false, 0 > is true
                if(value <= 0) return false;
                return true;
            }
            else
            {
                // -1 <= is false, 0 >= is true
                if(value < 0) return false;
                return true;
            }
        }

        /// <summary>
        /// Convert inch value to metric value
        /// </summary>
        /// <param name="inchValue">The inch value to convert to metric</param>
        /// <returns>Metric float</returns>
        public static float InchToMetric(float inchValue)
        {
            return inchValue * 25.4f;
        }

        /// <summary>
        /// Convert Kg/m to Lbs (!NOT Lbs/ft!) 1 kg/m = 2.20462262185 lbs
        /// </summary>
        /// <param name="kgmValue">The Kg/m value to convert to lbs/ft</param>
        /// <returns>Lbs/ft float value</returns>
        public static float KgmToLbs(float kgmValue)
        {
            // https://www.csharp-console-examples.com/basic/kg-to-lb-in-c-convert-kilograms-to-pounds-in-c/
            return kgmValue * 2.20462262185f;
        }

        /// <summary>
        /// Convert Kg/m to lbs/ft
        /// </summary>
        /// <param name="kgmValue"></param>
        /// <returns>Lbs/ft</returns>
        public static float KgmToLbsFt(float kgmValue)
        {
            //https://www.convertunits.com/from/kg/m/to/lb/ft
            //https://www.convertunits.com/from/lb/ft/to/kg/m
            return kgmValue * 0.67196897675131f;
        }

        /// <summary>
        /// Convert Lbs/ft to Kg/m
        /// </summary>
        /// <param name="lbsFt"></param>
        /// <returns></returns>
        public static float LbsFtToKgm(float lbsFtValue)
        {
            return lbsFtValue / 0.67196897675131f;
        }

        /// <summary>
        /// Convert Lbs to kg/m. 1 kg/m = 2.20462262185 lbs
        /// </summary>
        /// <param name="lbsValue">The lbs value to convert to kg/m</param>
        /// <returns>kg/m in float</returns>
        public static float LbsToKgm(float lbsValue)
        {
            return lbsValue / 2.20462262185f;
        }

        /// <summary>
        /// Maps a value between 2 new values
        /// </summary>
        /// <param name="valueToMap">The value to map</param>
        /// <param name="oldMin">The old minimum value the valueToMap could be</param>
        /// <param name="oldMax">The old maximum value the valueToMap could be</param>
        /// <param name="newMin">The new minimum value the valueToMap can be</param>
        /// <param name="newMax">The new maximum value the valueToMap can be</param>
        /// <returns></returns>
        public static float Map(float valueToMap, float oldMin, float oldMax, float newMin, float newMax)
        {
            //https://forum.unity.com/threads/mapping-or-scaling-values-to-a-new-range.180090/
            float oldRange = oldMax - oldMin;
            float newRange = newMax - newMin;
            return (((valueToMap - oldMin) * newRange) / oldRange) + newMin;
        }

        /// <summary>
        /// Convert metric float to inch
        /// </summary>
        /// <param name="metricValue">The metric value to convert to inch</param>
        /// <returns>Inch float</returns>
        public static float MetricToInch(float metricValue)
        {
            return metricValue / 25.4f;
        }
    }
}