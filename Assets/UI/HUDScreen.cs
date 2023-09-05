using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScreen : UIScreen
{
    public HUDScreen() {
        _screen.AddToClassList("HUD");
        Generate();
    }

    private void Generate() {
    }
}
