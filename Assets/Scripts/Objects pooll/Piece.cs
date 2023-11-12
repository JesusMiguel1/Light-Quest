using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace punk_vs_robots
{
    public class Piece : MonoBehaviour
    {
        private IEnumerator destroyDust;
        // Start is called before the first frame update
        void Start()
        {
            this.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0.0f, 1.0f, 0.75f, 1.0f, 0.5f, 1.0f);
        }
        private void Update()
        {
            DestroyDust();
        }
        public void DestroyDust()
        {
            destroyDust = SelfDestroy();
            StartCoroutine(destroyDust);
        }

        IEnumerator SelfDestroy()
        {
            GameObject[] dust = GameObject.FindGameObjectsWithTag("Piece");
            foreach (GameObject target in dust)
            {
                yield return new WaitForSeconds(0.001f);
                if (target != null)
                {
                    target.SetActive(false);
                }
            }
        }
    }

}