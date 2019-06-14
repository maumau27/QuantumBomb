using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public Bounds spawnArea;
    public List<GameObject> itens;
    public float spawTime;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Bounds bounds = spawnArea;

        Vector3 up_left_corner = new Vector3(bounds.min.x, bounds.min.y, 0);
        Vector3 up_rigth_corner = new Vector3(bounds.min.x, bounds.max.y, 0);
        Vector3 down_left_corner = new Vector3(bounds.max.x, bounds.min.y, 0);
        Vector3 down_rigth_corner = new Vector3(bounds.max.x, bounds.max.y, 0);

        Gizmos.DrawLine(up_left_corner, up_rigth_corner);
        Gizmos.DrawLine(up_left_corner, down_left_corner);
        Gizmos.DrawLine(up_rigth_corner, down_rigth_corner);
        Gizmos.DrawLine(down_rigth_corner, down_left_corner);
    }
}
