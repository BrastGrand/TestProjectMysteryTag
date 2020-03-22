using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestProjectForMysteryTag
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Player playerPrefabs;
        [SerializeField] private Bull bullPrefab;
        [SerializeField] private GameObject explosion;
        [SerializeField] private float fireRate;
        private float nextFire;

        private Player currentPlayer;

        public void Init(float speedPlayer, BoundaryMove boundsMove, Vector3 spawnPositionPlayer)
        {
            currentPlayer = Instantiate(playerPrefabs, spawnPositionPlayer, Quaternion.Euler(-90,0,0), transform);
            currentPlayer.Init(speedPlayer, boundsMove, ContactEnemy, bullPrefab);
            StartCoroutine(CheckMove());
            StartCoroutine(CheckFire());
        }

        private void ContactEnemy(GameObject enemy)
        {
            SpaceShooterGame.Instance.GetDamagePlayer();
            if (SpaceShooterGame.Instance.CountLive <= 0)
            {
                DestroyPlayer();
            }

        }

        public void DestroyPlayer(bool isDamage = true)
        {
            if (isDamage && explosion != null)
            {
                Instantiate(explosion, currentPlayer.transform.position, currentPlayer.transform.rotation);
            }
            
            SpaceShooterGame.Instance.ObjectsPool.DestroyFromPool(gameObject);
        }

        private IEnumerator CheckMove()
        {
            while (SpaceShooterGame.Instance.CountLive > 0)
            {
                yield return new WaitForFixedUpdate();
                if (currentPlayer == null)
                    break;
                float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");

                Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0f);
                currentPlayer.Move(movement);
            }     
        }

        private IEnumerator CheckFire()
        {
            while (SpaceShooterGame.Instance.CountLive > 0)
            {
                yield return null;
                if (currentPlayer == null)
                    break;
                if (Time.time > nextFire)
                {
                    nextFire = Time.time + fireRate;
                    currentPlayer.Fire();                  
                }
             
            }
        }

    }

    [System.Serializable]
    public struct BoundaryMove
    {
        public float xMin, xMax, yMin, yMax;

        public BoundaryMove(float xMin, float xMax, float yMin, float yMax)
        {
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }
    }
}