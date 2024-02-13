using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShoot : MonoBehaviour
{
        public Camera cam;
        
        // Update is called once per frame
        void Update()
        {
            //shoot with left click
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100f))
            {
                if (hit.transform.tag == "Targets")
                {
                    //score goes up
                    GameManager.instance.Score++;
                    //relocates to random new location
                    hit.transform.position = new Vector3(
                        Random.Range(-9, 9),
                        Random.Range(0, 2),
                        Random.Range(9, 4)
                    );
                }
            }
        }
}
