using UnityEngine;
using System.Collections;

public class RPCEngine : MonoBehaviour
{
    public Transform playerPrefab;

    [RPC]
    void RegisterPlayer(NetworkViewID viewId, Vector3 position)
    {
        Transform clone;
        clone = Instantiate(playerPrefab, position, Quaternion.identity) as Transform;
        NetworkView nView;
        nView = clone.GetComponent<NetworkView>();
        nView.viewID = viewId;
    }
}
