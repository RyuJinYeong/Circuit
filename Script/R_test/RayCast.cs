using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCast : MonoBehaviourPun
{
    private Camera camera;
    [SerializeField] Canvas cursor;
    [SerializeField] Image cursorImage;
    public GameObject battery;
    float maxDistance = 15f;    
    public int batCount;
    RaycastHit hit;
    Ray ray;    

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false)
        {
            return;
        }
        

        ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

        //Debug.DrawLine(ray.origin, hit.point, Color.red);
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            GameObject Device = hit.transform.gameObject;

            if (Device.CompareTag("BatteryDevice"))
            {
                Debug.Log(Device.tag);
                cursorImage.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Device.GetComponentInParent<BatteryDeviceController>().IsOpenChange();
                }
            }
            else if (Device.CompareTag("Battery"))
            {
                cursorImage.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (Device.transform.parent == null)
                    {
                        int temp = Device.GetPhotonView().ViewID;
                        photonView.RPC("DestroyBat", RpcTarget.All, temp);
                    }
                    else if(Device.transform.parent.CompareTag("BatteryDeviceDoor"))
                    {
                        string[] name = Device.transform.parent.name.Split('_');
                        string names = name[1];
                        Debug.Log("names = " + names);
                        int n = int.Parse(names);
                        Debug.Log("n = " + n);
                        Device.GetComponentInParent<BatteryDeviceDoorController>().BatSetActive(false, n-1);
                        Device.GetComponentInParent<BatteryDeviceController>().BatCountChange(-1);
                    }
                    else
                    {
                        int temp = Device.GetPhotonView().ViewID;
                        photonView.RPC("DestroyBat", RpcTarget.All, temp);
                    }

                    batCount++;
                }
            }
            else if (Device.CompareTag("BatteryDeviceDoor"))
            {
                cursorImage.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    string[] name = Device.name.Split('_');                    
                    string names = name[1];
                    Debug.Log("names = " + names);
                    int n = int.Parse(names);
                    Debug.Log("n = " + n);
                    if (batCount > 0)
                    {
                        if (!Device.GetComponentInParent<BatteryDeviceDoorController>().childBat[n-1].activeSelf)
                        {                            
                            Device.GetComponentInParent<BatteryDeviceDoorController>().BatSetActive(true, n-1);
                        }
                        else
                            return;
                        Device.GetComponentInParent<BatteryDeviceController>().BatCountChange(1); 
                        batCount--;
                    }
                }
            }
            else if (Device.CompareTag("Holdable"))
            {
                cursorImage.gameObject.SetActive(true);
            }
            else if (Device.CompareTag("Monitor"))
            {
                cursorImage.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    //Debug.Log("Device_name : "+Device.name);
                    Cursor.visible = true;
                    Device.GetComponentInParent<PlayCodding>().isCanvasOpen = true;
                    Device.GetComponentInParent<PlayCodding>().player = this.gameObject;
                }
            }
            else if (Device.CompareTag("Controller")) //플레이어 컨트롤러에 접근했을 때
            {
                cursorImage.gameObject.SetActive(true);

                if(Input.GetKeyDown(KeyCode.E))
                {
                    //Debug.Log("access Controller");
                    Device.GetComponentInParent<ControllerAction>().setIsAccess(true);
                    Device.GetComponentInParent<ControllerAction>().player = this.gameObject;
                }

            }
            else if (Device.CompareTag("SwitchDevice"))
            {
                cursorImage.gameObject.SetActive(true);
               
                if (Input.GetKeyDown(KeyCode.E))
                {                                                        
                    Device.GetComponentInParent<SwitchDeviceController>().isActive();
                }
            }
            else
            {
                cursorImage.gameObject.SetActive(false);
            }
        }
    }

    [PunRPC]
    public void DestroyBat(int temp)
    {
        GameObject bat = PhotonView.Find(temp).gameObject;
        PhotonNetwork.Destroy(bat);
    }
}