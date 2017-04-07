using UnityEngine;

public abstract class Hitable : MonoBehaviour{
	public abstract void hit(GameObject source, float damage = 0, float directionAngle = 0); //hit
}
