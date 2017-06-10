using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabImage : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RenderTexture tx = this.GetComponent<Camera>().targetTexture;
        RenderTexture.active = tx;
        Texture2D t2 = new Texture2D(tx.width, tx.height);
        t2.ReadPixels(new Rect(0, 0, tx.width, tx.height),0,0);
        RenderTexture.active = null;
        byte[] png = t2.EncodeToPNG();
        System.IO.File.WriteAllBytes("blah.png", png);
        RpcDebugMessages();
	}
}
