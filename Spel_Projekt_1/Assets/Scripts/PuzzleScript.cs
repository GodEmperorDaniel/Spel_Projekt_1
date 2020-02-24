﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using Fungus;

public class PuzzleScript : MonoBehaviour
{
    [SerializeField] protected Button startButton = null;

	[SerializeField] protected TextMeshProUGUI showPuzzleGuess = null;

   // [SerializeField] protected List<puzzleAndScene> sceneAndNumber = new List<puzzleAndScene>();

    private string puzzleCombination;

	private GameObject lastSelectedButton = null;


    private void Start()
    {
        startButton.Select();
    }

    void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
		}

		AlwaysSelected();

		if (showPuzzleGuess != null)
		{
			showPuzzleGuess.text = puzzleCombination;
		}
	}

	private void AlwaysSelected()
	{
		foreach (Selectable button in Button.allSelectablesArray)
		{
			if (button.gameObject == EventSystem.current.currentSelectedGameObject)
			{
				lastSelectedButton = button.gameObject;
			}
		}
		if (!EventSystem.current.alreadySelecting)
		{
			EventSystem.current.SetSelectedGameObject(lastSelectedButton);
		}
	}

	//public void AddSceneAndPuzzle(puzzleAndScene number)
	//   {
	//       sceneAndNumber.Add(number);
	//   }

	//   public void RemoveSceneAndPuzzle(puzzleAndScene number)
	//   {
	//       sceneAndNumber.Remove(number);
	//   }

	//   public void AddPuzzlePiece(string puzzleCharacter)
	//   { 
	//       puzzleCombination += puzzleCharacter;
	//   }

	//   public void CheckNumber()
	//   {
	//       for (int i = 0; i < sceneAndNumber.Count; i++)
	//       {
	//           if (puzzleCombination == sceneAndNumber[i].puzzleSolution)
	//           {
	//               PlayerStatic.freezePlayer = false;
	//               SceneManager.LoadScene(sceneAndNumber[i].nameOfNextScene);
	//           }
	//           else
	//           {
	//               Debug.Log("No signal on this number... Try another");
	//           }
	//       }
	//       puzzleCombination = null;
	//   }

	private void CallBlock(InteractionSettings settings)
	{
		if (settings.flowchart && settings.block)
		{
			settings.flowchart.ExecuteBlock(settings.block);
		}
	}
}

[System.Serializable]
public struct PuzzleAndScene
{
    public string puzzleSolution;

	public Flowchart flowchart;

	[HideInInspector] public Block block;

	[HideInInspector] public bool _foldout;
	[HideInInspector] public bool _showPopup;
}
