using UnityEngine;
using System.Collections;

public class TouchManager : TouchBtnlogic 
{
	public TouchBtnlogic[] touches2Manage;
	
	void OnTouchEndedAnywhere()
	{
		foreach(TouchBtnlogic obj in touches2Manage)
			if(obj.touch2Watch > TouchBtnlogic.currTouch)
				obj.touch2Watch--;
	}
}
