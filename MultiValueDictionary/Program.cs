using System;
using System.Linq;

namespace MultiValueDictionary
{

    public class Program
    {
        private static IMultiValueDictionary multiValueDictionary;
        private static void Main(string[] args)
        {
            Console.WriteLine("please insert command: ALLMEMBERS,MEMBERS,KEYEXISTS,VALUEEXISTS,ITEMS,KEYS,REMOVEALL,REMOVE,ADD,CLEAR");
            Console.WriteLine("enter exit to quit");

            multiValueDictionary = new MultiValueDictionary(); //inject in real application

            var input = Console.ReadLine();

            while (input.ToLower() != "exit")
            {
                var inputArray = input.Split(" ".ToCharArray());

                switch (inputArray[0].ToUpper())
                {
                    case "CLEAR":
                        multiValueDictionary.Clear();
                        Console.WriteLine(")CLEARED");
                        break;
                    case "ADD":
                        try
                        {
                            var addResponse = multiValueDictionary.Add(inputArray[1], inputArray[2]);
                            if (addResponse)
                                Console.WriteLine(")Added");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    case "REMOVE":
                        try
                        {
                            var removeResponse = multiValueDictionary.Remove(inputArray[1], inputArray[2]);
                            if (removeResponse)
                                Console.WriteLine(")REMOVED");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    case "REMOVEALL":
                        try
                        {
                            var removeAllResponse = multiValueDictionary.RemoveAll(inputArray[1]);
                            if (removeAllResponse)
                                Console.WriteLine(")REMOVED");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                    case "KEYS":
                        Console.WriteLine(">KEYS");

                        var keys = multiValueDictionary.Keys();
                        if (!keys.Any())
                            Console.WriteLine("(EMPTY SET)");
                        else
                        {
                            int keysIndex = 1;

                            foreach (var key in keys)
                            {
                                Console.WriteLine($"{keysIndex}){key}");
                            }
                        }

                        break;
                    case "ITEMS":
                        var items = multiValueDictionary.Items();

                        if (!items.Any())
                            Console.WriteLine("(EMPTY SET)");
                        else
                            foreach (var item in items)
                            {
                                Console.WriteLine($"{item.Item1}: {item.Item2}");
                            }
                        break;
                    case "VALUEEXISTS":
                        var valueExists = multiValueDictionary.ValueExists(inputArray[1], inputArray[2]);
                        Console.WriteLine($") {valueExists}");
                        break;
                    case "KEYEXISTS":
                        var keyExists = multiValueDictionary.KeyExists(inputArray[1]);
                        Console.WriteLine($") {keyExists}");
                        break;
                    case "ALLMEMBERS":
                        var allMembers = multiValueDictionary.AllMembers();
                        int allMembersIndex = 1;
                        foreach (var member in allMembers)
                        {
                            Console.WriteLine($"{allMembersIndex}){member}");
                            allMembersIndex++;
                        }
                        break;
                    case "MEMBERS":
                        try
                        {
                            var members = multiValueDictionary.Members(inputArray[1]);
                            int index = 1;
                            foreach (var member in members)
                            {
                                Console.WriteLine($"{index}){member}");
                                index++;
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;
                }

                Console.Write("> ");

                input = Console.ReadLine();
            }
        }



    }
}
