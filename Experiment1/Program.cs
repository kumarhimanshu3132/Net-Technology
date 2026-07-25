using System;
using System.Text.RegularExpressions;
class Student
{
    private string name = "";
    private string fatherName = "";
    private string motherName = "";
    private string city = "";
    private string state = "";
    private string pinCode = "";
    private string studentPhone = "";
    private string parentPhone = "";
    private double tenthPercentage;
    private double twelfthPercentage;
    private string program = "";
    private string stream = "";
    private double yearlyCourseFee;
    private double scholarship;
    private string paymentMode = "";
    private double courseFeePayable;
    private double busFeePayable;
    private double hostelFeePayable;
    private string hostelPaymentMode = "";

    private string GetInput(string prompt, string regex, string error)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? "").Trim();
            if (!string.IsNullOrEmpty(input) && Regex.IsMatch(input, regex)) return input;
            Console.WriteLine(error);
        }
    }

    private int GetIntChoice(string prompt, int min, int max)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= min && choice <= max) return choice;
            Console.WriteLine($"Invalid! Enter a number between {min} and {max}.");
        }
    }

    private string GetYesNo(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? "").Trim().ToLower();
            if (input == "yes" || input == "y") return "yes";
            if (input == "no" || input == "n") return "no";
            Console.WriteLine("Invalid! Enter 'yes' or 'no'.");
        }
    }

    public void GetGeneralInfo()
    {
        Console.WriteLine("\n--- STUDENT BASIC INFORMATION ---");
        string nameRegex = "^[a-zA-Z\\s]+$";
        string nameErr = "Invalid input! Only letters and spaces allowed.";
        
        name = GetInput("Enter Student Name: ", nameRegex, nameErr);
        fatherName = GetInput("Enter Father's Name: ", nameRegex, nameErr);
        motherName = GetInput("Enter Mother's Name: ", nameRegex, nameErr);
        city = GetInput("Enter Village/City: ", nameRegex, nameErr);
        state = GetInput("Enter State: ", nameRegex, nameErr);

        pinCode = GetInput("Enter Pin Code (6 digits): ", "^\\d{6}$", "Invalid! Must be exactly 6 digits.");
        studentPhone = GetInput("Student Phone Number (10 digits): ", "^\\d{10}$", "Invalid! Must be exactly 10 digits.");
        parentPhone = GetInput("Parent's Mobile Number (10 digits): ", "^\\d{10}$", "Invalid! Must be exactly 10 digits.");

        tenthPercentage = GetValidPercentage("Enter 10th Percentage (0-100): ");
        twelfthPercentage = GetValidPercentage("Enter 12th Percentage (0-100): ");
    }

    private double GetValidPercentage(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string input = (Console.ReadLine() ?? "").Replace("%", "").Trim();
            if (double.TryParse(input, out double percentage) && percentage >= 0 && percentage <= 100) return percentage;
            Console.WriteLine("Invalid! Enter a value between 0 and 100.");
        }
    }

    public bool IsEligible()
    {
        return tenthPercentage >= 55 && twelfthPercentage >= 55;
    }

    public void SelectBranchAndStream()
    {
        Console.WriteLine("\n--- AVAILABLE BRANCHES ---");
        Console.WriteLine("1. B.TECH\n2. BBA\n3. BCA\n4. M.TECH\n5. MBA\n6. MCA");
        int branchChoice = GetIntChoice("Select Branch (1-6): ", 1, 6);

        switch (branchChoice)
        {
            case 1:
                program = "B.TECH"; yearlyCourseFee = 100000;
                SelectStream(new string[] { "Computer Engineering", "Mechanical Engineering", "Civil Engineering", "Electronics Engineering" });
                break;
            case 2:
                program = "BBA"; yearlyCourseFee = 60000;
                SelectStream(new string[] { "Finance", "Marketing", "Human Resources" });
                break;
            case 3:
                program = "BCA"; yearlyCourseFee = 50000;
                SelectStream(new string[] { "General", "Data Science", "Cyber Security" });
                break;
            case 4:
                program = "M.TECH"; yearlyCourseFee = 120000;
                SelectStream(new string[] { "Computer Science", "VLSI Design", "Structural Engineering" });
                break;
            case 5:
                program = "MBA"; yearlyCourseFee = 150000;
                SelectStream(new string[] { "Finance", "Marketing", "Human Resources" });
                break;
            case 6:
                program = "MCA"; yearlyCourseFee = 80000;
                SelectStream(new string[] { "General", "Artificial Intelligence", "Cloud Computing" });
                break;
        }
        ChoosePaymentMode();
    }

    private void SelectStream(string[] streams)
    {
        Console.WriteLine($"\n--- AVAILABLE STREAMS FOR {program} ---");
        for (int i = 0; i < streams.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {streams[i]}");
        }
        int choice = GetIntChoice($"Select Stream (1-{streams.Length}): ", 1, streams.Length);
        stream = streams[choice - 1];
    }

    private void ChoosePaymentMode()
    {
        Console.WriteLine("\n--- COURSE PAYMENT MODE ---");
        Console.WriteLine($"Yearly Fee: Rs. {yearlyCourseFee} | Semester Fee: Rs. {yearlyCourseFee / 2}");
        while (true)
        {
            Console.Write("How do you want to pay? (sem / year): ");
            string choice = (Console.ReadLine() ?? "").Trim().ToLower();
            if (choice == "sem" || choice == "semester")
            {
                paymentMode = "Semester"; courseFeePayable = yearlyCourseFee / 2; break;
            }
            if (choice == "year" || choice == "yearly")
            {
                paymentMode = "Yearly"; courseFeePayable = yearlyCourseFee; break;
            }
            Console.WriteLine("Invalid! Enter 'sem' or 'year'.");
        }
    }

    public void CalculateScholarship()
    {
        if (twelfthPercentage >= 90) scholarship = 100;
        else if (twelfthPercentage >= 80) scholarship = 50;
        else if (twelfthPercentage >= 70) scholarship = 30;
        else if (twelfthPercentage >= 60) scholarship = 20;
        else scholarship = 0;
    }

    public void AddFacilities()
    {
        string busChoice = GetYesNo("\nDo you want Bus Facility? (yes/no): ");
        if (busChoice == "yes")
        {
            while (true)
            {
                Console.Write("Enter Distance from College (KM): ");
                string distInput = (Console.ReadLine() ?? "").ToLower().Replace("km", "").Trim();
                if (double.TryParse(distInput, out double distance) && distance >= 0)
                {
                    if (distance <= 5) busFeePayable = 500;
                    else if (distance <= 10) busFeePayable = 800;
                    else if (distance <= 20) busFeePayable = 1200;
                    else busFeePayable = 1500;
                    break;
                }
                Console.WriteLine("Invalid! Enter a valid positive number.");
            }
            hostelFeePayable = 0;
        }
        else
        {
            busFeePayable = 0;
            string hostelChoice = GetYesNo("Do you want Hostel Facility? (yes/no): ");
            if (hostelChoice == "yes")
            {
                Console.WriteLine("Hostel Fee: Rs. 55000 (Yearly) | Rs. 30000 (Semester)");
                while (true)
                {
                    Console.Write("How do you want to pay Hostel Fee? (sem / year): ");
                    string choice = (Console.ReadLine() ?? "").Trim().ToLower();
                    if (choice == "sem" || choice == "semester")
                    {
                        hostelFeePayable = 30000;
                        hostelPaymentMode = "Semester";
                        break;
                    }
                    if (choice == "year" || choice == "yearly")
                    {
                        hostelFeePayable = 55000;
                        hostelPaymentMode = "Yearly";
                        break;
                    }
                    Console.WriteLine("Invalid! Enter 'sem' or 'year'.");
                }
            }
            else
            {
                hostelFeePayable = 0;
            }
        }
    }

    public void DisplayAdmissionDetails()
    {
        double scholarshipAmount = courseFeePayable * scholarship / 100;
        double finalFee = (courseFeePayable - scholarshipAmount) + busFeePayable + hostelFeePayable;

        Console.WriteLine("\n======================================");
        Console.WriteLine(" FINAL ADMISSION DETAILS");
        Console.WriteLine("======================================");
        Console.WriteLine($"Student Name       : {name}");
        Console.WriteLine($"10th Percentage    : {tenthPercentage}%");
        Console.WriteLine($"12th Percentage    : {twelfthPercentage}%");
        Console.WriteLine($"Program & Stream   : {program} ({stream})");
        Console.WriteLine($"Course Pay Mode    : {paymentMode}");
        Console.WriteLine($"Base Course Fee    : Rs. {courseFeePayable}");
        Console.WriteLine($"Scholarship        : {scholarship}%");
        Console.WriteLine($"Scholarship Amount : Rs. {scholarshipAmount}");
        if (busFeePayable > 0) Console.WriteLine($"Bus Fee (Monthly)  : Rs. {busFeePayable}");
        if (hostelFeePayable > 0) Console.WriteLine($"Hostel Fee ({hostelPaymentMode}) : Rs. {hostelFeePayable}");
        Console.WriteLine("--------------------------------------");
        Console.WriteLine($"Initial Payable    : Rs. {finalFee}");
        Console.WriteLine("======================================");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("======================================");
        Console.WriteLine(" STUDENT ADMISSION MANAGEMENT");
        Console.WriteLine("======================================");

        Student student = new Student();
        student.GetGeneralInfo();

        if (!student.IsEligible())
        {
            Console.WriteLine("\nAdmission Rejected! Minimum 55% in both 10th and 12th is required.");
            return;
        }

        Console.WriteLine("\nCongratulations! You are eligible for admission.");
        student.SelectBranchAndStream();
        student.CalculateScholarship();
        student.AddFacilities();
        student.DisplayAdmissionDetails();
        Console.WriteLine("\nAdmission Process Completed Successfully!");
    }
}