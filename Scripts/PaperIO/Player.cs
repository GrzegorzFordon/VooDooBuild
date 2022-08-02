using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace PaperIO
{
    public class Player : MonoBehaviour
    {
        public MeshCreator meshCreator;
        public List<Vector3> points = new List<Vector3>();
        public float updateDelta;
        public float speed;

        public float rotSpeed;
        Camera maincam;

        public float startingMeshSize;

        public TrailRenderer trailRenderer;

        public bool isPrimed;




        private void Awake()
        {
            points.Clear();
            points.Add(transform.position);
            maincam = Camera.main;


            points.Add(transform.position + Vector3.forward * startingMeshSize);
            points.Add(transform.position + Vector3.left * startingMeshSize);
            points.Add(transform.position - Vector3.forward * startingMeshSize);
            points.Add(transform.position - Vector3.left * startingMeshSize);
            points.Add(transform.position + Vector3.forward * startingMeshSize);


        }

        private void Start()
        {
            TryCreateMesh();

        }

        private void FixedUpdate()
        {

            Rotate();
            transform.position += transform.forward * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, points[points.Count - 1]) > updateDelta)
            {
                points.Add(transform.position);
            }
        }

        void Rotate()
        {
            Vector3 mouseInput = new Vector3(Inputs.mousePos.x, Inputs.mousePos.y, 20);
            Vector3 mouseWorldPos = maincam.ScreenToWorldPoint(mouseInput);
            Quaternion lookDir = Quaternion.LookRotation(mouseWorldPos - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, rotSpeed * Time.deltaTime);
        }


        [ContextMenu("TryCreate")]
        public void TryCreateMesh()
        {
            Vector3[] pointsArray = points.ToArray();
            meshCreator.CreateMesh(pointsArray);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag != "Mesh" || !isPrimed) return;

            points.Add(transform.position + transform.forward * 0.1f);
            print("Trigger with " + other.gameObject.name);
            TryCreateMesh();
            trailRenderer.Clear();
            trailRenderer.emitting = false;
            isPrimed = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag != "Mesh") return;
            isPrimed = true;
            trailRenderer.emitting = true;

        }




    }
}