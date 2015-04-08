using UnityEngine;
using System.Collections;

public class WorldCreator : MonoBehaviour {

	public GameObject wall;
	public GameObject block;
	public GameObject spider;
	public float wall_distance;
	public Transform bird;
	public GameObject[] clouds;

	private bool first_time = true;

	float last_wall = 0;

	// Use this for initialization
	void Start () {
		Cloud.wind = Random.Range(-2.0f,2.0f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(bird.position.y+30>last_wall)
		{
			while(last_wall < bird.position.y+30)
			{
				int spider_prob = Random.Range(0,3);
				
				float pos = Random.Range(-3.0f, 3.0f);
				last_wall += wall_distance;
				Instantiate(wall, new Vector3(pos-13, last_wall, 0), Quaternion.identity);
				if(!first_time)
				{
					if(spider_prob==0 || spider_prob==1)
					{
						Instantiate(block, new Vector3(pos+Random.Range(-3.0f, 3.0f), last_wall-4, 0), Quaternion.identity);
					}
				}

				Instantiate(block, new Vector3(pos+Random.Range(-3.0f, 3.0f), last_wall+4, 0), Quaternion.identity);

				if(spider_prob==1 || spider_prob==2)
				{
					int val = Random.Range(0,2);
					float sep = Random.Range(1.0f, 6.0f);

					GameObject sp = (GameObject)Instantiate(spider, new Vector3(pos + 3.9f*(2*val-1), last_wall-sep, 0), Quaternion.identity);
					Spider s = sp.GetComponent<Spider>();
					s.setCableDist(sep);
				}

				first_time = false;

				int cloud_num = Random.Range(0,4);

				for(int i=0;i<cloud_num;i++)
				{
					Instantiate(clouds[Random.Range(0,clouds.Length)], new Vector3(Random.Range(1.0f, 3.0f)*(Random.Range(0,2)*2-1)*2.0f, last_wall+Random.Range(0.0f, wall_distance)-2.0f, 0), Quaternion.identity);
				}
			}
		}
	}
}
