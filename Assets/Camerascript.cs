using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerascript : MonoBehaviour {


    enum CameraStates
    {
        entry,
        waitOnChair
    }

    CameraStates cameraState = CameraStates.entry;

    #region AnimationObjects
    public List<GameObject> animationObjectsList = new List<GameObject>();

    public GameObject chair;
    private Animationcontroller chairController;



    #endregion

    public List<GameObject> cameraList = new List<GameObject>();

    private Animationmanager animationManager;

    public GameObject CAM1_virtualcam;
    private CinemachineVirtualCamera CAM1_cvirtualcamera;
    private CinemachineTrackedDolly CAM1_dolly;

    public GameObject CAM2_virtualcam;
    private CinemachineVirtualCamera CAM2_cvirtualcamera;
    private CinemachineTrackedDolly CAM2_dolly;

    void Start () {

        this.animationObjectsList.Add(this.chair);
        

        this.addComponentToObjects();

        this.cameraList.Add(this.CAM1_virtualcam);
        this.cameraList.Add(this.CAM2_virtualcam);


        this.chairController = this.chair.GetComponent<Animationcontroller>();
        this.animationManager = Animationmanager.Instance;

        this.CAM1_cvirtualcamera = this.CAM1_virtualcam.GetComponent<CinemachineVirtualCamera>();
        this.CAM1_dolly = this.CAM1_cvirtualcamera.GetCinemachineComponent<CinemachineTrackedDolly>();

        this.CAM2_cvirtualcamera = this.CAM2_virtualcam.GetComponent<CinemachineVirtualCamera>();
        this.CAM2_dolly = this.CAM2_cvirtualcamera.GetCinemachineComponent<CinemachineTrackedDolly>();


    }

    private void addComponentToObjects()
    {
        foreach (GameObject animationObject in this.animationObjectsList)
        {
            animationObject.AddComponent<Animationcontroller>();
        }
    }

    private void activateCam(GameObject cam)
    {
        foreach (GameObject camera in this.cameraList)
        {
            if (camera != cam)
            {
                camera.SetActive(false);
            }else
            {
                camera.SetActive(true);
            }
        }
    }


	void Update ()
    {


        switch (this.cameraState)
        {
            case CameraStates.entry:
                activateCam(this.CAM1_virtualcam);
                cameraRideIntro();
                break;

            case CameraStates.waitOnChair:
                activateCam(this.CAM2_virtualcam);
                cameraRideToChairPosition();
                break;
            default:
                break;
        }

       

    }

    private void cameraRideToChairPosition()
    {
        if (this.CAM2_dolly.m_PathPosition < (this.CAM2_dolly.m_Path.MaxPos))
        {
            this.CAM2_dolly.m_PathPosition += (Time.deltaTime / 2);
            this.chairController.Animate("rotate2");
        }
    }

    private void cameraRideIntro()
    {
        if (this.CAM1_dolly.m_PathPosition < (this.CAM1_dolly.m_Path.MaxPos))
        { 
            this.CAM1_dolly.m_PathPosition += Time.deltaTime;
            this.chairController.Animate("rotate");
        }
        else
        {

            if (this.chairController.isInAnimation == false)
            {
                this.cameraState = CameraStates.waitOnChair;
            }
            
        }
    }
}
