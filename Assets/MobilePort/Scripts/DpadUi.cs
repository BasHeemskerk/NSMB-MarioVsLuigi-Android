using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;
using UnityEngine.Sprites;

namespace UnityEngine.InputSystem.OnScreen
{
    [AddComponentMenu("Input/On-Screen Button")]
    public class DpadUi : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        public Image[] graphics;
        public Color defaultColor;
        public Color pressedColor;

        void Start(){
            defaultColor = graphics[0].color;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            SendValueToControl(1.0f);
            for (int i = 0; i < graphics.Length; i++){
                graphics[i].color = pressedColor;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(0.0f);
            for (int i = 0; i < graphics.Length; i++){
                graphics[i].color = defaultColor;
            }
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

