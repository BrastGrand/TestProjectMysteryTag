using UnityEngine;

namespace TestProjectForMysteryTag
{
    public class DestroyZone : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                SpaceShooterGame.Instance.ObjectsPool.DestroyFromPool(other.gameObject);
            }
        }
    }
}
