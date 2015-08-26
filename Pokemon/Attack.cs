using UnityEngine;
using System.Collections;

public class Attack {

	public string aname;
	public string description;

	public int ap;
	public uint maxAp;

	public uint strenght;
	public uint precise;

	// -HP = strenght * attack / defense

	public Type type;

	public enum Type {
		physical,
		special,
		status,
	};
}
