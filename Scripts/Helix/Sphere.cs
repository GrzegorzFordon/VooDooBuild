using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helix
{

    public class Sphere : MonoBehaviour
    {
        public float upForce;
        public int comboCounter;
        public int points;

        Rigidbody rb;
        ParticleSystem ps;

        private void Awake()
        {
            TryGetComponent<Rigidbody>(out rb);
            ps = GetComponentInChildren<ParticleSystem>();
            comboCounter = 0;
            points = 0;
        }

        private void OnCollisionEnter(Collision other)
        {

            if (other.transform.tag == "Obstacle")
            {
                //Restart Scene
                SceneLoader.instance.LoadScene(1);
            }
            else if (other.transform.tag == "Stage")
            {
                rb.AddForce(Vector3.up * upForce, ForceMode.Impulse);
                comboCounter = 0;
            }

            ps.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Stage")
            {
                Destroy(other.gameObject);

                comboCounter++;
                points += 100 * comboCounter * comboCounter;
                CanvasManager.instance.TrySetText("Score", points.ToString("0000"));
            }
        }

    }
}
