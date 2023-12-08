using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace object_pool
{
    public class SlowMotion : MonoBehaviour
    {
        public GameObject cube;
        public AI_RobotsScript[] robot;



        public bool destroyed;
        public bool cubeDestroyed;
        public bool powerUp;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {


            if (cube.activeInHierarchy)
            {
                destroyed = false;

            }
            else if (!cube.activeInHierarchy)
            {
                destroyed = true;
                SlowMotionPower();
                Invoke("SetBoolBack", 2f);

            }



        }
        public void SlowMotionPower()
        {

            foreach (AI_RobotsScript robots in robot)
            {
                if (destroyed == true)
                {
                    //robots.moveSpeed = 1;

                }
                else if (destroyed == false)
                {
                    //robots.moveSpeed = 10;
                }

            }

        }

        private void SetBoolBack()
        {
            cube.SetActive(true);
            destroyed = false;
            foreach (AI_RobotsScript robots in robot)
            {
                if (destroyed == false)
                {
                    //robots.moveSpeed = 10;

                }


            }

        }

    }

}