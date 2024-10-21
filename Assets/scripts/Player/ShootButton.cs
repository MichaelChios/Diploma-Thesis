using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button shootBtn = GetComponent<Button>();
        shootBtn.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClick()
    {
        Debug.Log("Button pressed");
    }
}
