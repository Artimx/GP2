
using UnityEngine;

public class TurretClickHandler : MonoBehaviour
{
    [SerializeField] private TurretColorSwitch colorSwitch;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (mainCamera == null)
            return;

        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform clickedObject = hit.transform;

            if (clickedObject == transform ||
                clickedObject.IsChildOf(transform))
            {
                if (colorSwitch != null)
                    colorSwitch.Switch();
            }
        }
    }
}

