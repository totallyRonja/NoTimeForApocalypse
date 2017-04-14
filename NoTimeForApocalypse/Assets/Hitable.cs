using UnityEngine;

public abstract class Hitable : MonoBehaviour{
	public abstract void Hit(GameObject source, float damage = 0, float directionAngle = 0); //hit
}
