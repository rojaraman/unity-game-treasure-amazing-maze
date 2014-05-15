using UnityEngine;
using System.Collections;

public class touchnewgamebtn :TouchBtnlogic
{
	void OnTouchBegan()
	{
		Application.LoadLevel ("_scene");
	}
}
