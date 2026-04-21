using System;
using System.Collections.Generic;

class ATM
{
    static float balance = 1000;
    static int correctPin = 1234;

    static List<string> transactions = new List<string>();
    static int withdrawCount = 0;

    static void Main(string[] args)
    {
        CheckPin();
    }

    static void CheckPin()
    {
        int attempts = 3;

        while (attempts > 0 )
        {
            Console.Write("Please Enter your Pin: ");
            string input = Console.ReadLine();
            int enteredPin;

            if (int.TryParse(input, out enteredPin))
            {
                if (enteredPin == correctPin)
                {
                    Menu();
                    return;
                }
                else
                {
                    attempts--;
                    Console.WriteLine($"Incorrect PIN, {attempts} attempts left!\n");
                }
            }
            else
            {
                Console.WriteLine("Invalid PIN format!");
            }
        }

        Console.WriteLine("Your Card has been blocked");
    }

    static void Menu()
    {
        while (true)
        {
            Console.WriteLine("\n---------MENU-----------\n");
            Console.WriteLine("1. Check your balance");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Mini Statement");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your Option from 1 to 5: ");
            string input = Console.ReadLine();
            int optionNum;

            if (!int.TryParse(input, out optionNum))
            {
                Console.WriteLine("Enter valid number!");
                continue;
            }

            switch (optionNum)
            {
                case 1:
                    CheckBalance();
                    break;
                case 2:
                    DepositAmt();
                    break;
                case 3:
                    WithdrawAmt();
                    break;
                case 4:
                    MiniStatement();
                    break;
                case 5:
                    Console.WriteLine("Thank You for visiting....");
                    return;
                default:
                    Console.WriteLine("Please enter valid option...");
                    break;
            }
        }
    }

    static void CheckBalance()
    {
        Console.WriteLine($"Your Current Balance is Rs. { balance}");
    }

    static void DepositAmt()
    {
        Console.Write("Enter Deposit Amount: ");
        string input = Console.ReadLine();
        int deposit;

        if (int.TryParse(input, out deposit) && deposit > 0)
        {
            balance += deposit;

            AddTransaction($"Deposited Rs. {deposit} on {DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt")}");

            Console.WriteLine($"Deposited Rs. {deposit}");
            Console.WriteLine($"Updated Balance Rs. {balance}");
        }
        else
        {
            Console.WriteLine("Enter valid amount!");
        }
    }

    static void WithdrawAmt()
    {
        Console.Write("Enter Withdraw Amount: ");
        string input = Console.ReadLine();
        int withdraw;

        if (int.TryParse(input, out withdraw) && withdraw > 0)
        {
            if (withdrawCount >= 3)
            {
                Console.WriteLine("Daily withdraw limit reached! (Max 3 times)");
                return;
            }

            if (withdraw <= balance)
            {
                balance -= withdraw;
                withdrawCount++;

                AddTransaction($"Withdrawn Rs. {withdraw} on {DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt")}");

                Console.WriteLine($"Withdrawn Rs. {withdraw}");
                Console.WriteLine($"Remaining Balance Rs. {balance}");
                Console.WriteLine($"Withdraw used: {withdrawCount}/3");
            }
            else
            {
                Console.WriteLine("Insufficient Balance!");
            }
        }
        else
        {
            Console.WriteLine("Enter valid amount!");
        }
    }

    static void AddTransaction(string message)
    {
        transactions.Add(message);

        if (transactions.Count > 3)
        {
            transactions.RemoveAt(0);
        }
    }

    static void MiniStatement()
    {
        Console.WriteLine("\n-------- Mini Statement --------\n");

        if (transactions.Count == 0)
        {
            Console.WriteLine("No transactions yet");
        }
        else
        {
            foreach (string t in transactions)
            {
                Console.WriteLine(t);
            }
        }

        Console.WriteLine($"\nCurrent Balance: Rs. {balance}");
    }
}