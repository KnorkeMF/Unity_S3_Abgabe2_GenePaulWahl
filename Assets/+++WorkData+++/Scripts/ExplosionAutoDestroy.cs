using UnityEngine;

public class ExplosionAutoDestroy : MonoBehaviour
{
    private void OnEnable()
    {
        float length = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject, length);
    }
}