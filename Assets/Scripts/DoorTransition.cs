using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class DoorTransition : MonoBehaviour
{
    public GameObject character;

    public CinemachineVirtualCamera vcam;

    public PolygonCollider2D newCollider;

    public GameObject nextDoor;

    public Image miniMapTile;

    public bool up;
    public bool down;
    public bool right;
    public bool left;


private void Start() {
    vcam = GameObject.Find("Camera").GetComponentInChildren<CinemachineVirtualCamera>();
    character = GameObject.Find("Player");
    if (nextDoor){
        newCollider = nextDoor.transform.parent.GetComponentInChildren<PolygonCollider2D>();
    }
}
    void OnTriggerEnter2D(Collider2D other) {
        CinemachineConfiner confiner = vcam.GetComponent<CinemachineConfiner>();
        if (other.gameObject.name == "Player"){
            other.transform.position = nextDoor.transform.position;
            nextDoor.transform.parent.GetComponent<RoomManager>().setCurrentRoom();
            confiner.m_BoundingShape2D = newCollider;
            if (right) {other.transform.position += new Vector3(1, -1, 0);}
            if (left) {other.transform.position -= new Vector3(1, 1, 0);}
            if (up) {other.transform.position += new Vector3(0, 2, 0);}
            if (down) {other.transform.position -= new Vector3(0, 3, 0);}
            if (miniMapTile) {miniMapTile.color = new Color(255, 255, 255, 255);}
        }
    }
}