using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    public bool isEnabled = true;

    private Camera _camera;
    private Vector3? _prevPosition;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (isEnabled)
        {
            var currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                _prevPosition = currentPosition;
            }
            else if (Input.GetMouseButton(0) && _prevPosition.HasValue)
            {
                var delta = currentPosition - _prevPosition.Value;
                transform.position = new Vector3(transform.position.x - delta.x, transform.position.y - delta.y, transform.position.z);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                _prevPosition = null;
            }
        }
    }
}
