using UnityEngine;
using System.Collections;

public class CloudLogo : MonoBehaviour {

	public float wind;
	private float speed_factor = 1.0f;

	// Use this for initialization
	void Start () {
		speed_factor = Random.Range(0.5f, 2.0f);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x += wind*speed_factor*0.02f;
		if(pos.x>18) pos.x = -18;
		//Debug.Log (pos.x);
		transform.position = pos;

	}
}
