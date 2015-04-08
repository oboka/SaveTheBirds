using UnityEngine;
using System.Collections;

public class RandomMove : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector2 v2 = Random.insideUnitCircle * Time.deltaTime * 0.6f;
		Vector3 pos = transform.position;
		pos.x+=v2.x;
		pos.y+=v2.y;
		transform.position = pos;

	}
}
