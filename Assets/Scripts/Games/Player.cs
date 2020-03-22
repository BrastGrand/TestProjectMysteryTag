using System;
using UnityEngine;

namespace TestProjectForMysteryTag
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float speed = 10;
        [SerializeField] private float tilt = 5;
        [SerializeField] private BoundaryMove boundary;
        [SerializeField] private Transform spawnBullTransform;
        [SerializeField] private Rigidbody rigidbody;

        private Bull bullPrefabs;

        private Action<GameObject> contactAction;

        public void Init(float speed, BoundaryMove boundaryMove, Action<GameObject> contactEnemyAction, Bull bullPrefab)
        {
            this.speed = speed;
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody>();
            }

            boundary = boundaryMove;
            contactAction += contactEnemyAction;
            bullPrefabs = bullPrefab;
        }


        public void Move(Vector3 movement)
        {
            rigidbody.MovePosition(transform.position + movement * speed * Time.deltaTime);

            var nextPosition = new Vector3(Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax), Mathf.Clamp(rigidbody.position.y, boundary.yMin, boundary.yMax), rigidbody.position.z);
            rigidbody.MovePosition(nextPosition);

            Debug.Log(movement.x);
            rigidbody.MoveRotation(Quaternion.Euler(-90, movement.x * -tilt, 0));
        }


        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                contactAction?.Invoke(other.gameObject);
            }
        }

        public void Fire()
        {
            if (bullPrefabs != null)
            {
                SpaceShooterGame.Instance.ObjectsPool.InstantiateFromPool(bullPrefabs.gameObject, spawnBullTransform.position, spawnBullTransform.rotation, PoolType.Bull);
            }
            else
            {
                Debug.LogWarning("No bull prefabs");
            }
        }
    }
}
