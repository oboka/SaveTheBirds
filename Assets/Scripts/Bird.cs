using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Bird : MonoBehaviour {

	public GameStateManager gameManager;

	public float force = 1;
	private bool dead = false;
	private float minx = 0;
	private float maxx = 0;

	float best_score = 0;

	bool flying = false;

	public float force_x = 1;
	public float force_y = 10;

	Animator anim;

	public AudioSource effect_jump;
	public AudioSource effect_hit_branch;
	public AudioSource effect_hit_bee;
	public AudioSource effect_fall;

	public bool invincible = false;

	public ParticleSystem particles;
	public ParticleSystem plumas;
	public ParticleSystem plumas_dead;
	public ParticleSystem stars_dead;


	void Start () 
	{


		dead = false;

		rigidbody2D.isKinematic = true;

		float camw = (Screen.width+0.0f)*Camera.main.orthographicSize/(Screen.height+0.0f);
		minx = -camw;
		maxx = camw;

		anim = GetComponent<Animator>();

	}
	
	void Update () 
	{

		if(!dead)
		{
			bool left = false;
			bool right = false;

			for(int i=0;i<Input.touchCount;i++)
			{
				Touch t = Input.GetTouch(i);
				if(t.phase==TouchPhase.Began)
				{
					if(t.position.y<Screen.height*0.8f)
					{
						if(t.position.x < Screen.width/2)
						{
							left = true;
						}else{
							right = true;
						}
					}
				}
			}

			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				left = true;
			}else if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				right = true;
			}

			if(left)
			{
				if(!flying)
				{
					anim.SetTrigger("fly");
					flying = true;
					gameManager.startFly();
				}
				transform.localScale = new Vector3(1,1,1);
				rigidbody2D.isKinematic = false;
				rigidbody2D.velocity = Vector2.zero;
				rigidbody2D.AddForce(new Vector2(-force_x, force_y)*force);
				transform.localRotation = Quaternion.Euler(0,0,15);
				AudioJump();

			}else if(right)
			{
				if(!flying)
				{
					anim.SetTrigger("fly");
					flying = true;
					gameManager.startFly();
				}

				transform.localScale = new Vector3(-1,1,1);
				rigidbody2D.isKinematic = false;
				rigidbody2D.velocity = Vector2.zero;
				rigidbody2D.AddForce(new Vector2(force_x, force_y)*force);
				transform.localRotation = Quaternion.Euler(0,0,-15);
				AudioJump();
			}

			if(transform.position.x<minx || transform.position.x>maxx)
			{
				Vector2 v2 = rigidbody2D.velocity;
				v2.x = -v2.x;
				rigidbody2D.velocity = v2;
			}
		}
	}


	void AudioJump()
	{
		plumas.emissionRate = Random.Range(0,4);
		plumas.Play();
		particles.Play();
		effect_jump.Stop();
		effect_jump.Play();
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(!dead && !invincible)
		{
			if(col.gameObject.name.StartsWith("Wall"))
			{
				effect_hit_branch.Stop();
				effect_hit_branch.Play();
			}else{
				effect_hit_bee.Stop();
				effect_hit_bee.Play();
			}

			effect_fall.Stop();
			effect_fall.Play();

			plumas_dead.Play();
			stars_dead.Play();

			dead = true;
			rigidbody2D.velocity = Vector2.zero;
			rigidbody2D.AddTorque(50);

			gameManager.birdCrash();
		}
	}

}
