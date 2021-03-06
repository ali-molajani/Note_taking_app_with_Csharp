using ConsoleApp1;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;

int currentId;
var todo = "";
while (true)
{
    List<data> notes = interfaces.readFile();
    if (notes == null)
    {
        currentId = 0;
        notes = new List<data>();
        var rule = new Rule("[indianred]Saved Notes[/]");
        rule.Alignment = Justify.Left;
        AnsiConsole.Render(rule);

        Console.WriteLine($"Saved notes:{currentId}");

        var rule1 = new Rule("[indianred]Current Task[/]");
        rule1.Alignment = Justify.Left;
        AnsiConsole.Render(rule1);
        Console.WriteLine("There is no notes here Let's write some !");
        todo = "w";
    }
    else
    {
        currentId = notes.Count;
        var rule = new Rule("[indianred]Saved Notes[/]");
        rule.Alignment = Justify.Left;
        AnsiConsole.Render(rule);

        Console.WriteLine($"Saved notes:{currentId}");

        var rule1 = new Rule("[indianred]Current Task[/]");
        rule1.Alignment = Justify.Left;
        AnsiConsole.Render(rule1);

        todo = AnsiConsole.Prompt(
            new TextPrompt<string>("What are you going [gray]todo[/]: [olive]Read[/] the previous Notes or [mediumvioletred]write[/] a new one?")
                .InvalidChoiceMessage("[red]That's not a valid option[/]")
                .DefaultValue("r")
                .AddChoice("r")
                .AddChoice("i")
                .AddChoice("u")
                .AddChoice("d")
                .AddChoice("w")
                .AddChoice("q"));
    }

    int nextId = currentId + 1;
    data newNote = new data();

    switch (todo)
    {
        case "r":
            interfaces.showAll();
            break;

        case "w":
            Console.WriteLine("Enter the Title:");
            var input = Console.ReadLine();
            newNote.Title = handle.Input(input);

            Console.WriteLine("Enter the Body:");
            input = null;
            input = Console.ReadLine();
            newNote.Body = handle.Input(input);
            newNote.Id = nextId;
            notes.Add(newNote);
            interfaces.writeNote(notes);
            Console.WriteLine("Note are saved!");
            break;
        case "i":
            Console.WriteLine("Enter the Note id: ");
            int id = int.Parse(Console.ReadLine(), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
            interfaces.showById(id);
            break;
        case "u":
            Console.WriteLine("Enter the Note id: ");
            id = 0;
            id = int.Parse(Console.ReadLine(), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
            Console.WriteLine("Enter new title for note");
            input = null;
            input = Console.ReadLine();
            string newtitle = handle.Input(input);
            Console.WriteLine("Enter new body for the note: ");
            input = null;
            input = Console.ReadLine();
            string newbody = handle.Input(input);
            interfaces.Update(id, newtitle, newbody);
            break;
        case "d":
            Console.WriteLine("Enter the Note id: ");
            id = 0;
            id = int.Parse(Console.ReadLine(), NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite);
            interfaces.Delete(id);
            break;
        case "q":
            Console.WriteLine("have a nice day :)");
            return;
    }
}