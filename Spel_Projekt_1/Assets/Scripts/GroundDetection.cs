﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class GroundDetection : MonoBehaviour
{
	public List<SoundAndGround> sounds = new List<SoundAndGround>();
	[Min(0)]
	public Animator ani;
	public Tilemap tileMap;
	private Vector3Int gridCoords;
	private GridLayout grid;
	private Tile tile;
	private GameObject player;

	public void Start()
	{
		player = PlayerStatic.playerInstance;
		grid = tileMap.layoutGrid;
	}

	public void CheckSound()
	{
		if (ani.GetBool("movement"))
		{
			gridCoords = grid.WorldToCell(player.transform.position);
			tile = tileMap.GetTile(gridCoords) as Tile;
			for (int i = 0; i < sounds.Count; i++)
			{
				if (tile.sprite == sounds[i].spriteOfTile)
				{
					PlaySound(sounds[i].nameOfSound);
				}
			}
		}
	}

	private void PlaySound(string sound)
	{
		FMODUnity.RuntimeManager.PlayOneShot(sound);
	}
}

[System.Serializable]
public struct SoundAndGround
{
	[FMODUnity.EventRef]
	public string nameOfSound;
	public Sprite spriteOfTile;
}