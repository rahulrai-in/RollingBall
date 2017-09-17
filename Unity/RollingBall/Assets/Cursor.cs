using UnityEngine;

public class Cursor : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public GameObject FocusedObject { get; private set; }

    // Use this for initialization
    private void Start()
    {
        this.meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;

        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            this.meshRenderer.enabled = true;
            this.transform.position = hitInfo.point;
            this.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
            var newFocusedObject = hitInfo.collider.gameObject;
            if (this.FocusedObject != null && newFocusedObject != this.FocusedObject)
            {
                this.FocusedObject.SendMessage("OnReset");
            }

            this.FocusedObject = newFocusedObject;
            this.FocusedObject.SendMessage("OnSelect");
        }
        else
        {
            this.meshRenderer.enabled = false;
            if (this.FocusedObject != null)
            {
                this.FocusedObject.SendMessage("OnReset");
            }

            this.FocusedObject = null;
        }
    }
}