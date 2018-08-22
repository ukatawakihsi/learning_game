using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour {

	enum Player_Status{
		STAND = 0,
		READY,
		RUN,
		DASH,
		ATTACK_SHOT,
		ATTACK_SIDE,
		ATTACK_VERTICAL_D,
		ATTACK_VERTICAL_U,
		DAMAGE,
		MAX
	};

	// variables =-=-=-=-=-=-=-=-

	// public
	public float SPEED;
	public float DBG_SPEED;
	public int CHANGE_READY_TIME;
	public int RESET_CNT;

	//Sprite
	public Sprite stand;
	public Sprite ready;
	public Sprite run;

	//Camera
	public Camera cam;

	//Debug
	public Player_debugText dbg_status;
	public Player_debugText dbg_cnt;
	public Player_debugText dbg_pos_x;
	public Player_debugText dbg_pos_y;
	public Player_debugText dbg_vec_x;
	public Player_debugText dbg_vec_y;
	public Player_debugText dbg_direction;

	// private 
	private int cnt;
	private int status;
	private float x,y;
	private SpriteRenderer MainSpriteRenderer;
	private bool direction; // false: left  true: right
	private bool updated;
	private Vector2 vec2;

	// private for mouse
	private Vector3 screenPoint;
	private Vector3 offset;

	// Function =-=-=-=-=-=-=-=-
	private void player_DebugLogUpdate()
	{

		Vector3 pos = GameObject.Find("PLAYER").transform.position;
		string update = "";

		dbg_cnt.dbg_var = cnt.ToString();

		dbg_pos_x.dbg_var = pos.x.ToString();
		dbg_pos_y.dbg_var = pos.y.ToString();
		dbg_vec_x.dbg_var = vec2.x.ToString();
		dbg_vec_y.dbg_var = vec2.y.ToString();

		//status
		switch (status) {
		case (int)Player_Status.STAND:
			update = "Stand";
			break;
		case (int)Player_Status.READY:
			update = "Ready";
			break;
		case (int)Player_Status.RUN:
			update = "Run";
			break;
		default:
			break;
		}
		dbg_status.dbg_var = update;
		dbg_direction.dbg_var = direction ? "RIGHT" : "LEFT";
	}

	void player_SpriteUpdate()
	{
		Sprite update = null;

		switch (status) {
		case (int)Player_Status.STAND:
			update = stand;
			break;
		case (int)Player_Status.READY:
			update = ready;
			break;
		case (int)Player_Status.RUN:
			update = run;
			break;
		case (int)Player_Status.DASH:
		default:
			break;
		}

		if (!update) {
			Debug.Log("Warning!@SpriteUpdate");
			return;
		}

		if (update != MainSpriteRenderer.sprite) {
			MainSpriteRenderer.sprite = update;
		} else {
			Debug.Log("Skip_SpriteUpdate");
		}
	}

	void UpdateDirection()
	{
		bool current_direction;
		Vector3 scale = transform.localScale;

		if (x > 0)
			current_direction = false; //left
		else if (x < 0)
			current_direction = true; //right
		else
			return;

		if (direction != current_direction) {
			direction = current_direction;
			scale.x = scale.x * -1.0f;
			transform.localScale = scale;
		}			
	}

	// Use this for initialization
	void Start ()
	{
		cnt = 0;
		status = 0;
		updated = false;
		direction = false;
		MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
	}


	//MOUSE=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
	void OnMouseDown()
	{
		screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}
	void OnMouseDrag()
	{
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
		//x = (float) (transform.position.x + currentPosition.x);
		//y = (float) (transform.position.y + currentPosition.y);
		transform.position = currentPosition;

		updated = true;
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		Rigidbody2D rb = GetComponent<Rigidbody2D>();

		if (!updated) {
			x = Input.GetAxis("Horizontal") * DBG_SPEED;
			y = Input.GetAxis("Vertical") * DBG_SPEED;
		}

		UpdateDirection();

		vec2 = new Vector2 (x, y);

		if (vec2.x == 0.0f && vec2.y == 0.0f) {
			cnt++;
			if (status != (int)Player_Status.READY)
				status = (int)Player_Status.STAND;
		} else {
		cnt = 0;
			status = (int)Player_Status.RUN;
		}

		if (cnt > CHANGE_READY_TIME && status != (int)Player_Status.READY)  {
			status = (int)Player_Status.READY;
			cnt = 0;
		} else if (cnt > RESET_CNT) {
			cnt = 0;
		}

		//Object update =-=-=-=-=-=-=-=
		rb.velocity = vec2;
		player_SpriteUpdate();
		player_DebugLogUpdate();

		// reset
		x = 0.0f;
		y = 0.0f;
		updated = false;
	}



}
