using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FireBulletOnActivate : NetworkBehaviour
{
    public GameObject bullet;    
    public float fireSpeed = 20f;
    private BulletSpawn bulletSpawnScript;

    private void Start()
    {
        StartShot();
    }
    void StartShot()
    {
        GameObject bulletSpawnerObject = GameObject.FindGameObjectWithTag("SpawnPoint");
        if (!IsClient || !IsServer) return;
        if (bulletSpawnerObject != null)
        {
            Debug.Log("��� ����!");
            bulletSpawnScript = bulletSpawnerObject.GetComponent<BulletSpawn>();
            if (bulletSpawnScript != null)
            {
                Debug.Log("������ ������!");
                XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();

                grabbable.activated.AddListener((ActivateEventArgs arg) => FireBullet());
            }
            else
            {
                Debug.LogWarning("������ �� ����� XD");
            }
        }
        else
        {
            Debug.LogWarning("������ �� ������ LOL");
        }

    }

    // Update is called once per frame
    void Update()
    {

    }



    
    public void FireBullet()
    {
        bulletSpawnScript.SpawnBulletClientRPC();
    }
}
