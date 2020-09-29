using UnityEngine;

namespace QFramework.Helpers.DataHelpers.ScriptableData
   {
       public class GamesCountDeactivator : MonoBehaviour
       {
           [SerializeField] IntPPScriptableData data;
           [SerializeField] GamesCountDeactivatorConfig config;
   
           void Awake()
           {
               if (data.Value > config.gamesCount)
                   gameObject.SetActive(false);
           }
       }
   }