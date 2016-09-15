using UnityEngine;
using System.Collections;

public class DetectCollision : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col)
	{
		Debug.Log ("collide with: " + col.gameObject.name);
	}
}
