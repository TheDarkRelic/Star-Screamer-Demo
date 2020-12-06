using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsHolder : MonoBehaviour
{
    private Powerup[]  optionsCount;
    // Start is called before the first frame update
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(CheckForOptions());
    }

    private IEnumerator CheckForOptions()
    {
        yield return new WaitForSeconds(.5f);
        var optionLeft = GameObject.Find("Ship_Options_Left");
        var optionRight = GameObject.Find("Ship_Options_Right");
        if (optionLeft == null && optionRight == null)
        {
            Player._optionsActive = false;
            Destroy(this.gameObject);
        }
    }
}
