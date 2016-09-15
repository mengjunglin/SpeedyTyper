using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartGameHandler : MonoBehaviour {

    public GameObject square;
    public InputField inputField;
    public static int score = 0;
    public Text txtscore;
	public Text txtSpeed;
    public GameObject[] groups;
    public float spawnTime;
    public static Dictionary<string, GameObject> spawnDictionary;
    public Dictionary<string, string> wordDictionary;
    public string[] wordDictKey;
    public int spawnIndex = 0;
    public int destroyIndex = 0;
	public bool spawn = true;
	public GameObject deadLine;
	public TextAsset textAsset;
	public Slider sSpeed;
	public static float currentSpeed;

    public string alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string word = string.Empty;
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
        spawnDictionary = new Dictionary<string, GameObject>();
        wordDictionary = BuildDictionary();
        wordDictKey = new string[wordDictionary.Count];
        wordDictionary.Keys.CopyTo(wordDictKey, 0);

        inputField.Select();
		score = 0;
		currentSpeed = sSpeed.value;
		spawnTime = SpeedScale[currentSpeed];

        //InvokeRepeating("SpawnSquare", spawnTime, spawnTime);
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
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			sSpeed.value += 1f;
		}
		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			sSpeed.value -= 1f;
		}
		currentSpeed = sSpeed.value;
        txtscore.text = score.ToString();
		spawnTime = SpeedScale[currentSpeed];
		txtSpeed.text = currentSpeed.ToString ();
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
			score += 300 * (int)currentSpeed;
        }
        else
			score -= 100 * (int)currentSpeed;
    }

	public void TogglePause()
	{
		spawn = !spawn;
		inputField.enabled = !inputField.enabled;
		Debug.Log ("toggle="+spawn);
	}

	void GameOver()
	{
		spawn = !spawn;
		inputField.enabled = !inputField.enabled;
		Debug.Log ("Game Over D:");
		Application.LoadLevel ("GameOverScene");
	}
}
