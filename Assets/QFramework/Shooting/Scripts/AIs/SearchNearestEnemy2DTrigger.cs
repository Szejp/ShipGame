using System.Collections.Generic;
using QFramework.GameModule.GameTools.Entities;
using QFramework.GameModule.GameTools.Teams;
using QFramework.Helpers.Interfaces;
using QFramework.Helpers.Physics;
using UnityEngine;

namespace QFramework.Shooting.Scripts.AIs
{
    [RequireComponent(typeof(Entity))]
    public class SearchNearestEnemy2DTrigger : MonoBehaviour, ITargetProvider
    {
        [SerializeField] OnTrigger2D onTrigger2D;

        List<Entity> entities = new List<Entity>();
        Entity entity;

        void Awake()
        {
            onTrigger2D.onTriggerEnter2D += OnTriggerEnter2DHandler;
            onTrigger2D.onTriggerExit2D += OnTriggerExit2DHandler;
            entity = GetComponent<Entity>();
        }

        void OnDestroy()
        {
            onTrigger2D.onTriggerEnter2D -= OnTriggerEnter2DHandler;
            onTrigger2D.onTriggerExit2D -= OnTriggerExit2DHandler;
        }

        void OnTriggerEnter2DHandler(OnTrigger2D trigger, Collider2D collider)
        {
            var collidedEntity = collider.GetComponent<Entity>();
            if(collidedEntity != null && !entities.Contains(collidedEntity))
                entities.Add(collidedEntity);
        }

        void OnTriggerExit2DHandler(OnTrigger2D trigger, Collider2D collider)
        {
            var collidedEntity = collider.GetComponent<Entity>();
            if(collidedEntity != null && entities.Contains(collidedEntity))
                entities.Remove(collidedEntity);
        }

        public Transform GetTarget()
        {
            if (entities.Count.Equals(0))
                return null;

            Transform result = null;
            for (int i = 0; i < entities.Count; i++)
            {
                if (Teams.AreInTheSameTeam(entity.TeamId, entities[i].TeamId))
                    continue;
                
                if (result == null || Vector3.Distance(transform.position, entities[i].transform.position) <
                    Vector3.Distance(transform.position, result.position))
                    result = entities[i].transform;
            }

            return result;
        }
    }
}