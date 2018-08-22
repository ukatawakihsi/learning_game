using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public Player_debugText dbg_pos_x;
	public Player_debugText dbg_pos_y;
	public Player_debugText dbg_target;

	// Use this for initialization
	void Start ()
	{
		
	}

		private void OnMouseDown()
		{
			Vector2 currentTouchPos;
			Touch touch_info;

//			if (Input.touchCount > 0)
//			{
				touch_info = Input.GetTouch(0);
				currentTouchPos = new Vector2(touch_info.position.x, Screen.height - touch_info.position.y);
				dbg_pos_x.dbg_var = currentTouchPos.x.ToString();
				dbg_pos_y.dbg_var = currentTouchPos.y.ToString();
//			}

		}

	void TouchInputUpdate()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		TouchInputUpdate();
	}
}
