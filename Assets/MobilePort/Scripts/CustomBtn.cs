using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;
using UnityEngine.Sprites;

namespace UnityEngine.InputSystem.OnScreen
{
    [AddComponentMenu("Input/On-Screen Button")]
    public class CustomBtn : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        public Sprite non_pressed;
        public Sprite pressed;
        public Image imageComponent;

        void Start()
        {
            imageComponent = GetComponent<Image>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SendValueToControl(1.0f);
            imageComponent.sprite = pressed;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(0.0f);
            imageComponent.sprite = non_pressed;
        }

        [InputControl(layout = "Button")]
        [SerializeField]
        private string m_ControlPath;

        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }
    }
}

