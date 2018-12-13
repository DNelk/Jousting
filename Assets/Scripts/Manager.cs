using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{

	private int levelIndex;

	public string[] levels;

	public int myIndex;

	public GameObject ui;

	private bool uiActive;

	private bool levelComplete;
	// Use this for initialization
	void Start ()
	{
		uiActive = false;
		if (ui)
		{
			ui.SetActive(uiActive);
		}

		levelComplete = false;
	}
	
	// Update is called once per frame
	void Update()
	{
		//if (Input.GetKeyDown(KeyCode.R))
		//{
			//SceneManager.LoadScene("1");
		//}
	}

	public void AdvanceState(bool success)
	{
		uiActive = true;
		ui.SetActive(uiActive);
		if(success)
		{
			levelComplete = true;
			GameObject.Find("failure").SetActive(false);
		}
		else
		{
			GameObject.Find("success").SetActive(false);
			GameObject.Find("next").SetActive(false);
		}
	}

	public void StartGame()
	{
		SceneManager.LoadScene("1");
	}

	public void Restart()
	{
		Debug.Log("restart");
		SceneManager.LoadScene(levels[myIndex]);
	}

	public void Next()
	{
		if(levels.Length-1 > myIndex)
			SceneManager.LoadScene(levels[myIndex+1]);
	}

	public bool IsComplete()
	{
		return levelComplete;
	}
}

