using ConsoleApp1;
using Spectre.Console;
using System.Globalization;

List<data> notes = interfaces.readFile();
int currentId;
if (notes == null)
{
    currentId = 0;
    notes = new List<data>();
}
else
{
    currentId = notes.Count;
}

int nextId = currentId + 1;
data newNote = new data();

var rule = new Rule("[indianred]Saved Notes[/]");
rule.Alignment = Justify.Left;
AnsiConsole.Render(rule);

Console.WriteLine($"Saved notes:{currentId}");

var rule1 = new Rule("[indianred]Current Task[/]");
rule1.Alignment = Justify.Left;
AnsiConsole.Render(rule1);

var todo = AnsiConsole.Prompt(
    new TextPrompt<string>("What are you going [gray]todo[/]: [olive]Read[/] the previous Notes or [mediumvioletred]write[/] a new one?")
        .InvalidChoiceMessage("[red]That's not a valid option[/]")
        .DefaultValue("r")
        .AddChoice("r")
        .AddChoice("i")
        .AddChoice("u")
        .AddChoice("d")
        .AddChoice("w"));

switch (todo)
{
    case "r":
        interfaces.showAll();
        return;

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
}