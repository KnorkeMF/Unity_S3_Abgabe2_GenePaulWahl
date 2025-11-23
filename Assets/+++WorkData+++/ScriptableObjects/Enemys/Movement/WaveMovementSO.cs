using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Movement/Wave")]
public class WaveMovementSO : EnemyMovementSO
{
    public float amplitude = 1f;
    public float frequency = 2f;

    public override Vector2 GetDirection(EnemyBehavior enemy)
    {
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        return new Vector2(-1, y).normalized;
    }
}