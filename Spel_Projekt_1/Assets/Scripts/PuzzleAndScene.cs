﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[System.Serializable]
public struct PuzzleAndScene
{
	public Flowchart flowchart;

	[HideInInspector] public Block block;

	[HideInInspector] public bool _foldout;
	[HideInInspector] public bool _showPopup;
}