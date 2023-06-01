using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPositionSync : MonoBehaviourPun, IPunObservable
{
    [SerializeField] Rigidbody[] rigidbody = new Rigidbody[2];


    private void Update()
    {
        if(GetComponentInChildren<HPhysic.PhysicCableCon>().IsLift == true)
        {
            
        }
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rigidbody[0].position);
            stream.SendNext(rigidbody[0].rotation);
            stream.SendNext(rigidbody[0].velocity);

            stream.SendNext(rigidbody[1].position);
            stream.SendNext(rigidbody[1].rotation);
            stream.SendNext(rigidbody[1].velocity);
        }
        else
        {
            rigidbody[0].position = (Vector3)stream.ReceiveNext();
            rigidbody[0].rotation = (Quaternion)stream.ReceiveNext();
            rigidbody[0].velocity = (Vector3)stream.ReceiveNext();

            rigidbody[1].position = (Vector3)stream.ReceiveNext();
            rigidbody[1].rotation = (Quaternion)stream.ReceiveNext();
            rigidbody[1].velocity = (Vector3)stream.ReceiveNext();
        }
    }
}
