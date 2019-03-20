using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DialogueBox : MonoBehaviour
{
    public GameObject speaker;
    public Vector2 boxOffset;
    public Vector2 triggerOffset;
    public Vector2 triggerSize;
    public bool lockToWorldPosition;
    public Vector2 triggerWorldPosition;

    private Vector2 boxOffsetMemory;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
         PositionUpdate();
    }


    private void PositionUpdate()
    {
        BoxCollider2D childCollider = GetComponentInChildren<BoxCollider2D>();

        if (speaker != null) {
            transform.position = speaker.transform.position + (Vector3)boxOffset;
        }

        LockToWorld(childCollider);

        childCollider.offset = -boxOffset + triggerOffset;
        childCollider.size = triggerSize;
    }

    private void LockToWorld(BoxCollider2D childCollider)
    {
        if (lockToWorldPosition) {
            childCollider.transform.position = triggerWorldPosition;
        }
        else {
            childCollider.transform.position = transform.position;
        }
    }
}
