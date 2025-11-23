using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Shooting/Burst")]
public class ShootBurstSO : EnemyShootingSO
{
    public int burstCount = 5;
    public float burstPause = 1.5f;

    private int shotsFired = 0;
    private float nextBurstTime = 0f;

    public override bool ShouldShoot(EnemyBehavior enemy)
    {
        if (Time.time < nextBurstTime)
            return false;

        if (shotsFired < burstCount)
        {
            shotsFired++;
            return true;
        }
        else
        {
            shotsFired = 0;
            nextBurstTime = Time.time + burstPause;
            return false;
        }
    }
}