using System;
using System.Collections.Generic;

class Program
{
    static List<Note> notes;

    static void Main(string[] args)
    {
        notes = new List<Note>();


        Note note1 = new Note("1.Вторник", "Прогулять пары вместе с Сеней в мираторге ", new DateTime(2023, 10, 10));
        Note note2 = new Note("2.Среда", "После учебы написать новый бит ", new DateTime(2023, 10, 11));
        Note note3 = new Note("3.Четверг", "Сделать ежедневник", new DateTime(2023, 10, 12));


        notes.Add(note1);
        notes.Add(note2);
        notes.Add(note3);


        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Приветствую тебя дорогой друг!!!");
            Console.WriteLine("Меню ежедневника:");
            Console.WriteLine("1. Просмотреть список старых заметок");
            Console.WriteLine("2. Просмотреть список новых заметок");
            Console.WriteLine("3. Добавить новую заметку");
            Console.WriteLine("4. Выход");
            Console.WriteLine("Выберите пункт меню (1-4):");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewOldNotes();
                    break;
                case "2":
                    ViewNewNotes();
                    break;
                case "3":
                    AddNewNote();
                    break;
                case "4":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Ай ай ай. Пожалуйста, выберите пункт меню от 1 до 4.");
                    break;
            }
        }
    }

    static void ViewOldNotes()
    {
        Console.WriteLine("Список старых заметок:");
        foreach (Note note in notes)
        {
            if (note.Date < DateTime.Now)
            {
                Console.WriteLine($"Название: {note.Title}");
                Console.WriteLine($"Описание: {note.Description}");
                Console.WriteLine($"Дата: {note.Date.ToString("dd.MM.yyyy")}");
                Console.WriteLine(".....");
            }
        }
        Console.WriteLine("Нажми ENTER, чтобы продолжить!!!");
        Console.ReadLine();
    }

    static void ViewNewNotes()
    {
        Console.WriteLine("Список новых заметок:");
        foreach (Note note in notes)
        {
            if (note.Date >= DateTime.Now)
            {
                Console.WriteLine($"Название: {note.Title}");
                Console.WriteLine($"Описание: {note.Description}");
                Console.WriteLine($"Дата: {note.Date.ToString("dd.MM.yyyy")}");
                Console.WriteLine(".....");
            }
        }
        Console.WriteLine("Нажми ENTER, чтобы продолжить...");
        Console.ReadLine();
    }

    static void AddNewNote()
    {
        Console.WriteLine("Введи название заметки:");
        string title = Console.ReadLine();
        Console.WriteLine("Введи описание заметки:");
        string description = Console.ReadLine();
        Console.WriteLine("Введи дату заметки в формате dd.MM.yyyy:");
        string dateInput = Console.ReadLine();

        if (DateTime.TryParseExact(dateInput, "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            Note newNote = new Note(title, description, date);
            notes.Add(newNote);
            Console.WriteLine("Заметка  добавлена!");
        }
        else
        {
            Console.WriteLine("Другой формат даты. Заметка не была добавлена.");
        }

        Console.WriteLine("Нажмите ENTER, чтобы продолжить...");
        Console.ReadLine();
    }
}

class Note
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public Note(string title, string description, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;
    }
}

