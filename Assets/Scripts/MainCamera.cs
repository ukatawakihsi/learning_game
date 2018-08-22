using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public float LIMIT_X;
	public float LIMIT_Y;

	// Use this for initialization
	void Start () {

	}

    // Update is called once per frame
    void Update () {
		Vector3 player_pos = GameObject.Find("PLAYER").transform.position;
		Vector3 update_pos = transform.position;

		if (Mathf.Abs(player_pos.x) <= LIMIT_X)
			update_pos.x = player_pos.x;

		if (Mathf.Abs(player_pos.y) <= LIMIT_Y)
			update_pos.y = player_pos.y;

		transform.position = update_pos;
	}
}
