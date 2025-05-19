using System;

public class Entry
{
    private string _date;
    private string _prompt;
    private string _response;

    public Entry(string prompt, string response)
    {
        _date = DateTime.Now.ToString("yyyy-MM-dd");
        _prompt = prompt;
        _response = response;
    }

    public string GetEntryText()
    {
        return $"Date: {_date} | Prompt: {_prompt} | Response: {_response}";
    }

    public string ToFileFormat()
    {
        return $"{_date}~|~{_prompt}~|~{_response}";
    }

    public static Entry FromFileFormat(string line)
    {
        string[] parts = line.Split("~|~");
        return new Entry(parts[1], parts[2]) { _date = parts[0] };
    }

    public void Display()
    {
        Console.WriteLine(GetEntryText());
    }
}
