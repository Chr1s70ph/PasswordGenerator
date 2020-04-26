using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PasswordGenerator
{
    internal class Program
    {
        [STAThread]
        private static void Main(string[] args)
        {
            Advanced adv = new Advanced();
            adv.Generator();
            Thread.Sleep(5000);
        }
    }

    internal class Advanced
    {
        [NonSerialized] public bool StringEligible;
        [NonSerialized] public bool WrongInputError;

        //Main entry point to Advanced Generator
        public void Generator()
        {
            Greeting();
            string InputLength = Console.ReadLine();
            IsLengthEligible(InputLength);
            if (StringEligible == true)
            {
                AdvancedSettings(Length);
            }
            else
            {
                Generator();
            }
        }

        private void Greeting()
        {
            //Greeting when entering the advanced Generator
            if (WrongInputError == false)
            {
                //only triggered the first time
                Console.WriteLine("Hi, welcome to my Password-Generator.");
                Console.WriteLine("How long do you want your new password?");
            }
        }

        public int Length;

        public void IsLengthEligible(string InputLength)
        {
            if (InputLength.All(char.IsDigit))
            {
                try
                {
                    Length = Convert.ToInt32(InputLength);
                    if (Length < 4 && Length > 0)
                    {
                        StringEligible = false;
                        WrongInputError = true;
                        Console.WriteLine("The length you entered is too short.");
                        Console.WriteLine("The smallest eligible password length is 4");
                    }
                    else
                    {
                        StringEligible = true;
                        Console.WriteLine("The length you entered is: " + Length);
                    }
                }
                catch (FormatException e)
                {
                    StringEligible = false;
                    Console.WriteLine(e);
                }
            }
            else
            {
                WrongInputError = true;
                Console.WriteLine("Make sure you only enter a number!");
            }
        }

        private void AdvancedSettings(int Length)
        {
            ASText();
            string Passwordconfig = Console.ReadLine();

            CheckASInput(Passwordconfig, Length);
        }

        private void ASText()
        {
            Console.WriteLine("Please configure the chars of your desired password.");
            Console.WriteLine("   0        1       2       3");
            Console.WriteLine("  ------------------------------");
            Console.WriteLine("  abcd     aBcd    aB1d    aB1d$");
        }

        private void CheckASInput(string Passwordconfig, int Length)
        {
            if (Passwordconfig.All(char.IsDigit))
            {
                try
                {
                    int ASI = Convert.ToInt32(Passwordconfig);

                    if (!(ASI < 0 || ASI > 3))
                    {
                        SelectGeneration(ASI, Length);
                    }
                    else
                    {
                        Console.WriteLine("Please enter a viable Number!");
                        AdvancedSettings(Length);
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e);
                    AdvancedSettings(Length);
                }
            }
            else
            {
                Console.WriteLine("Make sure you only enter a number!");
                AdvancedSettings(Length);
            }
        }

        private void SelectGeneration(int ASI, int Length)
        {
            if (ASI == 0)
            {
                GeneratorZero(Length);
            }
            else if (ASI == 1)
            {
                GeneratorOne(Length);
            }
            else if (ASI == 2)
            {
                GeneratorTwo(Length);
            }
            else if (ASI == 3)
            {
                GeneratorThree(Length);
            }
        }

        private const string characters = "abcdefghijklmnopqrstuvwxyzäöüßABCDEFGHIJKLMNOPQRSTUVWXYZÖÄÜ0123456789^°!§$%&/{([)]=}?`´+*~#,;.:-_><|";

        private static Random random = new Random();

        public void GeneratorZero(int Length)
        {
            int index = characters.IndexOf('ß');
            string result = characters.Substring(0, index);

            string Password = new string(Enumerable.Repeat(result, Length)
               .Select(s => s[random.Next(s.Length)]).ToArray());
            Console.WriteLine(Password);
            Clipboard.SetText(Password);
        }

        private void GeneratorOne(int Length)
        {
            int index = characters.IndexOf('Ü');
            string result = characters.Substring(0, index);

            string Password = new string(Enumerable.Repeat(result, Length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            if (Password.Any(char.IsLower) && Password.Any(char.IsUpper))
            {
                Console.WriteLine(Password);
                Clipboard.SetText(Password);
            }
            else GeneratorOne(Length);
        }

        private void GeneratorTwo(int Length)
        {
            int index = characters.IndexOf('9');
            string result = characters.Substring(0, index);

            string Password = new string(Enumerable.Repeat(result, Length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            if (Password.Any(char.IsLower) && Password.Any(char.IsUpper) && Password.Any(char.IsDigit))
            {
                Console.WriteLine(Password);
                Clipboard.SetText(Password);
            }
            else GeneratorTwo(Length);
        }

        private void GeneratorThree(int Length)
        {
            string Password = new string(Enumerable.Repeat(characters, Length)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            bool PasswordIsOkay;

            if (Password.IndexOfAny(new char[] { '^', '°', '!', '§', '$', '%', '&', '/', '{', '(', '[', ')', ']', '=', '}', '?', '`', '´', '+', '*', '~', '#', ',', ';', '.', ':', '-', '_', '>', '<', '|', }) >= 0 && Password.Any(char.IsLower) && Password.Any(char.IsUpper) && Password.Any(char.IsDigit))
            {
                PasswordIsOkay = true;
            }
            else PasswordIsOkay = false;

            if (PasswordIsOkay == true)
            {
                Console.WriteLine(Password);
                Clipboard.SetText(Password);
            }
            else GeneratorThree(Length);
        }
    }
}