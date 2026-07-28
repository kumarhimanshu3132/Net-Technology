using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
interface IPayrollSystem
{
    void Register();
    void ProcessPayroll();
    void PrintPaySlip();
}
abstract class Employee : IPayrollSystem
{
    public string Id { get; protected set; } = "";
    public string Name { get; protected set; } = "";
    public int LeavesTaken { get; protected set; }
    public double GrossPay { get; protected set; }
    public double NetPay { get; protected set; }
    public double TotalDeductions { get; protected set; }
    public double LeaveDeductionAmount { get; protected set; }
    public bool IsPayrollProcessed { get; protected set; } = false;
    public virtual void Register()
    {
        Id = GetInput("Enter Employee ID (e.g., EMP01): ", "^(?=.*[a-zA-Z])(?=.*[0-9])[a-zA-Z0-9]+$", "Invalid! ID must contain both letters AND numbers.");
        Name = GetInput("Enter Full Name: ", "^[a-zA-Z\\s]+$", "Invalid! Only letters and spaces allowed.");
    }
    public void ProcessPayroll()
    {
        Console.WriteLine($"\n--- Processing Payroll for {Name} (ID: {Id}) ---");
        LeavesTaken = GetValidInt("Enter Leaves Taken in this Month (0-31): ", 0, 31);
        CalculateSalary();
        IsPayrollProcessed = true;
    }
    protected abstract void CalculateSalary();
    public abstract void PrintPaySlip();
    protected string GetInput(string prompt, string regex, string errorMsg)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? "").Trim();
            if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, regex)) return input;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[Warning] {errorMsg}");
            Console.ResetColor();
        }
    }
    protected double GetValidNumber(string prompt, double min)
    {
        while (true)
        {
            Console.Write(prompt);
            if (double.TryParse(Console.ReadLine(), out double value) && value >= min) return value;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[Warning] Please enter a valid number >= {min}.");
            Console.ResetColor();
        }
    }
    protected int GetValidInt(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value) && value >= min && value <= max) return value;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[Warning] Please enter a whole number between {min} and {max}.");
            Console.ResetColor();
        }
    }
}
class FullTimeStaff : Employee
{
    private double basic;
    private double hra;
    private double da;
    private double ta;
    private double pf;
    private double tax;
    public override void Register()
    {
        Console.WriteLine("\n>>> ONBOARD NEW FULL-TIME STAFF <<<");
        base.Register();
        basic = GetValidNumber("Enter Basic Monthly Salary: Rs. ", 5000);
    }
    protected override void CalculateSalary()
    {
        hra = basic * 0.20;
        da = basic * 0.15;
        ta = basic * 0.05;
        GrossPay = basic + hra + da + ta;
        pf = basic * 0.12;
        tax = GrossPay > 30000 ? GrossPay * 0.05 : 0;
        int extraLeaves = LeavesTaken > 3 ? LeavesTaken - 3 : 0;
        double dailyWage = basic / 30.0;
        LeaveDeductionAmount = extraLeaves * dailyWage;
        TotalDeductions = pf + tax + LeaveDeductionAmount;
        NetPay = GrossPay - TotalDeductions;
    }
    public override void PrintPaySlip()
    {
        if (!IsPayrollProcessed) return;
        Console.WriteLine("\n------------------------------------------------");
        Console.WriteLine($"PAY SLIP: {Name} (ID: {Id}) - FULL TIME");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Leaves Taken    : {LeavesTaken}");
        Console.WriteLine($"Basic Pay       : Rs. {basic:F2}");
        Console.WriteLine($"HRA (20%)       : Rs. {hra:F2}");
        Console.WriteLine($"DA (15%)        : Rs. {da:F2}");
        Console.WriteLine($"TA (5%)         : Rs. {ta:F2}");
        Console.WriteLine($"Gross Earnings  : Rs. {GrossPay:F2}");
        Console.WriteLine("--- DEDUCTIONS ---");
        Console.WriteLine($"PF (12%)        : Rs. {pf:F2}");
        Console.WriteLine($"Income Tax (5%) : Rs. {tax:F2}");
        if (LeavesTaken > 3)
        {
            Console.WriteLine($"Leave Deduct    : Rs. {LeaveDeductionAmount:F2} ({LeavesTaken - 3} extra days)");
        }
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"NET PAYABLE     : Rs. {NetPay:F2}");
        Console.WriteLine("------------------------------------------------");
    }
}
class Program
{
        static void Main()
    {
        List<Employee> employeeDB = new List<Employee>();
        while (true)
        {
            Console.WriteLine("\n****************************************************");
            Console.WriteLine("         CORPORATE HR & PAYROLL DASHBOARD");
            Console.WriteLine("****************************************************");
            Console.WriteLine("1. Onboard Full-Time Staff");
            Console.WriteLine("2. Process Monthly Payroll & Pay Slips");
            Console.WriteLine("3. View Company Financial Summary");
            Console.WriteLine("4. Exit System");
            Console.WriteLine("****************************************************");
            Console.Write("Select an operation (1-4): ");
            string choice = (Console.ReadLine() ?? "").Trim();
            if (choice == "1")
            {
                Employee ft = new FullTimeStaff();
                ft.Register();
                employeeDB.Add(ft);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[SUCCESS] {ft.Name} registered successfully.");
                Console.ResetColor();
            }
            else if (choice == "2")
            {
                if (employeeDB.Count == 0)
                {
                    Console.WriteLine("\nNo records found. Please onboard staff first.");
                    continue;
                }
                Console.WriteLine("\n>>> MONTHLY PAYROLL PROCESSING <<<");
                foreach (var emp in employeeDB)
                {
                    emp.ProcessPayroll();
                    emp.PrintPaySlip();
                }
            }
            else if (choice == "3")
            {
                GenerateSummary(employeeDB);
            }
            else if (choice == "4")
            {
                Console.WriteLine("Exiting Dashboard. Have a great day!");
                break;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid command. Please select a valid number.");
                Console.ResetColor();
            }
        }
    }
    static void GenerateSummary(List<Employee> db)
    {
        Console.WriteLine("\n================================================");
        Console.WriteLine("         FINANCIAL SUMMARY REPORT");
        Console.WriteLine("================================================");
        double totalGross = 0;
        double totalNet = 0;
        double totalDeductions = 0;
        int processedCount = 0;
        foreach (var emp in db)
        {
            if (emp.IsPayrollProcessed)
            {
                totalGross += emp.GrossPay;
                totalNet += emp.NetPay;
                totalDeductions += emp.TotalDeductions;
                processedCount++;
            }
        }
        Console.WriteLine($"Total Active Staff    : {db.Count}");
        Console.WriteLine($"Payroll Processed For : {processedCount}");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Total Gross Disbursed : Rs. {totalGross:F2}");
        Console.WriteLine($"Total Tax/Deductions  : Rs. {totalDeductions:F2}");
        Console.WriteLine($"Total Net Paid        : Rs. {totalNet:F2}");
        Console.WriteLine("================================================");
    }
}