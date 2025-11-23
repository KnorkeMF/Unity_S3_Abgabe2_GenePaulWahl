using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Shooting/Straight")]
public class ShootStraightSO : EnemyShootingSO
{
    public override bool ShouldShoot(EnemyBehavior enemy)
    {
        return true;
    }
}