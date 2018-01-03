using System;
using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using FairyGUI.Utils;
using UnityEngine;

public class EmojiTools {
	static Dictionary<uint, Emoji> _emojies; //表情库
	public static void Init () {
		UIPackage.AddPackage ("UI/Emoji/Emoji");
		_emojies = new Dictionary<uint, Emoji> ();
		for (uint i = 0x1f004; i < 0x1f6c5; i++) {
			string url = UIPackage.GetItemURL ("Emoji", Convert.ToString (i, 16));
			if (url != null)
				_emojies.Add (i, new Emoji (url));
		}
		for (uint i = 0x0023; i < 0x00ae; i++) {
			string url = UIPackage.GetItemURL ("Emoji", Convert.ToString (i, 16));
			if (url != null)
				_emojies.Add (i, new Emoji (url));
		}
		for (uint i = 0x203c; i < 0x3299; i++) {
			string url = UIPackage.GetItemURL ("Emoji", Convert.ToString (i, 16));
			if (url != null)
				_emojies.Add (i, new Emoji (url));
		}
	}

	public static Dictionary<uint, Emoji> GetEmojis () {
		if (_emojies == null) {
			Init ();
		}
		return _emojies;
	}

	public static void Parse (GRichTextField tf, string text) {
		tf.emojies = EmojiTools.GetEmojis (); //给富文本设置emoji包
		tf.width = tf.initWidth;
		tf.text = EmojiParser.inst.Parse (text); //解析string替换Emoji表情
		tf.width = tf.textWidth; //宽度自适应
	}


	private class EmojiParser : UBBParser {
		static EmojiParser _instance;
		public new static EmojiParser inst {
			get {
				if (_instance == null)
					_instance = new EmojiParser ();
				return _instance;
			}
		}

		private static string[] TAGS = new string[] { "88", "am", "bs", "bz", "ch", "cool", "dhq", "dn", "fd", "gz", "han", "hx", "hxiao", "hxiu" };
		public EmojiParser () {
			foreach (string ss in TAGS) {
				this.handlers[":" + ss] = OnTag_Emoji;
			}
		}

		string OnTag_Emoji (string tagName, bool end, string attr) {
			return "<img src='" + UIPackage.GetItemURL ("Emoji", tagName.Substring (1).ToLower ()) + "'/>";
		}
	}
}