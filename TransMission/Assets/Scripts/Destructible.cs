using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour {

	public bool destroyedByGroundPound;
	public bool destroyedByAttack;

	void OnTriggerEnter2D(Collider2D other) {
		bool destroy = false;
		if (other.tag == "Player") {
			Player player = other.gameObject.GetComponent<Player> ();
			destroy = 
				(destroyedByGroundPound && ((Player)other.transform.gameObject.GetComponent<Player>()).groundPound) 
				|| (destroyedByAttack && ((Player)other.transform.gameObject.GetComponent<Player>()).attacking);
		}
		if (destroy) {
			Destroy (this.gameObject);
		}
	}
}
