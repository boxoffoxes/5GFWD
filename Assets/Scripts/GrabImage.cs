using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class GrabImage : NetworkBehaviour {
    public GameObject plane;
    float sampleTime = 20;
    float nextFrameTime = 0;
    private Texture2D t;
    private Texture2D t2;

    // Use this for initialization
    void Start() {
        t = new Texture2D(256, 256);
        t2 = new Texture2D(256, 256);
    }

    // Update is called once per frame
    void Update() {
        RenderTexture tx = this.GetComponent<Camera>().targetTexture;
        RenderTexture.active = tx;
        //Texture2D t2 = new Texture2D(tx.width, tx.height);
        t2.ReadPixels(new Rect(0, 0, tx.width, tx.height), 0, 0);
        byte[] png = t2.EncodeToJPG();
        /* System.IO.File.WriteAllBytes("blah.jpg", png); */
        if (isServer) {
            if (/* Input.GetMouseButton(0) && */ Time.time > nextFrameTime)
            {
                RpcDebugMessages(png);
                nextFrameTime = Time.time + (1/sampleTime);
            }
        } else {

        }
        RenderTexture.active = null;
    }
    [ClientRpc]
    void RpcDebugMessages(byte[] png)
    {
        Debug.Log("I'm so happy");
        //Texture2D t = new Texture2D(256,256);
        t.LoadImage(png);
        plane.transform.GetComponent<MeshRenderer>().material.SetTexture("_MainTex", t);
    }
}
