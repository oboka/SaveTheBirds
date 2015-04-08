using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	Transform camera_transform;
	float camera_height;

	void Start()
	{
		camera_transform = Camera.main.transform;
		camera_height = Camera.main.orthographicSize*2;
	}

	void Update()
	{
		if(transform.position.y < camera_transform.position.y - camera_height)
		{
			Destroy(gameObject);
		}
	}
}
