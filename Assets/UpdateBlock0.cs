﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateBlock0 : MonoBehaviour {

    Text blockText;

	// Use this for initialization
	void Start () {
        blockText = GetComponent<Text>();
        blockText.text = WordOnGroup.char0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
