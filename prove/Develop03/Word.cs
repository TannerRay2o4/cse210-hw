using System;
using System.Text.RegularExpressions;

internal class Word
{
    private string _original;
    private string _coreWord;
    private string _punctuation;
    private bool _isVisible;

    public Word(string text)
    {
        _original = text;
        _isVisible = true;

        Match match = Regex.Match(text, @"^(\w+)(\W*)$");
        if (match.Success)
        {
            _coreWord = match.Groups[1].Value;
            _punctuation = match.Groups[2].Value;
        }
        else
        {
            _coreWord = text;
            _punctuation = "";
        }
    }

    public void Hide()
    {
        _isVisible = false;
    }

    public bool IsVisible()
    {
        return _isVisible;
    }

    public void Display()
    {
        if (_isVisible)
        {
            Console.Write(_coreWord + _punctuation);
        }
        else
        {
            Console.Write(new string('_', _coreWord.Length) + _punctuation);
        }
    }

    public string OriginalText()
    {
        return _coreWord + _punctuation;
    }
}
