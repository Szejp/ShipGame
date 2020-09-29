using System;
 using UnityEngine;
 
 namespace QFramework.Helpers
 {
     [Serializable]
     public class AnimationCurveData
     {
         public AnimationCurve animationCurve;
         public float xModifier;
 
         public float Evaluate(float x)
         {
             return animationCurve.Evaluate(x / xModifier);
         }
     }
 }