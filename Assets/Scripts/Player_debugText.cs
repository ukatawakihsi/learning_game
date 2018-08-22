using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_debugText : MonoBehaviour {

	//public
	public string dbg_name;
	public string dbg_var;
	//private 


	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		this.GetComponent<Text>().text = dbg_name + " : " + dbg_var;
	}
}
