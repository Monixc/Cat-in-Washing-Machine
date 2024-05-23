using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Diagnostics;

public class BackToMain : MonoBehaviour
{
    public void GoToMain()
    {
        UnityEngine.Debug.Log("Go to Main Scene");
        PhotonNetwork.LoadLevel("main");
    }
}
