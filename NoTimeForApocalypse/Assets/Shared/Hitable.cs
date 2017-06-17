using UnityEngine;

public abstract class Hitable : MonoBehaviour{
	public abstract void Hit(GameObject source, float damage = 0, Vector2 directionAngle = new Vector2()); //hit
}
