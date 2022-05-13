using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenCvSharp;

public class faceDetector : MonoBehaviour
{
    WebCamTexture _webcamTexture;
    CascadeClassifier cascade;
    OpenCvSharp.Rect MyFace;
    void Start()
    {
        WebCamDevice[] devices = WebCamTexture.devices;
        _webcamTexture = new WebCamTexture(devices[0].name);
        _webcamTexture.Play();
        cascade = new CascadeClassifier(Application.dataPath + @"/haarcascade_frontalface_default1.xml");
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Renderer>().material.mainTexture = _webcamTexture;
        Mat frame = OpenCvSharp.Unity.TextureToMat(_webcamTexture);
        findNewFace(frame);
        display(frame);
    }
    void findNewFace(Mat frame)
    {
        var faces = cascade.DetectMultiScale(frame, 1.1, 2, HaarDetectionType.ScaleImage);
        if(faces.Length >= 1)
        {
            Debug.Log(faces[0].Location);
            MyFace = faces[0];
        }
    }
    void display(Mat frame)
    {
        if(MyFace != null)
        {
            frame.Rectangle(MyFace, new Scalar(250, 0, 0), 2);
        }
        Texture newTexture = OpenCvSharp.Unity.MatToTexture(frame);
        GetComponent<Renderer>().material.mainTexture = newTexture;
    }
}
