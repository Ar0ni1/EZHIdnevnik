using System;
using System.Collections.Generic;

class MyDailyPlanner
{
    private List<MyNote> myNotes = new List<MyNote>();
    private int currentNoteIndex = 0;
    private bool selectingAction = false;
    private int selectedActionIndex = 0;
    public void DisplayMyDateSelection()
    {
        MyNote myNote = myNotes[currentNoteIndex];
        Console.WriteLine($"Выберите дату: {myNote.DueDate.ToShortDateString()}");
        Console.WriteLine("Юзайте стрелки влево и вправо для навигации.");
        Console.WriteLine("Нажмите 'Enter' для перехода к выбору действия.");
        Console.WriteLine("Нажмите 'Q' для выхода.");
        Console.WriteLine("Для создания новой заметки нажмите '+'.");
    }

    public MyDailyPlanner()
    {
        myNotes.Add(new MyNote
        {
            DueDate = new DateTime(2023, 10, 21),
            MyActions = new List<MyAction>
            {
                new MyAction { Description = "Исправить 2 по C#", DueDate = new DateTime(2023, 10, 21), NoteDescription = "Переделать практическую работу номер 2" },
                new MyAction { Description = "Погулять с друзьями", DueDate = new DateTime(2023, 10, 21), NoteDescription = "Встретится с друзьями и погулять в парке" }
            }
        });
        myNotes.Add(new MyNote
        {
            DueDate = new DateTime(2023, 10, 26),
            MyActions = new List<MyAction>
            {
                new MyAction { Description = "Съездить в колледж", DueDate = new DateTime(2023, 10, 26), NoteDescription = "Поехать в колледж, исправить оценку по БД, показать презентацию по Компьютерным сетям (Топология сетей)" },
                new MyAction { Description = "С Даньком на вписку", DueDate = new DateTime(2023, 10, 26), NoteDescription = "Встертиться с Даньком и пойти на концерт YETA'a" }
            }
        });
        myNotes.Add(new MyNote
        {
            DueDate = new DateTime(2023, 10, 30),
            MyActions = new List<MyAction>
            {
                new MyAction { Description = "Сдача нормативов по физкультуре", DueDate = new DateTime(2023, 10, 30), NoteDescription = "Подготовиться к сдаче нормативов по физкульуре" },
                new MyAction { Description = "Уехать со вписки", DueDate = new DateTime(2023, 10, 30), NoteDescription = "Поехать с Даньклм домой" }
            }
        });
    }

    public void DisplayMyMenu()
    {
        Console.Clear();
        if (selectingAction)
        {
            DisplayMyActionSelection();
        }
        else
        {
            DisplayMyDateSelection();
        }
    }


    public void DisplayMyActionSelection()
    {
        MyNote myNote = myNotes[currentNoteIndex];
        Console.WriteLine($"Действия на {myNote.DueDate.ToShortDateString()}:");
        for (int i = 0; i < myNote.MyActions.Count; i++)
        {
            if (i == selectedActionIndex)
            {
                Console.Write("-> ");
            }
            else
            {
                Console.Write("   ");
            }
            Console.WriteLine(myNote.MyActions[i].Description);
        }
        Console.WriteLine("Чтобы по кайфу было стрлеки юзай вверх вниз для выбора действия. Нажмите 'Enter' для отображения подробного описания действия. Нажмите 'Q' для выхода.");
    }

    public void DisplayMyActionDescription()
    {
        MyNote currentMyNote = myNotes[currentNoteIndex];
        if (selectedActionIndex >= 0 && selectedActionIndex < currentMyNote.MyActions.Count)
        {
            Console.Clear();
            Console.WriteLine($"Действие на {currentMyNote.DueDate.ToShortDateString()}:");
            MyAction selectedMyAction = currentMyNote.MyActions[selectedActionIndex];
            Console.WriteLine(selectedMyAction.Description);
            Console.WriteLine($"До {selectedMyAction.DueDate.ToShortDateString()}");
            Console.WriteLine("Описание: " + selectedMyAction.NoteDescription);
            Console.WriteLine("Введите 'Enter' для возврата к меню выбора действия.");
            Console.ReadKey();
        }
    }

    public void Run()
    {
        while (true)
        {
            DisplayMyMenu();
            ConsoleKey key = Console.ReadKey().Key;
            if (selectingAction)
            {
                HandleMyActionSelectionInput(key);
            }
            else
            {
                HandleMyDateSelectionInput(key);
            }
        }
    }

    public void HandleMyDateSelectionInput(ConsoleKey key)
    {
        if (key == ConsoleKey.LeftArrow)
        {
            ChangeMyDate(-1);
        }
        else if (key == ConsoleKey.RightArrow)
        {
            ChangeMyDate(1);
        }
        else if (key == ConsoleKey.Enter)
        {
            selectingAction = true;
        }
        else if (key == ConsoleKey.Q)
        {
            Environment.Exit(0); 
        }
        else if (key == ConsoleKey.OemPlus)
        {
            AddMyNote(); 
        }
    }

    public void HandleMyActionSelectionInput(ConsoleKey key)
    {
        if (key == ConsoleKey.UpArrow)
        {
            ChangeMyAction(-1);
        }
        else if (key == ConsoleKey.DownArrow)
        {
            ChangeMyAction(1);
        }
        else if (key == ConsoleKey.Enter)
        {
            DisplayMyActionDescription();
        }
        else if (key == ConsoleKey.Q)
        {
            selectingAction = false;
        }
    }

    public void ChangeMyDate(int direction)
    {
        int newIndex = currentNoteIndex + direction;
        if (newIndex >= 0 && newIndex < myNotes.Count)
        {
            currentNoteIndex = newIndex;
        }
    }

    public void ChangeMyAction(int direction)
    {
        int newMyActionIndex = selectedActionIndex + direction;
        int myActionsCount = myNotes[currentNoteIndex].MyActions.Count;
        if (newMyActionIndex >= 0 && newMyActionIndex < myActionsCount)
        {
            selectedActionIndex = newMyActionIndex;
        }
    }

    public void AddMyNote()
    {
        Console.Clear();
        Console.Write("Введите дату (гггг-мм-дд): ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
        {
            MyNote newMyNote = new MyNote { DueDate = dueDate, MyActions = new List<MyAction>() };

            while (true)
            {
                Console.Write("Название заметки (или 'q' для завершения добавления): ");
                string description = Console.ReadLine();
                if (description.ToLower() == "q")
                {
                    break;
                }

                Console.Write("Описание заметки: ");
                string noteDescription = Console.ReadLine();

                Console.Write("Дата выполнения (гггг-мм-дд): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime actionDueDate))
                {
                    newMyNote.MyActions.Add(new MyAction { Description = description, DueDate = actionDueDate, NoteDescription = noteDescription });
                }
                else
                {
                    Console.WriteLine("Попробуй по другому дату записать. Действие не добавлено.");
                }
            }
            myNotes.Add(newMyNote);
        }
        else
        {
            Console.WriteLine("Попробуй по другому дату записать. Действие не добавлено.");
        }
        Console.WriteLine("Жми Enter для продолжения и все гладко будет.");
        Console.ReadKey();
    }
}
