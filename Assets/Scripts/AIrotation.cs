using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIrotation : MonoBehaviour
{
    public Transform player;
    // Instantiate random number generator.
    private bool rotate;  
  
// Generates a random number within a range.      

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        rotate = transform.rotation.z <= 0;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 current = transform.position;
        var direction = player.position - current;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
        if(transform.rotation.eulerAngles.z > 180 && rotate){
            transform.GetChild(0).transform.localRotation *= Quaternion.Euler(0, 180, 0);
            rotate = false;
        }
        if (transform.rotation.eulerAngles.z < 180 && !rotate){
            transform.GetChild(0).transform.localRotation *= Quaternion.Euler(0, 180, 0);
            rotate = true;
        }
    }
}
