using UnityEngine;
using System.Collections;

public class deactivateSelf : MonoBehaviour
{

    public void deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
