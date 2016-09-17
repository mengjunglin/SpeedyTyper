using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartGameHandler : MonoBehaviour {
	private static Dictionary<string, GameObject> spawnDictionary; //keeps track of the objects that are spawned
	private Dictionary<string, string> wordDictionary; //store words from the text file
	private string[] wordDictKey;
	private float spawnTime; //time to wait before spawning the next object
	private bool spawn = true;

	// Controls on the game scene
    public GameObject square;
    public InputField inputField;
    public Text txtscore;
	public Text txtSpeed;
	public TextAsset textAsset;
	public Slider sSpeed;

	//public static variables that will need to be accessed from another class
	public static int score = 0;
	public static float currentSpeed;
    public static string word = string.Empty;

	// mapping the speed shown on the game scene and the actual spawn time
	public Dictionary<float, float> SpeedScale = new Dictionary<float, float> ()
	{
		{1f, 2.5f}, 
		{2f, 2f}, 
		{3f, 1.5f}, 
		{4f, 1f}, 
		{5f, 0.5f}
	};

    void Start()
    {
		// initializa the dictionary
        spawnDictionary = new Dictionary<string, GameObject>();
        wordDictionary = BuildDictionary();
        wordDictKey = new string[wordDictionary.Count];
        wordDictionary.Keys.CopyTo(wordDictKey, 0);

		// reset the game scene
        inputField.Select();
		score = 0;
		currentSpeed = sSpeed.value;
		spawnTime = SpeedScale[currentSpeed];

		StartCoroutine(Spawnning());
    }

	IEnumerator Spawnning()
	{
		while (true) {
			yield return new WaitForSeconds (spawnTime);
			SpawnSquare ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		// updating speed of the game
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			sSpeed.value += 1f;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			sSpeed.value -= 1f;
		}

		// updating values on UI
		currentSpeed = sSpeed.value;
        txtscore.text = score.ToString();
		spawnTime = SpeedScale[currentSpeed];
		txtSpeed.text = currentSpeed.ToString ();
    }

	// Build the dictionary from the TextAsset, which is the text file containing the wordrs
    Dictionary<string, string> BuildDictionary()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string[] lines;
		lines = textAsset.text.Split ('\n');
		foreach(string l in lines)
			dict.Add(l, l);

        return dict;
    }

	// spawing the square/object
    void SpawnSquare()
    {
		if (spawn) {
			word = wordDictKey [Random.Range (0, wordDictionary.Count)];

			// Where to spawn
			Vector3 spawnPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, 9) * 65 + 130, 550, 0)); //130-650
			spawnPosition.z = 0;

			// Check if spawning will collide with existing block
			RectTransform rt = (RectTransform)square.transform;
			float w = rt.rect.width;
			float h = rt.rect.height;

			// game over if object will overlap another object at spawning
			if (Physics2D.OverlapBox (spawnPosition, new Vector2 (w, h), 0f) != null)
				GameOver ();
			else {
				GameObject obj = Instantiate (square, spawnPosition, Quaternion.identity) as GameObject;
				spawnDictionary.Add (word, obj);
				Debug.Log (word);
			}
		}
    }

    public static void CheckAndDestroy(string key)
    {
        if (spawnDictionary.ContainsKey(key))
        {
            Destroy(spawnDictionary[key]);
            spawnDictionary.Remove(key);
			score += 300 * (int)currentSpeed;
        }
        else
			score -= 100 * (int)currentSpeed;
    }

	public void TogglePause()
	{
		spawn = !spawn;
		inputField.enabled = !inputField.enabled;
	}

	void GameOver()
	{
		spawn = !spawn;
		inputField.enabled = !inputField.enabled;
		Debug.Log ("Game Over D:");
		Application.LoadLevel ("GameOverScene");
	}
}
