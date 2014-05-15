using UnityEngine;
using System.Collections;

public class Joystick : TouchBtnlogic 
{
	public enum JoystickType {Movement, LookRotation, SkyColor};
	public JoystickType joystickType;
	public Transform player = null;
	public float playerSpeed = 2f, maxJoyDelta = 0.05f, rotateSpeed = 100.0f;
	private Vector3 oJoyPos, joyDelta;
	private Transform joyTrans = null;
	public CharacterController troller;
	private float pitch = 0.0f,
	yaw = 0.0f;
	//[NEW]//cache initial rotation of player so pitch and yaw don't reset to 0 before rotating
	private Vector3 oRotation;
	void Start () 
	{
		joyTrans = this.transform;
		oJoyPos = joyTrans.position;
		//NEW//cache original rotation of player so pitch and yaw don't reset to 0 before rotating
		oRotation = player.eulerAngles;
		pitch = oRotation.x;
		yaw = oRotation.y;
	}
	
	void OnTouchBegan()
	{
		//Used so the joystick only pays attention to the touch that began on the joystick
		touch2Watch = TouchBtnlogic.currTouch;
	}
	
	void OnTouchMovedAnywhere()
	{
		if(TouchBtnlogic.currTouch == touch2Watch)
		{
			//move the joystick
			joyTrans.position = MoveJoyStick();
			ApplyDeltaJoy();
		}
	}
	
	void OnTouchStayedAnywhere()
	{
		if(TouchBtnlogic.currTouch == touch2Watch)
		{
			ApplyDeltaJoy();
		}
	}
	
	void OnTouchEndedAnywhere()
	{
		//the || condition is a failsafe so joystick never gets stuck with no fingers on screen
		if(TouchBtnlogic.currTouch == touch2Watch || Input.touches.Length <= 0)
		{
			//move the joystick back to its orig position
			joyTrans.position = oJoyPos;
			touch2Watch = 64;
		}
	}
	
	void ApplyDeltaJoy()
	{
		switch(joystickType)
		{
		case JoystickType.Movement:
			troller.Move ((player.forward * joyDelta.z + player.right * joyDelta.x) * playerSpeed * Time.deltaTime);
			break;
		case JoystickType.LookRotation:
			pitch -= Input.GetTouch(touch2Watch).deltaPosition.y * rotateSpeed * Time.deltaTime;
			yaw += Input.GetTouch(touch2Watch).deltaPosition.x * rotateSpeed * Time.deltaTime;
			
			//limit so we dont do backflips
			pitch = Mathf.Clamp(pitch, -80, 80);
			
			//do the rotations of our camera 
			player.eulerAngles += new Vector3 ( pitch, yaw, 0.0f);
			break;
		case JoystickType.SkyColor:
			Camera.main.backgroundColor = new Color(joyDelta.x, joyDelta.z, joyDelta.x*joyDelta.z);
			break;
			
		}
	}
	
	Vector3 MoveJoyStick()
	{
		//convert the touch position to a % of the screen to move our joystick
		float x = Input.GetTouch (touch2Watch).position.x / Screen.width,
		y = Input.GetTouch (touch2Watch).position.y / Screen.height;
		//combine the floats into a single Vector3 and limit the delta distance
		Vector3 position = new Vector3 (Mathf.Clamp(x, oJoyPos.x - maxJoyDelta, oJoyPos.x + maxJoyDelta),
		                                Mathf.Clamp(y, oJoyPos.y - maxJoyDelta, oJoyPos.y + maxJoyDelta), 0);
		//joyDelta used for moving the player
		joyDelta = new Vector3(position.x - oJoyPos.x, 0, position.y - oJoyPos.y).normalized;
		//position used for moving the joystick
		return position;
	}
	
	void LateUpdate()
	{
		if(!troller.isGrounded)
			troller.Move(Vector3.down * 2);
	}
}
