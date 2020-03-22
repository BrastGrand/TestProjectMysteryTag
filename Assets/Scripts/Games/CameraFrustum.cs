using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFrustum : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private float distance;
    [SerializeField] private float size = 5;
    private void OnDrawGizmos()
    {
        if (camera != null)
        {

            Vector3 point1 = camera.ScreenToWorldPoint(new Vector3(0f, 0f, distance));
            Vector3 point2 = camera.ScreenToWorldPoint(new Vector3(0f, camera.pixelHeight, distance));
            Vector3 point3 = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, distance));
            Vector3 point4 = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, 0f, distance));
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(point1, Vector3.one* size);
            Gizmos.color = Color.red;
            Gizmos.DrawCube(point2, Vector3.one* size);
            Gizmos.color = Color.green;
            Gizmos.DrawCube(point3, Vector3.one* size);
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(point4, Vector3.one* size);
            Gizmos.DrawLine(point1, point2);
            Gizmos.DrawLine(point2, point3);
            Gizmos.DrawLine(point3, point4);
            Gizmos.DrawLine(point4, point1);
        }
    }
}