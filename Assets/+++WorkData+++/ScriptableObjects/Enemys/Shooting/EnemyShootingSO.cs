using UnityEngine;

public abstract class EnemyShootingSO : ScriptableObject
{
    public abstract bool ShouldShoot(EnemyBehavior enemy);
}