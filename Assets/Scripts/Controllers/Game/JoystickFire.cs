using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class JoystickFire : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {

    [HideInInspector]
    public bool Pressed;

    //----------------------------------------------------------------------------------
    //  Joystick behavior
    //----------------------------------------------------------------------------------

    public void OnPointerUp(PointerEventData eventData) {
        Pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData) {
        Pressed = true;
    }

}
