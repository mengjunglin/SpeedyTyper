using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UpdateSquareText : MonoBehaviour {
    Text textObject;

	// Use this for initialization
	void Start () {
        textObject = GetComponent<Text>();
        textObject.text = StartGameHandler.word;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
