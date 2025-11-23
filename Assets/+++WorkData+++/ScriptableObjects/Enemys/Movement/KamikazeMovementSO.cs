using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Movement/Kamikaze")]
public class KamikazeMovementSO : EnemyMovementSO
{
    public float inaccuracy = 1f;

    public override Vector2 GetDirection(EnemyBehavior enemy)
    {
        Transform player = enemy.GetPlayer();
        if (player == null)
            return Vector2.left;
        
        Vector2 dir = (player.position - enemy.transform.position);
        
        dir.y += Random.Range(-inaccuracy, inaccuracy);

        return dir.normalized;
    }
}