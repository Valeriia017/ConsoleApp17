using System;

// Клас події
class Event
{
    public delegate void EventHandler();
    public event EventHandler OnEvent;

    public void GenerateEvent()
    {
        Console.WriteLine($"Сьогодні {DateTime.Now.Day}");
        OnEvent?.Invoke();
    }
}

// Клас Meter (Лічильник)
class Meter
{
    public event EventHandler EventOccurred;

    public void GenerateEvent()
    {
        Console.WriteLine($"Сьогодні {DateTime.Now.Day}. Час платити");
        EventOccurred.Invoke();
    }
}

// Клас Lodger (квартирознімач)
class Lodger
{
    public string Name { get; set; }
    public double AccountBalance { get; set; }
    public double Debt { get; set; }

    public Lodger(string name, double accountBalance, double debt)
    {
        Name = name;
        AccountBalance = accountBalance;
        Debt = debt;
    }

    public void HandleEvent()
    {
        Console.WriteLine($"{Name}, заплатив – {Debt}");
        AccountBalance -= Debt;
        Console.WriteLine($"На рахунку було – {AccountBalance + Debt}");
        Console.WriteLine($"На рахунку залишилося {AccountBalance}");
    }
}

// Клас ManagingCompany (ЖЕК)
class ManagingCompany
{
    private Lodger lodger;

    public ManagingCompany(Lodger lodger)
    {
        this.lodger = lodger;
    }

    public void HandleEvent()
    {
        Console.WriteLine($"Ваш борг – {lodger.Debt}");
    }
}

// Клас Program
class Program
{
    static void Main()
    {
        Event myEvent = new Event();
        Meter meter = new Meter();
        Lodger lodger = new Lodger("Іванов", 900, 500);
        ManagingCompany managingCompany = new ManagingCompany(lodger);

        myEvent.OnEvent += meter.GenerateEvent;
        myEvent.OnEvent += lodger.HandleEvent;
        myEvent.OnEvent += managingCompany.HandleEvent;

        for (int i = 1; i <= 12; i++)
        {
            myEvent.GenerateEvent();
        }
    }
}