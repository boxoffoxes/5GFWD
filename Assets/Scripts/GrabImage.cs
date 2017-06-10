using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class GrabImage : NetworkBehaviour {
    public GameObject plane;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        RenderTexture tx = this.GetComponent<Camera>().targetTexture;
        RenderTexture.active = tx;
        Texture2D t2 = new Texture2D(tx.width, tx.height);
        t2.ReadPixels(new Rect(0, 0, tx.width, tx.height), 0, 0);
        byte[] png = t2.EncodeToPNG();
        System.IO.File.WriteAllBytes("blah.png", png);
        if (isServer) {
            RpcDebugMessages(png);
        } else {

        }
        RenderTexture.active = null;
    }
    [ClientRpc]
    void RpcDebugMessages(byte[] png)
    {
        Debug.Log("I'm so happy");
        Texture2D t = new Texture2D(256,256);
        t.LoadImage(png);
        plane.transform.GetComponent<Material>().SetTexture("_MainTex", t);
    }
}
