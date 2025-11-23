using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Shooting/Phases")]
public class ShootPhasesSO : EnemyShootingSO
{
    public float shootDuration = 2f;
    public float pauseDuration = 1f;

    public override bool ShouldShoot(EnemyBehavior enemy)
    {
        enemy.shootingTimer += Time.deltaTime;

        if (enemy.isShootingPhase)
        {
            if (enemy.shootingTimer >= shootDuration)
            {
                enemy.isShootingPhase = false;
                enemy.shootingTimer = 0f;
            }

            return true;
        }
        else
        {
            if (enemy.shootingTimer >= pauseDuration)
            {
                enemy.isShootingPhase = true;
                enemy.shootingTimer = 0f;
            }

            return false;
        }
    }
}