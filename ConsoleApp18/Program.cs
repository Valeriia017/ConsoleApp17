using System;

// Клас події
class EventArguments : EventArgs
{
    public int CurrentDay { get; }

    public EventArguments(int currentDay)
    {
        CurrentDay = currentDay;
    }
}

// Клас Лічильник
class Meter
{
    public event EventHandler<EventArguments> Event;

    // Виводить повідомлення
    public void GenerateEvent(int currentDay)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine($"Сьогодні {currentDay}");

        if (currentDay == 10)
        {
            Console.WriteLine("Час оплати");
            Event?.Invoke(this, new EventArguments(currentDay));
        }
    }
}

// Клас квартирознімач
class Lodger
{
    private string name;
    private int accountBalance;
    private int debt;

    public Lodger(string name, int initialBalance)
    {
        this.name = name;
        this.accountBalance = initialBalance;
        this.debt = 500; // Припустимо, що борг завжди 500
    }

    public int AccountBalance => accountBalance;
    public int Debt => debt;

    public void HandleEvent(object sender, EventArguments e)
    {
        if (e.CurrentDay == 10)
        {
            // Виводить повідомлення
            Console.WriteLine($"{name}, заплатив – {debt}");
            Console.WriteLine($"На рахунку було – {accountBalance + debt}");
            Console.WriteLine($"На рахунку залишилося {accountBalance}");
        }
    }
}

// Клас ЖЕК
class ManagingCompany
{
    private Lodger lodger;

    public ManagingCompany(Lodger lodger)
    {
        this.lodger = lodger;
    }

    public void HandleEvent(object sender, EventArguments e)
    {
        // Виводить повідомлення
        if (e.CurrentDay == 10)
        {
            Console.WriteLine($"Ваш борг – {lodger.Debt}");
        }
    }
}

// Клас тестування
class Program
{
    static void Main()
    {
        Meter meter = new Meter();
        Lodger lodger = new Lodger("Іванов", 400);
        ManagingCompany managingCompany = new ManagingCompany(lodger);

        meter.Event += managingCompany.HandleEvent;
        meter.Event += lodger.HandleEvent;

        for (int i = 1; i <= 16; i++)
        {
            meter.GenerateEvent(i);
        }
    }
}