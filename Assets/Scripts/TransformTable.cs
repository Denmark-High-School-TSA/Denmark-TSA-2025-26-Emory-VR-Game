using UnityEngine;
using UnityEngine.UI;
public class TransformTable : MonoBehaviour
{
    public Slider mySlider;
    public GameObject Menu;

    public void SetPositionY(float value)
    {
        transform.position = new Vector3(transform.position.x, 0.76f + value,transform.position.z);
        Menu.transform.position = new Vector3(Menu.transform.position.x, 1.5f + value,Menu.transform.position.z);
    }
}
