using UnityEngine;
using System.Collections;

public class BGMusicSingleton : MonoBehaviour {

	public AudioSource music;
	public UIManager ui_manager;

	private static BGMusicSingleton instance = null;
	
	public static BGMusicSingleton GetInstance() {
		return instance;
	}

	public bool audio_enabled = true;

	void Awake()
	{
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}

		DontDestroyOnLoad(this.gameObject);
	}

	void Start () 
	{
		audio_enabled = (PlayerPrefs.GetInt("audio_enabled", 1)==1);

		ui_manager.setAudioState(audio_enabled);

		AudioListener.pause = !audio_enabled;

		if(audio_enabled)
		{
			music.Play();
		}
	}

	public void toggleAudio()
	{
		audio_enabled = !audio_enabled;

		AudioListener.pause = !audio_enabled;
		
		PlayerPrefs.SetInt("audio_enabled", audio_enabled?1:0);
		
		AudioSource music = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
		if(audio_enabled)
		{
			music.Play();
		}else{
			music.Stop();
		}
	}
	
	void OnApplicationPause(bool pause) 
	{
		bool audio_enabled = (PlayerPrefs.GetInt("audio_enabled", 1)==1);
		AudioListener.pause = !audio_enabled;
	}
}
