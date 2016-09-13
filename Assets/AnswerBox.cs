using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AnswerBox : MonoBehaviour {
    InputField input;

	// Use this for initialization
	void Start () {
        input = GetComponent<InputField>();
        var submitEvent = new InputField.SubmitEvent();
        submitEvent.AddListener(SubmitAnswer);
        input.onEndEdit = submitEvent;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void SubmitAnswer(string arg0)
    {
        Debug.Log(arg0);
        StartGameHandler.CheckAndDestroy(arg0.ToUpper());
        input.text = "";
        input.OnPointerClick(new PointerEventData(EventSystem.current));
    }
}
