using Microsoft.Win32.SafeHandles;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;
    public HealthData healthData;

    public int damage = 20;
    public float fireRate = 1.5f;
    public float movementSpeed = 5f;   

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        damage = 20;
        fireRate = 1.5f;
        movementSpeed = 5.0f;
    }
}
