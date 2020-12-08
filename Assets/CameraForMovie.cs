using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Assets.Scripts.Services.InputService;
using Snake_box;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CameraForMovie : MonoBehaviour
{

    public static Action stop;
    public static Action<Direction> Turn;
    
    public enum act
    {
        look,
        rotate,
    }

    

    
    public GameObject Player;
    public Vector3 Range;
    public float currenttime;
    public bool starting;
    private float offsetx;
    private float offsety;
    private GameObject rotatearound;
    private bool issetdistance;
    private bool cameraset;
    private int section = 0;
    public List<float>  sectiontiming = new List<float>(4);


    private void Update()
    {
        if (!cameraset)
            if (Player == null)
            {
                Player = GameObject.FindWithTag("Player");
                if (Player != null)
                {
                    SetCamera();
                    cameraset = true;
                    starting = true;
                    //fillgamepref();
                    sectiontiming.Sort();
                }
            }

        if (starting)
        {
            currenttime += Time.deltaTime;
            if (currenttime>sectiontiming[0]&&section == 0)
            {
                Turn.Invoke(Direction.Down);
                section++;
                
            }
            if (currenttime>sectiontiming[1]&&section == 1)
            {
                Turn.Invoke(Direction.Right);
                section++;
                
            }
            if (currenttime>sectiontiming[2]&&section == 2)
            {
                Turn.Invoke(Direction.Down);
                section++;
                
            }
            if (currenttime > sectiontiming[3])
            {
                stop.Invoke();
                starting = false;
            }
        }
        
    }

    private void resetCamera()
    {
        gameObject.transform.SetParent(GameObject.FindGameObjectWithTag("TurretPoint").transform);
        rotatearound = GameObject.FindGameObjectWithTag("TurretPoint");
        transform.rotation = Quaternion.identity;
        transform.position = new Vector3(0, 5, -5);
        transform.LookAt(rotatearound.transform);
    }


    private void SetCamera()
    {
        transform.position = Range;
        gameObject.transform.SetParent(Player.transform);
        transform.LookAt(Player.transform);
        Player.transform.position = new Vector3(-16, 0, 65);
    }

    public void ReserCamera()
    {
        gameObject.transform.SetParent(null);
    }
}
