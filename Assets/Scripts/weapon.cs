using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject FireballPrefab;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Shoot();
        }
    }

    IEnumerator OneSecond()
    {
        yield return new WaitForSeconds(0.2f);
        Instantiate(FireballPrefab, firePoint.position, firePoint.rotation);
    }


    void Shoot()
    {
        StartCoroutine(OneSecond());
    }

}
