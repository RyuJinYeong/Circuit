using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorAction : MonoBehaviourPun
{
    /// <summary>
    /// Generator의 동작
    /// </summary>
    ///
    public GameObject[] fan; //Generator가 켜지면 같이 돌아가는 fan(object)
    public GameObject[] positiveObject; //Generator가 켜지면 같이 켜지는(SetActive(true)) object
    public GameObject[] negativeObject; //Generator가 켜지면 꺼지는(SetActive(false)) object

    [SerializeField] private DeviceInfo di;

    private bool isFanOn;
    private bool isGeneratorOn;

    void Start()
    {
        di = this.GetComponent<DeviceInfo>();

        isFanOn = false;
        isGeneratorOn = false;
    }

    // Update is called once per frame
    void Update() 
    {
        if (di.getIsOn())
        {            
            isFanOn = di.getIsOn();
            isGeneratorOn = di.getIsOn();
         
            photonView.RPC("SyncStateRPC", RpcTarget.Others, isFanOn, isGeneratorOn);
        }

        FanRun(isFanOn);
        GeneratorRun(isGeneratorOn);
    }

    void FanRun(bool isOn)
    {
        if(isOn)
        {
            for(int i = 0; i < fan.Length; i++)
            {
                fan[i].transform.Rotate(0, 0, 500 * Time.deltaTime);
            }
        }
        return;
    }

    void GeneratorRun(bool isOn)
    {
        for(int i = 0; i < positiveObject.Length; i++)
        {
            positiveObject[i].SetActive(isOn);
        }
        for(int i = 0; i < negativeObject.Length; i++)
        {
            negativeObject[i].SetActive(!isOn);
        }
        return;
    }

    [PunRPC]
    void SyncStateRPC(bool fanState, bool generatorState)
    {
        isFanOn = fanState;
        isGeneratorOn = generatorState;
    }
}
