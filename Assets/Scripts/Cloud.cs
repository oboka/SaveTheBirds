using UnityEngine;
using System.Collections;

public class Cloud : MonoBehaviour {

	public static float wind;
	private float speed_factor = 1.0f;


	// Use this for initialization
	void Start () {
		speed_factor = Random.Range(0.5f, 2.0f);
		int r = Random.Range(0,2);
		if(r==1)
		{
			Vector3 v3 = transform.localScale;
			v3.x = -1;
			transform.localScale = v3;
		}

		SpriteRenderer sr = (SpriteRenderer)renderer;

		Color c = Camera.main.backgroundColor;

		c.r+=0.1f; if(c.r>1) c.r = 1;
		c.g+=0.1f; if(c.g>1) c.g = 1;
		c.b+=0.1f; if(c.b>1) c.b = 1;

		sr.color = c;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x += wind*speed_factor*0.02f;
		transform.position = pos;
	}
}
