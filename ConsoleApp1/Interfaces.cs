using ConsoleApp1;
using Newtonsoft.Json;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;

class interfaces
{
    public static List<data> readFile()
    {
        if (File.Exists(@"Notes.txt"))
        {
            string file = File.ReadAllText(@"Notes.txt");
            List<data> notes = JsonConvert.DeserializeObject<List<data>>(file);
            return notes;
        }
        else
        {
            Console.Error.WriteLine("File does not exsits!");
            return null;
        }
    }
    public static void showAll()
    {
        List<data> notes = interfaces.readFile();
        var table = new Table();
        var root = new Tree("Notes").Guide(TreeGuide.DoubleLine);
        foreach (data data in notes)
        {
            root.AddNode($"Id:{data.Id}")
                .AddNode(new Table().RoundedBorder()
                .AddColumn($"Title: {data.Title}")
                .AddRow($"Note: {data.Body}"));
        }
        AnsiConsole.Render(root);
    }
    public static void writeNote(List<data> note)
    {
        if (File.Exists(@"Notes.txt"))
        {
            var json = JsonConvert.SerializeObject(note, Formatting.Indented);
            File.WriteAllText(@"Notes.txt", json);
        }
        else
        {
            using (StreamWriter file = File.CreateText(@"Notes.txt"))
            {
                var json = JsonConvert.SerializeObject(note, Formatting.Indented);
                file.WriteLine(json);
            }
        }
    }
    public static void showById(int id)
    {
        List<data> notes = interfaces.readFile();
        data note = notes.Find(x => x.Id == id);
        var table = new Table();
        table.AddColumns("Title", "Body");
        table.AddRow(note.Title, note.Body);
        AnsiConsole.Render(table);
    }
    public static void Update(int id, string title, string body)
    {
        List<data> notes = interfaces.readFile();
        notes.Find(x => x.Id == id).Title = title;
        notes.Find(x => x.Id == id).Body = body;
        var json = JsonConvert.SerializeObject(notes, Formatting.Indented);
        File.WriteAllText(@"Notes.txt", json);
    }
    public static void Delete(int id)
    {
        List<data> notes = interfaces.readFile();
        data note = notes.Find(x => x.Id == id);
        notes.Remove(note);
        if (notes.Exists(x => x.Id > id))
        {
            notes.FindAll(x => x.Id > id).ForEach(x => x.Id = x.Id - 1);
        }
        var json = JsonConvert.SerializeObject(notes, Formatting.Indented);
        File.WriteAllText(@"Notes.txt", json);
        Console.WriteLine("Your selected note removed");
    }
}
class handle
{
    public static string Input(string? input)
    {
        if (input != "")
        {
            return input.ToString();
        }
        else
        {
            return "N/A";
        }
    }
}