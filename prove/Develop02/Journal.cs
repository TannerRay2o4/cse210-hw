using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    private List<Entry> entries = new List<Entry>();
    private const string AutoSaveFilename = "journal_autosave.txt";

    public void AddEntry(string prompt, string response)
    {
        entries.Add(new Entry(prompt, response));
        SaveToFile(AutoSaveFilename); // Automatically save to file after adding entry
    }

    public void DisplayAll()
    {
        foreach (Entry entry in entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in entries)
            {
                writer.WriteLine(entry.ToFileFormat());
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        entries.Clear();
        foreach (string line in File.ReadLines(filename))
        {
            entries.Add(Entry.FromFileFormat(line));
        }
    }

    public void LoadAutoSave()
    {
        LoadFromFile(AutoSaveFilename);
    }
}