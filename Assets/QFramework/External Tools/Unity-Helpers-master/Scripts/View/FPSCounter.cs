﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace QFramework.External_Tools.Scripts.View
{
    [RequireComponent(typeof(Text))]
    public class FPSCounter : MonoBehaviour
    {
        public float timeBetweenUpdates = 1f;

        private Text text;
        private DateTime timeOfLastCount;
        private float leftover;
        private float frameCount;
 
        void Start()
        {
            text = GetComponent<Text>();
            timeOfLastCount = DateTime.Now;
            leftover = 0;
        }

        void Update()
        {
            frameCount++;
            var ms = (float)(DateTime.Now - timeOfLastCount).TotalSeconds + leftover;
            if (ms > timeBetweenUpdates)
            {
                leftover = ms - timeBetweenUpdates;
                timeOfLastCount = DateTime.Now;
                var fps = frameCount * (1f / timeBetweenUpdates);
                text.text = fps.ToString("00");
                frameCount = 0;
            }
        }

    }
}
