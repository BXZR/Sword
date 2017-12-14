using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class onConnectFlash : PunBehaviour 
{
	public override void OnPhotonPlayerConnected (PhotonPlayer newPlayer) 
	{
		print (newPlayer.ToString());
	}
}
