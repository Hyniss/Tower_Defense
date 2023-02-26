using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
	[SerializeField]
	private AudioClip arrow;
	[SerializeField]
	private AudioClip death;
	[SerializeField]
	private AudioClip fireball;
	[SerializeField]
	private AudioClip gameover;
	[SerializeField]
	private AudioClip hit;
	[SerializeField]
	private AudioClip level;
	[SerializeField]
	private AudioClip newGame;
	[SerializeField]
	private AudioClip rock;
	[SerializeField]
	private AudioClip towerBuilts;

	public AudioClip Arrow { get => arrow; }
	public AudioClip Death { get => death; }
	public AudioClip FireBall { get => fireball; }
	public AudioClip GameOver { get => gameover; }
	public AudioClip Hit { get => hit; }
	public AudioClip Level { get => level; }
	public AudioClip NewGame { get => newGame; }
	public AudioClip Rock { get => rock; }
	public AudioClip TowerBuilts { get => towerBuilts; }
}
