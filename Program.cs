using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Тема: Взаємодія з файловою системою
//Модуль 12. Частина 1


namespace _07._05._24_c_
{ 
    internal class Program
    {
        static void Main(string[] args)
        {

            // Завдання 1:
            //Додаток генерує 100 цілих чисел. Збережіть в один файл усі
            //прості числа, в інший файл усі числа Фібоначчі. Статистику роботи
            //додатку виведіть на екран.

            Console.WriteLine($"Task 1\n");

            int prime_counter_1 = 0, fibonacci_counter_1 = 0;

            Random random_1 = new Random();

            try
            {
                for (int i = 0; i < 100; i++)
                {
                    int random_number_1 = random_1.Next();

                    bool isPrime = true;
                    if (random_number_1 <= 1)
                    {
                        isPrime = false;
                    }
                    else
                    {
                        for (int j = 2; j <= Math.Sqrt(random_number_1); j++)
                        {
                            if (random_number_1 % j == 0)
                            {
                                isPrime = false;
                                break;
                            }
                        }
                    }

                    if (isPrime)
                    {
                        prime_counter_1++;
                        using (StreamWriter sw_prime_1 = new StreamWriter("prime.txt", true))
                        {
                            sw_prime_1.Write($"{random_number_1} ");
                        }
                    }

                    int a = 0, b = 1, sum = 0;
                    while (sum < random_number_1)
                    {
                        if (sum == random_number_1)
                        {
                            fibonacci_counter_1++;
                            using (StreamWriter sw_fibonacci_1 = new StreamWriter("fibonacci.txt", true))
                            {
                                sw_fibonacci_1.Write($"{random_number_1} ");
                            }
                        }
                        sum = a + b;
                        a = b;
                        b = sum;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }

            Console.WriteLine($"prime:\t\t\t{prime_counter_1}");
            Console.WriteLine($"fibonacci:\t\t{fibonacci_counter_1}");

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            Console.Clear();

            //Завдання 2:
            //Користувач вводить з клавіатури слово для пошуку у файлі і
            //слово для заміни. Додаток має змінити усі входження шуканого
            //слова на слово для заміни. Статистику роботи додатку виведіть
            //на екран.

            Console.WriteLine($"Task 2\n");

            try
            {
                Console.Write("enter path:\t\t\t");
                string file_path_2 = Console.ReadLine();
                Console.WriteLine();

                StreamReader stream_reader_2 = new StreamReader(file_path_2);
                string file_data_2 = stream_reader_2.ReadToEnd();
                Console.WriteLine($"original file content:\t\t{file_data_2}\n");

                Console.Write("enter original word:\t\t");
                string original_word_2 = Console.ReadLine();
                Console.WriteLine();

                bool is_contain = file_data_2.Contains(original_word_2);
                Console.WriteLine($"the word \"{original_word_2}\" {(is_contain ? "found" : "not found")}");
                if (is_contain)
                {
                    Console.Write("enter replaced word:\t\t");
                    string replaced_word_2 = Console.ReadLine();
                    Console.WriteLine();

                    string replaced_string = file_data_2.Replace(original_word_2, replaced_word_2);
                    Console.WriteLine($"replaced file content:\t\t{replaced_string}\n");
                    stream_reader_2.Close();
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("file not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            Console.Clear();

            //Завдання 3:
            //Створіть додаток «Модератор». Користувач вводить шлях до
            //файлу з текстом і до файлу зі словами для модерації. За
            //підсумками роботи додатка, всі слова для модерації у
            //початковому файлі мають бути замінені на "*".

            //Наприклад, файл зі словами для модерації:
            //car telephone

            //Файл з текстом:
            //test best rest car
            //man telephone

            //Результат:
            //test best rest***
            //man*********

            Console.WriteLine($"Task 3\n");

            try
            {
                Console.Write("enter path to text file:\t\t");
                string textfile_path_3 = Console.ReadLine();
                Console.WriteLine();

                if (File.Exists(textfile_path_3))
                {
                    Console.Write("enter path to moderate file:\t\t");
                    string moderatefile_path_3 = Console.ReadLine();
                    Console.WriteLine();

                    if (File.Exists(moderatefile_path_3))
                    {
                        using (StreamReader stream_reader_moderation = new StreamReader(moderatefile_path_3))
                        {
                            string moderation_data = stream_reader_moderation.ReadToEnd();
                            string[] moderation_words = moderation_data.Split(' ');

                            using (StreamReader stream_reader_3 = new StreamReader(textfile_path_3))
                            using (StreamWriter stream_writer_3 = new StreamWriter("moderator.txt", false))
                            {
                                string line;
                                while ((line = stream_reader_3.ReadLine()) != null)
                                {
                                    string[] words = line.Split(' ');

                                    foreach (string word in words)
                                    {
                                        bool is_moderated = false;
                                        foreach (string moderation_word in moderation_words)
                                        {
                                            if (word == moderation_word)
                                            {
                                                is_moderated = true;
                                                break;
                                            }
                                        }
                                        if (is_moderated)
                                        {
                                            stream_writer_3.Write(new string('*', word.Length) + " ");
                                        }
                                        else
                                        {
                                            stream_writer_3.Write(word + " ");
                                        }
                                    }
                                    stream_writer_3.WriteLine();
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("moderate file not found");
                    }
                }
                else
                {
                    Console.WriteLine("text file not found");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            Console.Clear();

            //Завдання 4:
            //Користувач вводить шлях до файлу. Додаток має перевернути
            //вміст файлу і утворити новий файл.

            Console.WriteLine($"Task 4\n");

            try
            {
                Console.Write("enter path:\t\t\t");
                string file_path_4 = Console.ReadLine();

                using (StreamReader stream_reader_4 = new StreamReader(file_path_4))
                {
                    string file_data_5 = stream_reader_4.ReadToEnd();

                    char[] reversed_file_data_5 = file_data_5.ToCharArray();
                    Array.Reverse(reversed_file_data_5);

                    string reversedFilePath = "reversed_" + file_path_4;

                    try
                    {
                        using (StreamWriter stream_writer_4 = new StreamWriter(reversedFilePath, false))
                        {
                            stream_writer_4.Write(reversed_file_data_5);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"error: {ex.Message}");
                    }
                }
                Console.WriteLine();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("file not found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue . . .");
            Console.ReadKey();
            Console.Clear();

            //Завдання 5:
            //Файл містить 100000 цілих чисел. Додаток має проаналізувати
            //файл і відобразити статистику по ньому:
            //1.Кількість додатних чисел.
            //2.Кількість від'ємних чисел.
            //3.Кількість двозначних чисел.
            //4.Кількість п'ятизначних чисел.
            //Крім того, додаток має створити файли з цими числами(додатні,
            //від'ємні і т. д.).

            Console.WriteLine($"Task 5\n");

            int positive_counter = 0, negative_counter = 0, two_digit_counter = 0, five_digit_counter = 0;

            try
            {
                StreamReader stream_reader_5 = new StreamReader("numbers_test.txt");
                string file_data_5 = stream_reader_5.ReadToEnd().Trim();
                stream_reader_5.Close();

                ArrayList array_list_5 = new ArrayList();
                string[] arr_5 = file_data_5.Split(' ');
                foreach (string s in arr_5)
                {
                    array_list_5.Add(int.Parse(s));
                }

                try
                {
                    foreach (int number_5 in array_list_5)
                    {
                        if (number_5 > 0)
                        {
                            positive_counter++;
                            using (StreamWriter stream_writer_5 = new StreamWriter("positive.txt", true))
                            {
                                stream_writer_5.Write($"{number_5} ");
                            }
                        }
                        else if (number_5 < 0)
                        {
                            negative_counter++;
                            using (StreamWriter stream_writer_5 = new StreamWriter("negative.txt", true))
                            {
                                stream_writer_5.Write($"{number_5} ");
                            }
                        };
                        if ((number_5 >= 10 && number_5 < 100) || (number_5 < -100 && number_5 >= -10))
                        {
                            two_digit_counter++;
                            using (StreamWriter stream_writer_5 = new StreamWriter("two_digit.txt", true))
                            {
                                stream_writer_5.Write($"{number_5} ");
                            }
                        }
                        if ((number_5 >= 10000 && number_5 < 100000) || (number_5 < -100000 && number_5 >= -10000))
                        {
                            five_digit_counter++;
                            using (StreamWriter stream_writer_5 = new StreamWriter("five_digit.txt", true))
                            {
                                stream_writer_5.Write($"{number_5} ");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"error: {ex.Message}");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("file not found/n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }

            Console.WriteLine($"file contains {positive_counter} positive number(s)\n");
            Console.WriteLine($"file contains {negative_counter} negative number(s)\n");
            Console.WriteLine($"file contains {two_digit_counter} two-digit number(s)\n");
            Console.WriteLine($"file contains {five_digit_counter} five-digit number(s)\n");

            Console.WriteLine();

        }
    }
}
