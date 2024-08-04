using System;
using UnityEngine.EventSystems;
//using UnityEngine.InputSystem.Layouts;
using UnityEngine.UI;
using UnityEngine.Sprites;

namespace UnityEngine.InputSystem.OnScreen{

    [AddComponentMenu("Input/Customizable control")]
    public class customizeControls : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("control ID")]
        public int id;
        public int lastObjID;
        public string controlName;

        [Header("control values")]
        public float sizeMultiplier;
        public float x;
        public float y;
        public float yCheck;
        public float xCheck;

        [Header("default values")]
        public float defaultSizeMultiplier;
        public float defaultX;
        public float defaultY;

        [Header("delays")]
        public float dragActionDelay;
        private float dragActionDelayBackup;
        public float pointerDownTime;

        [Header("States")]
        public bool canDoDragAction;
        public bool canDoResizeAction;
        public bool pointerDown;
        public bool isOutOfBounds;
        public bool mouseOutOfBounds;

        [Header("Pointer information")]
        public Vector3 mousePosition;
        public Vector3 calculatedRealPosition;

        //[Header("position information")]
        //public Vector3 objPosition;

        [Header("limitation area")]
        public GameObject limitationAreaObject;
        public GameObject[] limitationAreas;
        public Color notLimitedColor;
        public Color limitedColor;
        public string limitationAreaTag;

        [Header("Settings")]
        [SerializeField]
        private Settings settings;
        public translator translatorScript;

        // Start is called before the first frame update
        void Start()
        {
            settings = Settings.Instance;
            //translatorScript = translator.Instance;
            dragActionDelayBackup = dragActionDelay;
        }

        void doDragAction(){
            this.x = mousePosition.x;
            if (!isOutOfBounds)
            {
                this.y = mousePosition.y;
                //this.x = mousePosition.x;
            }
            else{
                yCheck = this.y;
                //xCheck = this.x;
            }

            if (this.transform.position.y > 500){
                limitationAreaTag = "top";
            }
            else if (this.transform.position.y < 500){
                limitationAreaTag = "bottom";
            }
            else{
                limitationAreaTag = "none";
            }

            /*
            if (this.transform.position.x < 0){
                limitationAreaTag = "left";
            }
            else if (this.transform.position.x > 0){
                limitationAreaTag = "right";
            }
            else{
                limitationAreaTag = "none";
            }
            */

            switch (limitationAreaTag){
                case "top":
                    if (isOutOfBounds){
                        if (mousePosition.y < yCheck){
                            isOutOfBounds = false;
                        }
                    }
                    break;
                case "bottom":
                    if (isOutOfBounds){
                        if (mousePosition.y > yCheck){
                            isOutOfBounds = false;
                        }
                    }
                    break;
                /*case "left":
                    if (isOutOfBounds){
                        if (mousePosition.x > xCheck){
                            isOutOfBounds = false;
                        }
                    }
                    break;
                case "right":
                    if (isOutOfBounds){
                        if (mousePosition.x < xCheck){
                            isOutOfBounds = false;
                        }
                    }
                    break;*/
                case "none":
                    break;
                default:
                    break;
            }
            
            this.transform.position = new Vector3(x, y, 0);

            calculatedRealPosition = translatorScript.realPosition(this.transform.position.x, this.transform.position.y);
        }

        void doResizeAction(){

        }

        void checkOutOfBounds(){
            if (canDoDragAction)
            {
                limitationAreaObject.SetActive(true);
            }
            else
            {
                limitationAreaObject.SetActive(false);
            }

            RectTransform objectRectTransform = this.GetComponent<RectTransform>();
            Vector3[] objectCorners = new Vector3[4];
            objectRectTransform.GetWorldCorners(objectCorners);

            isOutOfBounds = false;
            mouseOutOfBounds = false;

            for (int i = 0; i < limitationAreas.Length; i++)
            {
                RectTransform limitationRectTransform = limitationAreas[i].GetComponent<RectTransform>();
                Vector3[] limitationCorners = new Vector3[4];
                limitationRectTransform.GetWorldCorners(limitationCorners);

                Rect limitationRect = new Rect(limitationCorners[0], limitationCorners[2] - limitationCorners[0]);

                foreach (Vector3 corner in objectCorners)
                {
                    if (limitationRect.Contains(corner))
                    {
                        limitationAreas[i].GetComponent<Image>().color = limitedColor;
                        isOutOfBounds = true;
                        break;
                    }
                    else
                    {
                        limitationAreas[i].GetComponent<Image>().color = notLimitedColor;
                    }
                }

                // Calculate the offset based on the object's dimensions
                //RectTransform objectRectTransform = this.GetComponent<RectTransform>();
                Vector3 offset = new Vector3(objectRectTransform.rect.width / 2, objectRectTransform.rect.height / 2, 0);

                // Adjust the mouse position to get the object's position
                Vector3 adjustedMousePosition = mousePosition - offset;

                if (limitationRect.Contains(adjustedMousePosition))
                {
                    mouseOutOfBounds = true;
                }
                else
                {
                    mouseOutOfBounds = false;
                }

                if (isOutOfBounds)
                {
                    break;
                }
            }
        }

        Vector3 getMousePosition(){
            if (Touchscreen.current.primaryTouch.press.isPressed){
                return Touchscreen.current.primaryTouch.position.ReadValue();
            }
            else{
                return Vector3.zero;
            }
            //return Mouse.current.position.ReadValue();
        }

        public void OnPointerDown(PointerEventData eventData){
            pointerDown = true;
        }

        public void OnPointerUp(PointerEventData eventData){
            pointerDown = false;
        }

        void doSaveAction(){
            settings.controlSizeMultiplier[id] = sizeMultiplier;
            settings.controlX[id] = x;
            settings.controlY[id] = y;
            settings.SaveSettingsToPreferences();
        }

        void main_loop(){
            doSaveAction();
            checkOutOfBounds();

            mousePosition = getMousePosition();

            if(pointerDown){
                dragActionDelay -= Time.deltaTime;
                if(dragActionDelay <= 0){
                    doDragAction();
                    canDoDragAction = true; 
                    dragActionDelay = 0;
                }
                else{
                    doResizeAction();
                }

                pointerDownTime += Time.deltaTime;
            }
            else{
                dragActionDelay = dragActionDelayBackup;
                pointerDownTime = 0;
                canDoDragAction = false;
            }

            if (pointerDownTime < dragActionDelayBackup){
                doResizeAction();
            }
            else{
                doDragAction();
            }
        }

        void FixedUpdate()
        {
            main_loop();
        }
    }
}


