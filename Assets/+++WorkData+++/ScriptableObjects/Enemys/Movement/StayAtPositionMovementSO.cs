using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Movement/Stay")]
public class StayAtPositionMovementSO : EnemyMovementSO
{
    public float stopX = 5f;

    public override Vector2 GetDirection(EnemyBehavior enemy)
    {
        if (enemy.transform.position.x > stopX)
            return Vector2.left;
        else
            return Vector2.zero; 
    }
}