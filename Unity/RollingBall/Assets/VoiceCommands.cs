using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceCommands : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;

    readonly Dictionary<string, Action> keywords = new Dictionary<string, Action>();

    private void OnKeywordRecognized(PhraseRecognizedEventArgs args)
    {
        Action keywordAction;
        if (this.keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    // Use this for initialization
    void Start()
    {
        this.keywords.Add("Reset blocks", () => this.BroadcastMessage("OnResetBlock"));
        this.keywords.Add("Shoot ball", () => this.BroadcastMessage("OnShoot"));
        this.keywordRecognizer = new KeywordRecognizer(this.keywords.Keys.ToArray());
        this.keywordRecognizer.OnPhraseRecognized += this.OnKeywordRecognized;
        this.keywordRecognizer.Start();
    }

    // Update is called once per frame
    void Update()
    {
    }
}