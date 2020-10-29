using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class FollowCP : MonoBehaviour
{
    public GameObject[] nodes;
    public float nodeDistance;
    public float speed;
    void Update()
    {
        setRot();
        setPos();
    }
    void setRot() {
        for (int i = 1; i < nodes.Length; i++) {
            nodes[i].transform.rotation = Quaternion.LookRotation(nodes[i-1].transform.position-nodes[i].transform.position);
        }
    }
    void setPos() {
        Vector3 point;
        for (int i = 1; i < nodes.Length; i++) {

            point = nodes[i - 1].transform.position + nodes[i - 1].transform.TransformDirection(Vector3.back) * nodeDistance;
            nodes[i].transform.position = Vector3.MoveTowards(nodes[i].transform.position, point, speed * Time.deltaTime);
        }
    }
}
