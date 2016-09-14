using UnityEngine;
using System.Collections;

public class WordOnGroup : MonoBehaviour {

    public string wordOnGroup;
    public static string char0;
    public static string char1;
    public static string char2;
    public static string char3;

    // Use this for initialization
    void Start () {
        wordOnGroup = StartGameHandler.word;
		if (wordOnGroup != string.Empty) {
			char0 = wordOnGroup [0].ToString ();
			char1 = wordOnGroup [1].ToString ();
			char2 = wordOnGroup [2].ToString ();
			char3 = wordOnGroup [3].ToString ();
		}
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
