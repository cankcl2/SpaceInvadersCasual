using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	private static UIManager instance;

	public TextMeshProUGUI scoreText;
	private int score;
	public TextMeshProUGUI highscoreText;
	private int highscore;
	public TextMeshProUGUI coinsText;
	public TextMeshProUGUI waveText;
	private int wave;
	public Image[] lifeSprites;
	public Image healthBar;
	public Sprite[] healthBars;
	private Color32 active = new Color(1, 1, 1, 1);
	private Color32 inactive = new Color(1, 1, 1, 0.25f);
	public GameObject secimPanel;
	

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public static void UpdateLives (int l)
	{
		foreach (Image i in instance.lifeSprites)
		{
			i.color = instance.inactive;
		}
		for (int i = 0; i < l; i++)
		{
			instance.lifeSprites[i].color = instance.active;
		}
	}
	public static void UpdateHealthBar (int h)
	{
		instance.healthBar.sprite = instance.healthBars[h];
	}
	public static void UpdateScore(int s)
	{
		instance.score += s;
		instance.scoreText.text = instance.score.ToString("000,000");
	}
	public static void UpdateHighScore()
	{
		// TODO
	}
	public static void UpdateWave()
	{
		instance.wave++;
		instance.waveText.text = instance.wave.ToString();
	}
	public static void UpdateCoins()
	{
		instance.coinsText.text = Inventory.currentCoins.ToString();
	}

	public static int GetWave()
    {
		return instance.wave;
    }
	public static GameObject GetSecimPanel()
    {
		return instance.secimPanel;
    }
}
