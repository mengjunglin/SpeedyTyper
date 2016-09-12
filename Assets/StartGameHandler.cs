using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class StartGameHandler : MonoBehaviour {

    public GameObject square;
    public float spawnTime = 0.5f;
    public Dictionary<int, GameObject> spawnDictionary;
    public Dictionary<string, string> wordDictionary;
    public string[] wordDictKey;
    public int spawnIndex = 0;
    public int destroyIndex = 0;

    public string alph = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    public static string word;

    void Start()
    {
        spawnDictionary = new Dictionary<int, GameObject>();
        wordDictionary = new Dictionary<string, string>();
        wordDictionary.Add("LION", "LION");
        wordDictionary.Add("DUCK", "DUCK");
        wordDictionary.Add("GOAT", "GOAT");
        wordDictionary.Add("BIRD", "BIRD");
        wordDictionary.Add("FISH", "FISH");
        wordDictKey = new string[wordDictionary.Count];
        wordDictionary.Keys.CopyTo(wordDictKey, 0);

        /*Vector3 clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(50, 500, 0));
        clickPosition.z = 0;
        GameObject obj = Instantiate(square, clickPosition, Quaternion.identity) as GameObject;

        Vector3 cp = Camera.main.ScreenToWorldPoint(new Vector3(570, 500, 0));
        cp.z = 0;
        GameObject obj2 = Instantiate(square, cp, Quaternion.identity) as GameObject;*/

        InvokeRepeating("SpawnSquare", spawnTime, spawnTime);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(spawnDictionary[destroyIndex]);
            destroyIndex++;
        }
    }

    void SpawnSquare()
    {
        // Where to spawn
        Vector3 clickPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0,11)*48+50,500,0));
        clickPosition.z = 0;

        word = wordDictKey[Random.Range(0, wordDictionary.Count)];

        // Now we can actually spawn a bob object
        GameObject obj = Instantiate(square, clickPosition, Quaternion.identity) as GameObject;
        spawnDictionary.Add(spawnIndex, obj);
        spawnIndex++;
    }
}
