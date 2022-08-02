using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Helix
{

    public class StageSpawner : MonoBehaviour
    {

        [SerializeField] private GameObject[] stagesArray;


        public int stagesAmount;
        public float spaceBetweenStages;

        private void Start()
        {
            for (int i = 0; i < stagesAmount; i++)
            {
                GameObject rndStage = stagesArray[Random.Range(0, stagesArray.Length)];
                GameObject newStage = Instantiate(rndStage, transform.position - transform.up * spaceBetweenStages * i, Quaternion.identity);
                float rndRot = Random.Range(0.0f, 360.0f);
                newStage.transform.Rotate(0, rndRot, 0);
                newStage.name = "Stage Nr." + i.ToString("000");
                newStage.transform.SetParent(transform);
            }
        }
    }
}
