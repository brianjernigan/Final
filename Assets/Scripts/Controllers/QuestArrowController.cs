using UnityEngine;

public class QuestArrowBounce : MonoBehaviour
{
    private const float BounceHeight = 0.25f;
    private const float BounceSpeed = 4f;

    private Vector3 _initialPosition;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void Update()
    {
        var newY = _initialPosition.y + Mathf.Sin(Time.time * BounceSpeed) * BounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
