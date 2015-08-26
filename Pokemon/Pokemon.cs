using UnityEngine;
using System.Collections;

public class Pokemon {

	public string name;
	public string trainer;

	public uint hp;
	public uint maxHp;

	public uint attack;
	public uint defense;
	public uint specialAttack;
	public uint specialDefense;
	public uint initiative;

	public uint level;
	public uint experience;

	public uint id;
	public uint pokedexId;

	public Type[] types;
	public Attack[] attacks;

	public Item item;

	public Time timeCaptured;
	public string placeCaptured;
	public uint levelCaptured;

	public Sprite picture;

	public enum Type {
		normal,
		fire,
		water,
		electric,
		grass,
		ice,
		fighting,
		poison,
		ground,
		flying,
		psychic,
		bug,
		rock,
		ghost,
		dragon,
		dark,
		steel,
		fairy,
	};
}
