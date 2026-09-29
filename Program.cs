public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            ShowHelp();
            return;
        }

        var manager = new TaskManager();

        string command = args[0].ToLower();
        switch (command)
        {
            case "add":
                if (args.Length < 2)
                    Console.WriteLine("Uso: task-cli add \"Descripción de la tarea\"");
                else
                    manager.Add(args[1]);
                break;

            case "update":
                if (args.Length < 3 || !int.TryParse(args[1], out int updateId))
                    Console.WriteLine("Uso: task-cli update <ID> \"Nueva descripción\"");
                else
                    manager.Update(updateId, args[2]);
                break;

            case "delete":
                if (args.Length < 2 || !int.TryParse(args[1], out int deleteId))
                    Console.WriteLine("Uso: task-cli delete <ID>");
                else
                    manager.Delete(deleteId);
                break;

            case "mark-in-progress":
                if (args.Length < 2 || !int.TryParse(args[1], out int inProgressId))
                    Console.WriteLine("Uso: task-cli mark-in-progress <ID>");
                else
                    manager.MarkStatus(inProgressId, "in-progress");
                break;

            case "mark-done":
                if (args.Length < 2 || !int.TryParse(args[1], out int doneId))
                    Console.WriteLine("Uso: task-cli mark-done <ID>");
                else
                    manager.MarkStatus(doneId, "done");
                break;

            case "list":
                if (args.Length == 1)
                {
                    manager.ListTasks();
                }
                else
                {
                    string filter = args[1].ToLower();
                    if (filter == "done" || filter == "todo" || filter == "in-progress")
                    {
                        manager.ListTasks(filter);
                    }
                    else
                    {
                        Console.WriteLine("Filtro no válido. Opciones: todo, in-progress, done");
                    }
                }
                break;

            default:
                Console.WriteLine($"Comando desconocido: {command}");
                ShowHelp();
                break;
        }
    }

    private static void ShowHelp()
    {
        Console.WriteLine("\nUso de Task Tracker CLI:");
        Console.WriteLine("  dotnet run -- add \"Descripción\"");
        Console.WriteLine("  dotnet run -- update <ID> \"Nueva descripción\"");
        Console.WriteLine("  dotnet run -- delete <ID>");
        Console.WriteLine("  dotnet run -- mark-in-progress <ID>");
        Console.WriteLine("  dotnet run -- mark-done <ID>");
        Console.WriteLine("  dotnet run -- list");
        Console.WriteLine("  dotnet run -- list [done|todo|in-progress]\n");
    }

}