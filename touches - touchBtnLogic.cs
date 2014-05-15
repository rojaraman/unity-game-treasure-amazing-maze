using UnityEngine;
using System.Collections;

public class TouchBtnlogic : MonoBehaviour 
{
	public static int currTouch = 0;//so other scripts can know what touch is currently on screen
	private Ray ray;//this will be the ray that we cast from our touch into the scene
	private RaycastHit rayHitInfo = new RaycastHit();//return the info of the object that was hit by the ray
	[HideInInspector]
	public int touch2Watch = 64;
	
	void Update () 
	{
		//is there a touch on screen?
		if(Input.touches.Length <= 0)
		{
			//if no touches then execute this code
		}
		else //if there is a touch
		{
			//loop through all the the touches on screen
			for(int i = 0; i < Input.touchCount; i++)
			{
				currTouch = i;
				Debug.Log(currTouch);
				//executes this code for current touch (i) on screen
				if(this.guiText != null && (this.guiText.HitTest(Input.GetTouch(i).position)))
				{
					//if current touch hits our guitexture, run this code
					if(Input.GetTouch(i).phase == TouchPhase.Began)
					{
						//need to send message b/c function is not present in this script
						//OnTouchBegan();
						this.SendMessage("OnTouchBegan");
					}
					if(Input.GetTouch(i).phase == TouchPhase.Ended)
					{
						//OnTouchEnded();
						this.SendMessage("OnTouchEnded");
					}
					if(Input.GetTouch(i).phase == TouchPhase.Moved)
					{
						//OnTouchMoved();
						this.SendMessage("OnTouchMoved");
					}
					if(Input.GetTouch(i).phase == TouchPhase.Stationary)
					{
						//OnTouchStayed();
						this.SendMessage("OnTouchStayed");
					}
				}
				
				//outside so it doesn't require the touch to be over the guitexture
				ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);//creates ray from screen point position
				switch(Input.GetTouch(i).phase)
				{
				case TouchPhase.Began:
					//OnTouchBeganAnywhere();
					this.SendMessage("OnTouchBeganAnyWhere");
					if(Physics.Raycast(ray, out rayHitInfo))
						rayHitInfo.transform.gameObject.SendMessage("OnTouchBegan3D");
					break;
				case TouchPhase.Ended:
					//OnTouchEndedAnywhere();
					this.SendMessage("OnTouchEndedAnywhere");
					if(Physics.Raycast(ray, out rayHitInfo))
						rayHitInfo.transform.gameObject.SendMessage("OnTouchEnded3D");
					break;
				case TouchPhase.Moved:
					//OnTouchMovedAnywhere();
					this.SendMessage("OnTouchMovedAnywhere");
					if(Physics.Raycast(ray, out rayHitInfo))
						rayHitInfo.transform.gameObject.SendMessage("OnTouchMoved3D");
					break;
				case TouchPhase.Stationary:
					//OnTouchStayedAnywhere();
					this.SendMessage("OnTouchStayedAnywhere");
					if(Physics.Raycast(ray, out rayHitInfo))
						rayHitInfo.transform.gameObject.SendMessage("OnTouchStayed3D");
					break;
				}
			}
		}
	}
}
