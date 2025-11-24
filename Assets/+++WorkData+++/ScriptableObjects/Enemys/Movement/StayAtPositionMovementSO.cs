using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Movement/Stay Random X")]
public class StayAtPositionMovementSO : EnemyMovementSO
{
    [Header("Stop X Range")]
    public Vector2 stopXRange = new Vector2(4.5f, 6.5f);

    public override Vector2 GetDirection(EnemyBehavior enemy)
    {
        // Falls noch nicht gesetzt → zufälligen StopX bestimmen
        if (!enemy.hasStopX)
        {
            enemy.stopX = Random.Range(stopXRange.x, stopXRange.y);
            enemy.hasStopX = true;
        }

        // Bewegung Richtung stopX
        if (enemy.transform.position.x > enemy.stopX)
            return Vector2.left;
        else
            return Vector2.zero;
    }
}