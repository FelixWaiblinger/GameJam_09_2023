using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class HUDScreen : UIScreen
{
    public Texture txt;

    protected override void Awake() {
        base.Awake();
        _screen.AddToClassList("hud");
        Generate();
    }

    private void Generate() {

        Label label = Create<Label>();
        label.text = "This is ingame!";
        _screen.Add(label);

        // Exp Bar
        ProgressBar expBar = Create<ProgressBar>("expBar");
        expBar.lowValue = 0;
        expBar.highValue = 1;
        expBar.value = 0.5f;
        _screen.Add(expBar);

        // HP Bar
        ProgressBar hpBar = Create<ProgressBar>("hpBar");
        hpBar.lowValue = 0;
        hpBar.highValue = 1;
        hpBar.value = 0.5f;
        _screen.Add(hpBar);



        Image image = Create<Image>("pic");
        image.image = txt;
        image.scaleMode = ScaleMode.ScaleToFit;
        _screen.Add(image);

        Image image1 = Create<Image>("pic");
        image1.image = txt;
        image1.scaleMode = ScaleMode.StretchToFill;
        _screen.Add(image1);

        Image image2 = Create<Image>("pic");
        image2.image = txt;
        image2.scaleMode = ScaleMode.ScaleAndCrop;
        _screen.Add(image2);


        // Skills Bar

    }
}
