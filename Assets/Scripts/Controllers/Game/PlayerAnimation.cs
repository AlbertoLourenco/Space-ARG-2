using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {

    [SerializeField]
    private Joystick _joystickMove;

    private Animator _animator;

    //----------------------------------------------------------------------------------
    //  MonoBehaviour
    //----------------------------------------------------------------------------------

	void Start () {

        _animator = this.GetComponent<Animator>();
	}
	
	void Update () {
        
        float horizontalAxis = Input.GetAxis("Horizontal") + _joystickMove.Horizontal;

        if (horizontalAxis < 0) {
            _animator.SetBool("Turning_Left", true);
            _animator.SetBool("Turning_Right", false);
        }else
            if (horizontalAxis > 0) {
                _animator.SetBool("Turning_Right", true);
                _animator.SetBool("Turning_Left", false);
            }else{
                _animator.SetBool("Turning_Left", false);
                _animator.SetBool("Turning_Right", false);
            }
	}
}
