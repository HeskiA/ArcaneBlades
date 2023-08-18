using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Transform target;
    public Vector3 offset = new Vector3(0, 2, -10);
    private void LateUpdate()
    {
        transform.position = target.position + offset;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
