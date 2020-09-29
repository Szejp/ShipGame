using QFramework.Helpers.Physics;
using UnityEngine;

namespace LevelManagement
{
    [RequireComponent(typeof(OnTrigger2D))]
    public class BossContainer : Container
    {
        OnTrigger2D trigger2d;

        void Awake()
        {
            trigger2d = GetComponent<OnTrigger2D>();
            trigger2d.onTriggerEnter2D += Trigger2DOnOnTriggerEnter2D;
        }

        void Trigger2DOnOnTriggerEnter2D(OnTrigger2D arg1, Collider2D arg2)
        {
            var player = arg2.GetComponent<Player>();

            if (player != null)
            {
                // GameContext.Effects.TriggerBossMode();
            }
        }
    }
}