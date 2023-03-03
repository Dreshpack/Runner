using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;

    public void PauseButton()
    {
        PausePanel.SetActive(true);
    }
}
