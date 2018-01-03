using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;
public class EmojiDemo : MonoBehaviour {

    // Emoji实现 需要依赖FairyGui的富文本控件，以及对应Unicode码命名的图片集
    public GComponent view;
    GTextInput textInput;

    GRichTextField richTextField;
    
    void Start () {
       
        UIPackage.AddPackage ("UI/EmojiDemo/EmojiDemo");
        GRoot.inst.SetContentScaleFactor (1080, 1920, UIContentScaler.ScreenMatchMode.MatchWidthOrHeight);
        view = UIPackage.CreateObject ("EmojiDemo", "EmojiDemo").asCom;
        view.SetSize (GRoot.inst.width, GRoot.inst.height);
        view.AddRelation (GRoot.inst, RelationType.Size);
        GRoot.inst.AddChild (view);

        richTextField = view.GetChild ("richtext").asRichTextField;

        string  text =	"An awesome string with a few 😉emojis!";
        EmojiTools.Parse(richTextField,text); 

        textInput = view.GetChild ("input").asTextInput;
        textInput.onFocusOut.Set (() => {
            SendInput ();
        });
    }

    void SendInput () {
        string text = textInput.text;
        
        EmojiTools.Parse(richTextField,text);  //富文本解析string 替换emoji
       
        textInput.text = "";                          
    }
}