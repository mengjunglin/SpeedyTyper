﻿using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.fullScreen = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("return"))
			Application.LoadLevel ("GameScene");
	}
}
