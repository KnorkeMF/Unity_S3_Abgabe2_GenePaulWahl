using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Movement/Kamikaze")]
public class KamikazeMovementSO : EnemyMovementSO
{
    public float inaccuracy = 1f;
    public float turnSpeed = 5f;   // Wie schnell er nachlenkt

    public override Vector2 GetDirection(EnemyBehavior enemy)
    {
        Transform player = enemy.GetPlayer();
        if (player == null)
            return Vector2.left;

        // === 1) Check: Hat der Kamikaze den Player bereits passiert? ===
        if (!enemy.kamikazePassedPlayer)
        {
            // Solange Enemy rechts vom Player ist → verfolgen
            if (enemy.transform.position.x <= player.position.x)
            {
                // Ab jetzt NICHT mehr nachlenken!
                enemy.kamikazePassedPlayer = true;
            }
        }

        // === 2) Phase A: Verfolgen ===
        if (!enemy.kamikazePassedPlayer)
        {
            Vector2 currentDir = enemy.kamikazeDirection == Vector2.zero
                ? Vector2.left
                : enemy.kamikazeDirection;

            // gewünschte Richtung
            Vector2 desiredDir = (player.position - enemy.transform.position).normalized;

            // Abweichung
            desiredDir.y += Random.Range(-inaccuracy, inaccuracy) * 0.1f;
            desiredDir = desiredDir.normalized;

            // sanft drehen
            Vector2 final = Vector2.Lerp(currentDir, desiredDir, Time.deltaTime * turnSpeed).normalized;
            enemy.kamikazeDirection = final;
            return final;
        }

        // === 3) Phase B: vorbei → einfach gerade weiterfliegen ===
        return enemy.kamikazeDirection;
    }
}