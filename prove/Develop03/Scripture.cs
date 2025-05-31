using System;
using System.Collections.Generic;

internal class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();
        foreach (string word in text.Split(' '))
        {
            _words.Add(new Word(word));
        }
    }

    public void Display()
    {
        Console.WriteLine(_reference.GetReference());
        foreach (Word word in _words)
        {
            word.Display();
            Console.Write(" ");
        }
    }

    public bool HideSome()
    {
        List<int> visibleIndices = new List<int>();
        for (int i = 0; i < _words.Count; i++)
        {
            if (_words[i].IsVisible())
            {
                visibleIndices.Add(i);
            }
        }

        if (visibleIndices.Count == 0)
            return false;

        int wordsToHide = Math.Min(3, visibleIndices.Count);
        for (int i = 0; i < wordsToHide; i++)
        {
            int randIndex = _random.Next(visibleIndices.Count);
            _words[visibleIndices[randIndex]].Hide();
            visibleIndices.RemoveAt(randIndex);
        }

        return true;
    }

    public string GetReference()
    {
        return _reference.GetReference();
    }

    public Scripture Clone()
    {
        return new Scripture(new Reference(_reference.GetReference()), GetText());
    }

    private string GetText()
    {
        List<string> words = new List<string>();
        foreach (Word word in _words)
        {
            words.Add(word.OriginalText());
        }
        return string.Join(" ", words);
    }
}
