using System.Collections;
using System.Collections.Generic;
using Tabtale.TTPlugins;
using UnityEngine;

public class TTPInitializer : MonoBehaviour
{

    private void Awake()
    {
        TTPCore.Setup();
    }
}
