using UnityEngine;
using System.Collections;

public class SwipeRotationPlayer : TouchBtnlogic 
{
	public float rotateSpeed = 100.0f;
	public int invertPitch = 1;
	public Transform player;
	private float pitch = 0.0f,
	yaw = 0.0f;
	//[NEW]//cache initial rotation of player so pitch and yaw don't reset to 0 before rotating
	private Vector3 oRotation;
	
	//[NEW]//
	void Start()
	{
		//cache original rotation of player so pitch and yaw don't reset to 0 before rotating
		oRotation = player.eulerAngles;
		pitch = oRotation.x;
		yaw = oRotation.y;
	}
	//[NEW]//
	void OnTouchBegan()
	{
		//need to cache the touch index that started on the pad so others wont interfere
		touch2Watch = TouchBtnlogic.currTouch;
	}
	
	public void OnTouchMoved()
	{
		pitch -= Input.GetTouch(touch2Watch).deltaPosition.y * rotateSpeed * invertPitch * Time.deltaTime;
		yaw += Input.GetTouch(touch2Watch).deltaPosition.x * rotateSpeed * invertPitch * Time.deltaTime;
		
		//limit so we dont do backflips
		pitch = Mathf.Clamp(pitch, -80, 80);
		
		//do the rotations of our camera 
		player.eulerAngles = new Vector3 ( pitch, yaw, 0.0f);
	}
	//[NEW]//
	void OnTouchEndedAnywhere()
	{
		//the || condition is a failsafe
		if(TouchBtnlogic.currTouch == touch2Watch || Input.touches.Length <= 0)
			touch2Watch = 64;
	}
}
