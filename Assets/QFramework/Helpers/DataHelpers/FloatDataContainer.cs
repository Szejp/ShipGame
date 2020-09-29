using UnityEngine;

namespace QFramework.Helpers.DataHelpers
 {
     [CreateAssetMenu(fileName = "DataContainer", menuName = "Data/DataContainer")]
     public class FloatDataContainer : ScriptableObject
     {
         public FloatData floatData;

         void OnEnable()
         {
             floatData.MinMax = floatData.MinMaxFloat;
             floatData.Value = (floatData.MinMaxFloat).Max;
         }
     }
 }