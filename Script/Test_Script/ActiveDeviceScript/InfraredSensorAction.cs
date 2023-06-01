using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfraredSensorAction : MonoBehaviour
{
    /// <summary>
    /// 적외선 센서의 동작
    /// 수동소자이므로 전원이 연결되면 일정한 값을 계속 DeviceInfo에 업데이트함
    /// DeviceInfo에 저장된 값을 C.C pinBlock으로 접근할 수 있음
    /// </summary>
    ///

    [SerializeField] DeviceInfo di;

    // Start is called before the first frame update
    void Start()
    {
        di = this.GetComponent<DeviceInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if(di.getIsOn())
        {
            IRSensorRun();
        }
    }

    void IRSensorRun()
    {
        Ray ray = new Ray(this.gameObject.transform.position, Vector3.forward);
        Debug.DrawRay(ray.origin, ray.direction * 5.0f, Color.red); //scen 창에서 Ray를 시각적으로 그려줌
        RaycastHit hit; //Ray와 부딪친 오브젝트

        if(Physics.Raycast(ray, out hit))
        {
            //Debug.Log("hitObject :" + hit.transform.name);
            float distance = hit.distance;

            di.setValue(distance);
        }
        else
        {
            di.setValue(0);
        }
    }
}
