using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PhotonPlayer : Player
{
    private PhotonView view = null;

    private void Awake()
    {
        view = GetComponent<PhotonView>();
        if (view != null)
        {
            id = view.ViewID;
        }
    }
}
