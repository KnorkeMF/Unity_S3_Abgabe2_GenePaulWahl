using UnityEngine;

public abstract class EnemyMovementSO : ScriptableObject
{
    public abstract Vector2 GetDirection(EnemyBehavior enemy);
}