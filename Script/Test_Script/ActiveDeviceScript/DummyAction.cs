using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyAction : MonoBehaviourPun
{
    public DeviceInfo di;
    private Animator ani;
    private Rigidbody rb;

    [SerializeField] private float moveSpeed = 5.0f;

    private void Start()
    {
        di = GetComponent<DeviceInfo>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (di.getIsOn())
        {
            ani.SetInteger("State", 0);
            MoveDummy(GetMovementInput());
        }
    }

    private Vector3 GetMovementInput()
    {
        Vector3 movement = Vector3.zero;

        if (di.getInputKeyDown(0)) // W
        {
            movement += Vector3.forward;
        }

        if (di.getInputKeyDown(1)) // A
        {
            movement += Vector3.left;
        }

        if (di.getInputKeyDown(2)) // S
        {
            movement += Vector3.back;
        }

        if (di.getInputKeyDown(3)) // D
        {
            movement += Vector3.right;
        }

        return movement.normalized * moveSpeed;
    }

    [PunRPC]
    private void MoveDummy(Vector3 velocity)
    {
        rb.velocity = velocity;

        float moveMagnitude = velocity.magnitude;
        if(moveMagnitude > 0)
            ani.SetInteger("State", 1);
    }

    public void setDeviceInfo(DeviceInfo di)
    {
        this.di = di;
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
        {
            photonView.RPC("MoveDummy", RpcTarget.Others, rb.velocity);
        }
    }
}