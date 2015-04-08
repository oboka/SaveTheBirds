using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {

	public Transform bird;
	public SpriteRenderer sky_texture;
	public Color[] sky_colors;
	public float color_distance;

	Camera cam;

	float time;
	int frames;

	int last_gallo = 0;

	public AudioSource effect_night;
	public AudioSource effect_gallo;

	public Transform mountains;

	// Use this for initialization
	void Start () {
		cam = camera;
	}
	
	// Update is called once per frame
	void Update () {

		time+=Time.deltaTime;
		frames++;

		float fps = Time.timeScale*frames/time;

		if(time>3)
		{
			frames = 0;
			time = 0;
		}

		float new_pos = bird.position.y + this.camera.orthographicSize / 2;
		if(new_pos>transform.position.y)
		{
			Vector3 v3 = transform.position;
			float dif = new_pos-transform.position.y;
			v3.y = new_pos;
			transform.position = v3;

			v3 = mountains.position;
			v3.y-=dif/4.0f;
			mountains.position = v3;

		}

		float current = transform.position.y / color_distance;
		Color c = getColorLerp(current);
		cam.backgroundColor = c;

		c = getColorLerp(current+1);
		sky_texture.color = c;

		int current_color = Mathf.FloorToInt(current);
		int real_current = current_color;
		float current_lerp = current - current_color;
		current_color = current_color%sky_colors.Length;

		current = current_color+current_lerp;

		if(current>=4 && current<=9)
		{
			if(!effect_night.isPlaying)
			{
				effect_night.Play();
			}

			if(current>=4 && current<=5)
			{
				float v = (current-4.0f)/2.0f;
				effect_night.volume = v;
			}else if(current>=8)
			{
				effect_night.volume = 9.0f-current;
			}else{
				effect_night.volume = 1;
			}

			effect_night.volume *= 0.5f;
		}else if(current_color==14 && last_gallo<real_current)
		{
			last_gallo = real_current;
			effect_gallo.Play();
		}else{
			if(effect_night.isPlaying)
			{
				effect_night.Stop();
			}
		}

	}

	Color getColorLerp(float current)
	{
		int current_color = Mathf.FloorToInt(current);
		float current_lerp = current - current_color;
		current_color = current_color%sky_colors.Length;
		Color c1 = sky_colors[current_color];
		current_color++;
		if(current_color>=sky_colors.Length) current_color = 0;
		Color c2 = sky_colors[current_color];
		
		Color c = Color.Lerp(c1, c2, current_lerp);	

		//Debug.Log(current+" "+current_lerp);

		return c;
	}
}
