using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Shooting/Spread")]
public class ShootSpreadSO : EnemyShootingSO
{
    public int projectileCount = 3;
    public float angleSpread = 45f;
    public float fireInterval = 1.5f;

    public override bool ShouldShoot(EnemyBehavior enemy)
    {
        return Time.time >= enemy.nextSpreadTime;
    }

    public void FireSpread(EnemyBehavior enemy)
    {
        float startAngle = -angleSpread / 2f;
        float angleStep = angleSpread / (projectileCount - 1);

        for (int i = 0; i < projectileCount; i++)
        {
            float angle = startAngle + angleStep * i;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.left;

            Projectile p = Object.Instantiate(
                enemy.projectilePrefab,
                enemy.firePoint.position,
                Quaternion.identity
            );

            p.data = enemy.data.projectileData;
            p.Init(dir);
        }

        enemy.nextSpreadTime = Time.time + fireInterval;
    }
}