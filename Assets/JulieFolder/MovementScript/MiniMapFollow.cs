using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    void Update()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }
}
