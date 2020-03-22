using System.Collections;
using UnityEngine;

namespace TestProjectForMysteryTag
{
    public class Bull : MonoBehaviour, IPoolObject
    {
        [SerializeField] private Rigidbody rigidbody;
        [SerializeField] private float speed;
        [SerializeField] private float timeLive = 0.5f;

        private Coroutine destroyCoroutine;

        private void Init()
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(transform.forward * speed, ForceMode.VelocityChange);
            destroyCoroutine = StartCoroutine(DestroyBull(timeLive));
        }

        private IEnumerator DestroyBull(float time)
        {
            yield return new WaitForSeconds(time);      
            SpaceShooterGame.Instance.ObjectsPool.DestroyFromPool(gameObject);
        }


        public bool IsPooledObject { get; set; }

        public void Activate(GameObject templatePrefab)
        {
            Init();
        }

        public void Deactivate()
        {
            if (destroyCoroutine != null)
            {
                StopCoroutine(destroyCoroutine);
                destroyCoroutine = null;
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Enemy")
            {
                SpaceShooterGame.Instance.ObjectsPool.DestroyFromPool(gameObject);
            }
        }
    }
}
