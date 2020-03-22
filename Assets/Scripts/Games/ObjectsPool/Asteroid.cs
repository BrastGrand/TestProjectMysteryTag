using UnityEngine;

namespace TestProjectForMysteryTag
{
    public class Asteroid : MonoBehaviour, IPoolObject
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private GameObject explosion;
        [SerializeField] private GameObject playerExplosion;
        [SerializeField] private int scoreValue;
        [SerializeField] private float speed;
        [SerializeField] private float tumble;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Bull"))
            {
                if (explosion != null)
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                }
                
                SpaceShooterGame.Instance.DestroyAsteroid(scoreValue, this);
                SpaceShooterGame.Instance.ObjectsPool.DestroyFromPool(gameObject);
            }

            if (other.CompareTag("Player"))
            {
                if (explosion != null)
                {
                    Instantiate(playerExplosion, transform.position, transform.rotation);
                }
                SpaceShooterGame.Instance.ObjectsPool.DestroyFromPool(gameObject);
            }
        }

        public bool IsPooledObject { get; set; }

        public void Activate(GameObject templatePrefab)
        {
            if (rigidbody)
            {
                rigidbody = GetComponent<Rigidbody>();
            }
            rigidbody.velocity = Vector3.zero;
            transform.eulerAngles = Vector3.zero;
            rigidbody.AddRelativeTorque(Random.insideUnitSphere * tumble, ForceMode.VelocityChange);
            rigidbody.AddForce(transform.up * -1 * this.speed, ForceMode.VelocityChange);
        }

        public void Deactivate()
        {
        }
    }
}
