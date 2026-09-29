using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


public class TaskItem
{
    public int Id { get; set; }

    public string Description { get; set; }

    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
}

public class TaskManager
{
    private const string FilePath = "tasks.json";

    private List<TaskItem> LoadTasks()
    {
        if (!File.Exists(FilePath))
        {
            return new List<TaskItem>();
        }
        try
        {
            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }
        catch (Exception ex)
        {

            return new List<TaskItem>();
        }
    }

    private void saveTaks(List<TaskItem> tasks)
    {
        var opts = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(tasks, opts);
        File.WriteAllText(FilePath, json);
    }

    public void Add(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Description cannot be empty.");
            return;
        }

        var tasks = LoadTasks();
        var task = tasks.Count > 0 ? tasks.Max(t => t.Id) + 1 : 1;

        var newTask = new TaskItem
        {
            Id = task,
            Description = description,
            Status = "Pending",
            CreatedAt = DateTime.Now,
            UpdateAt = DateTime.Now
        };
        tasks.Add(newTask);
        saveTaks(tasks);
    }

    public void Update(int id, string newDescription)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            Console.WriteLine($"Error: Tarea con ID {id} no encontrada.");
            return;
        }

        task.Description = newDescription;
        task.UpdateAt = DateTime.Now;
        saveTaks(tasks);
        Console.WriteLine($"Task {id} updated successfully.");
    }

    public void Delete(int id)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            Console.WriteLine($"Task with ID {id} not found.");
            return;
        }
        tasks.Remove(task);
        saveTaks(tasks);
        Console.WriteLine($"Task with ID {id} has been deleted.");
    }

    public void MarkStatus(int id, string newStatus)
    {
        var tasks = LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
        {
            Console.WriteLine($"Error: Tarea con ID {id} no encontrada.");
            return;
        }

        task.Status = newStatus;
        task.UpdateAt = DateTime.Now;
        saveTaks(tasks);
        Console.WriteLine($"Task {id} marked as {newStatus}.");
    }

    public void ListTasks(string? filterStatus = null)
    {
        var tasks = LoadTasks();

        if (filterStatus != null)
        {
            tasks = tasks.Where(t => t.Status.Equals(filterStatus, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (tasks.Count == 0)
        {
            Console.WriteLine("No hay tareas registradas.");
            return;
        }

        foreach (var task in tasks)
        {
            Console.WriteLine($"ID: {task.Id} | [{task.Status.ToUpper()}] | {task.Description} | Creada: {task.CreatedAt:yyyy-MM-dd HH:mm} | Actualizada: {task.UpdateAt:yyyy-MM-dd HH:mm}");
        }
    }





}

