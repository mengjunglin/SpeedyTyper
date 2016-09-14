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
	public Button pauseButton;

    public string alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string word = string.Empty;

    void Start()
    {
        spawnDictionary = new Dictionary<string, GameObject>();
        /*wordDictionary = new Dictionary<string, string>();
        wordDictionary.Add("LION", "LION");
        wordDictionary.Add("DUCK", "DUCK");
        wordDictionary.Add("GOAT", "GOAT");
        wordDictionary.Add("BIRD", "BIRD");
        wordDictionary.Add("FISH", "FISH");*/
        wordDictionary = BuildDictionary();
        wordDictKey = new string[wordDictionary.Count];
        wordDictionary.Keys.CopyTo(wordDictKey, 0);

        inputField.Select();

        /*Vector3 clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(60, 500, 0));
        clickPosition.z = 0;
        GameObject obj = Instantiate(square, clickPosition, Quaternion.identity) as GameObject;

        Vector3 cp = Camera.main.ScreenToWorldPoint(new Vector3(110, 500, 0));
        cp.z = 0;
        GameObject obj2 = Instantiate(square, cp, Quaternion.identity) as GameObject;*/

        InvokeRepeating("SpawnSquare", spawnTime, spawnTime);

		pauseButton.onClick.AddListener (() => { //something is not working here...
			TogglePause();
		});
    }
	
	// Update is called once per frame
	void Update () {
        txtscore.text = score.ToString();
        /*if (Input.GetMouseButtonDown(0))
        {
            Destroy(spawnDictionary[word]);
            destroyIndex++;
        }

        if(Input.GetKeyDown("return"))
        {
            Destroy(spawnDictionary[word]);
            destroyIndex++;
        }*/
    }

    Dictionary<string, string> BuildDictionary()
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        string line;

        System.IO.StreamReader file = new System.IO.StreamReader("Assets/wordDictionary.txt");
        while ((line = file.ReadLine()) != null)
        {
            dict.Add(line, line);
        }
        file.Close();

        return dict;
    }

    void SpawnSquare()
    {
		if (spawn) {
			word = wordDictKey [Random.Range (0, wordDictionary.Count)];
			// Where to spawn
			//Vector3 clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(650, 1000, 0));
			Vector3 clickPosition = Camera.main.ScreenToWorldPoint (new Vector3 (Random.Range (0, 9) * 65 + 130, 1000, 0)); //130-650
			clickPosition.z = 0;
			GameObject obj = Instantiate (square, clickPosition, Quaternion.identity) as GameObject;
			spawnDictionary.Add (word, obj);
			Debug.Log (word);

			//word = wordDictKey[Random.Range(0, wordDictionary.Count)];
			/*if (!spawnDictionary.ContainsKey(word))
        {
            // Now we can actually spawn a bob object
            GameObject obj = Instantiate(groups[0], clickPosition, Quaternion.identity) as GameObject;
            //obj.transform.localScale = new Vector3(2.0f, 1.0f, 1.0f);
            spawnDictionary.Add(word, obj);
            spawnIndex++;
        }*/
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
}
