using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {

	public enum GameState { Waiting, Playing, Paused, Ended };

	public Bird bird;
	public static GameState currentState;
	public UIManager ui_manager;
	public Canvas highscore_line;
	public BGMusicSingleton audio;
	public GameObject tap_tutorial;

	public float defaultTimeScale = 3;

	private static int game_counter = 0;

	public float best;
	public float score;


	void Start () {

		best = PlayerPrefs.GetFloat("best", 0);
		ui_manager.updateBestScore((int)best);

		initGame();
	}
	
	void Update () {

		if (Input.GetKeyUp(KeyCode.Escape))
		{
			if(currentState==GameState.Paused)
			{
				resumeGame();
			}else if(currentState==GameState.Ended)
			{
				playAgain();
			}else{
				pauseGame();
			}
		}	

		if(currentState==GameState.Playing)
		{
			score = Mathf.Floor(bird.transform.position.y);
			
			if(score<0) score = 0;
			if(score>best)
			{
				best = score;
				ui_manager.updateBestScore((int)best);
			}
			
			ui_manager.updateScore((int)score);
		}
	}

	public void initGame()
	{
		currentState = GameState.Waiting;

		float best = PlayerPrefs.GetFloat("best", 0);
		if(best<=10)
		{
			highscore_line.gameObject.SetActive(false);
		}else{
			highscore_line.transform.position = new Vector3(0,best,0);
		}

		Time.timeScale = defaultTimeScale;

		ui_manager.initGame();
		ui_manager.updateScore(0);
	}

	public void startFly()
	{
		currentState = GameState.Playing;
		hideTap();
	}

	void hideTap()
	{
		tap_tutorial.SetActive(false);
	}

	public void resumeGame()
	{
		currentState = GameState.Playing;
		Time.timeScale = defaultTimeScale;
		ui_manager.resumePause();
	}

	public void pauseGame()
	{
		currentState = GameState.Paused;
		Time.timeScale = 0;
		ui_manager.pauseGame();
	}

	public void birdCrash()
	{
		currentState = GameState.Ended;

		best = PlayerPrefs.GetFloat("best", 0);
		if(score>best)
		{
			best = score;
			PlayerPrefs.SetFloat("best", best);
		}

		ui_manager.gameFinished();
	}

	public void buttonToggleAudio()
	{
		audio.toggleAudio();
		ui_manager.setAudioState(audio.audio_enabled);
	}

	public void playAgain()
	{
		Application.LoadLevel("game");
		game_counter++;
	}

	public void buttonLeaderboard()
	{
	}


	void OnApplicationFocus(bool focusStatus) 
	{
		if(focusStatus)
		{
			if(currentState==GameState.Playing)
			{
				pauseGame();
			}
		}
	}


}
