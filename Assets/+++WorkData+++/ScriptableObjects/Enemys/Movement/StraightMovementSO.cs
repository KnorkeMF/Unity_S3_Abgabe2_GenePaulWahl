using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Movement/Straight")]
public class StraightMovementSO : EnemyMovementSO
{
    public override Vector2 GetDirection(EnemyBehavior enemy)
    {
        return Vector2.left; 
    }
}