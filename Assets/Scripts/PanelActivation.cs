using UnityEngine;

public class PanelActivation : MonoBehaviour
{
    public GameObject TheObject;
    public void ChangeActivation()
    {
       TheObject.SetActive(!TheObject.activeSelf);
    }
}
