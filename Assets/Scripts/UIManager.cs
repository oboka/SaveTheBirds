using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

	public Bird bird;

	public GameObject gamePanel;
	public GameObject pausePanel;
	public GameObject endGamePanel;

	public Text gameScore;
	public Text endScore;
	public Text endHighScore;

	public Image audio_on;
	public Image audio_off;


	void Start () {

	}
	

	void Update () {
	
	}

	public void updateBestScore(int best_score)
	{
		endHighScore.text = ""+best_score;
	}


	public void updateScore(int score)
	{
		gameScore.text = ""+score;
		endScore.text = ""+score;
	}

	public void setAudioState(bool audio_enabled)
	{
		audio_off.enabled = !audio_enabled;
		audio_on.enabled = audio_enabled;
	}

	public void pauseGame()
	{
		gamePanel.SetActive(false);
		pausePanel.SetActive(true);
		endGamePanel.SetActive(false);
	}


	public void initGame()
	{
		gamePanel.SetActive(true);
		pausePanel.SetActive(false);
		endGamePanel.SetActive(false);
	}

	public void gameFinished()
	{
		Invoke("activateEndPanel", 2.0f);
	}

	void activateEndPanel()
	{
		gamePanel.SetActive(false);
		pausePanel.SetActive(false);
		endGamePanel.SetActive(true);
	}


	public void resumePause()
	{
		gamePanel.SetActive(true);
		pausePanel.SetActive(false);
		endGamePanel.SetActive(false);
	}
	
}
