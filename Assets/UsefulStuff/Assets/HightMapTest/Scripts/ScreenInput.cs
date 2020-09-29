﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UsefulStuff.Assets.HightMapTest.Scripts {
    public class ScreenInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {

        public static event Action<Vector2> OnPointerDownEvent;
        public static event Action<Vector2> OnPointerUpEvent;

        public void OnPointerDown(PointerEventData pointerData) {
            OnPointerDownEvent?.Invoke(pointerData.position);
        
        }

        public void OnPointerUp(PointerEventData pointerData) {
            OnPointerUpEvent?.Invoke(pointerData.position);
        
        }
    }
}
