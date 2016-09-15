using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartGameHandler : MonoBehaviour {

    public GameObject square;
    public InputField inputField;
    public static int score = 0;
    public Text txtscore;
    public GameObject[] groups;
    public float spawnTime = 0.5f;
    public static Dictionary<string, GameObject> spawnDictionary;
    public Dictionary<string, string> wordDictionary;
    public string[] wordDictKey;
    public int spawnIndex = 0;
    public int destroyIndex = 0;
	public bool spawn = true;
	public GameObject deadLine;
	public TextAsset textAsset;

    public string alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string word = string.Empty;

    void Start()
    {
        spawnDictionary = new Dictionary<string, GameObject>();
        wordDictionary = BuildDictionary();
        wordDictKey = new string[wordDictionary.Count];
        wordDictionary.Keys.CopyTo(wordDictKey, 0);

        inputField.Select();
		score = 0;

        InvokeRepeating("SpawnSquare", spawnTime, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {
        txtscore.text = score.ToString();
    }

    Dictionary<string, string> BuildDictionary()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string[] lines;
		lines = textAsset.text.Split ('\n');
		foreach(string l in lines)
			dict.Add(l, l);

        return dict;
    }

    void SpawnSquare()
    {
		if (spawn) {
			word = wordDictKey [Random.Range (0, wordDictionary.Count)];
			// Where to spawn
			//Vector3 clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(650, 1000, 0));
			Vector3 spawnPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, 9) * 65 + 130, 550, 0)); //130-650
			spawnPosition.z = 0;

			// Check if spawning will collide with existing block
			RectTransform rt = (RectTransform)square.transform;
			float w = rt.rect.width;
			float h = rt.rect.height;
			if(Physics2D.OverlapBox (spawnPosition, new Vector2(w, h), 0f) != null)
				GameOver ();

			GameObject obj = Instantiate (square, spawnPosition, Quaternion.identity) as GameObject;
			spawnDictionary.Add (word, obj);
			Debug.Log (word);
		}
    }

    public static void CheckAndDestroy(string key)
    {
        if (spawnDictionary.ContainsKey(key))
        {
            Destroy(spawnDictionary[key]);
            spawnDictionary.Remove(key);
            score += 500;
        }
        else
            score -= 100;
    }

	public void TogglePause()
	{
		spawn = !spawn;
		inputField.enabled = !inputField.enabled;
		Debug.Log ("toggle");
	}

	void GameOver()
	{
		spawn = !spawn;
		inputField.enabled = !inputField.enabled;
		Debug.Log ("Game Over D:");
		Application.LoadLevel ("GameOverScene");
	}
}
