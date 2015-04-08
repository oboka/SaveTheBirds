using UnityEngine;
using System.Collections;

public class Spider : MonoBehaviour {

	public Transform cable;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setCableDist(float s)
	{
		Vector3 v = Vector3.one;
		v.y = s*2.3f;
		cable.localScale = v;
	}
}
