using Newtonsoft.Json;

namespace SecurityQuestionsApp
{
    class SecurityQuestions
    {
        static void Main(string[] args)
        {
            string usersFilePath = "users.json"; //specify the path to the file that stores user data
            Dictionary<string, List<Tuple<string, string>>> users = LoadUserData(usersFilePath);

            Console.WriteLine("Hi, what is your name?");
            string name = Console.ReadLine();

            if (users.ContainsKey(name))
            {
                Console.WriteLine("Do you want to answer a security question? (Y/N)");
                string answerSecurityQuestion = Console.ReadLine().ToUpper();

                if (answerSecurityQuestion == "Y")
                {
                    List<Tuple<string, string>> securityQuestions = users[name];
                    bool answeredCorrectly = false;

                    var rand = new Random();

                    while (securityQuestions.Count > 0 && !answeredCorrectly)
                    {
                        Tuple<string, string> question = securityQuestions[rand.Next(securityQuestions.Count)];

                        Console.WriteLine(question.Item1);
                        string answer = Console.ReadLine();

                        if (answer == question.Item2) //if the user's answer matches the correct answer for the security question
                        {
                            Console.WriteLine("Congratulations, you answered the question correctly!");
                            answeredCorrectly = true;
                        }
                        else //if the user's answer is incorrect
                        {
                            Console.WriteLine("Sorry, your answer is incorrect.");
                            securityQuestions.Remove(question);
                        }
                    }

                    if (!answeredCorrectly)  //if the user has run out of security questions to answer without answering any correctly
                    {
                        Console.WriteLine("You have run out of security questions.");
                    }
                }
                else //if the user does not want to answer a security question
                {
                    PromptAndStoreSecurityQuestions(name, users);
                }
            }
            else //if the user does not have security questions on file
            {
                PromptAndStoreSecurityQuestions(name, users);
            }

            SaveUserData(users, usersFilePath); //save the updated user data to the file
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static Dictionary<string, List<Tuple<string, string>>> LoadUserData(string usersFilePath)
        {
            Dictionary<string, List<Tuple<string, string>>> users = new Dictionary<string, List<Tuple<string, string>>>();

            try
            {
                if (!string.IsNullOrEmpty(usersFilePath) && File.Exists(usersFilePath))
                {
                    string json = File.ReadAllText(usersFilePath);
                    users = JsonConvert.DeserializeObject<Dictionary<string, List<Tuple<string, string>>>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while loading user data: {ex.Message}");
            }

            return users;
        }

        static void SaveUserData(Dictionary<string, List<Tuple<string, string>>> users, string usersFilePath)
        {
            try
            {
                var json = JsonConvert.SerializeObject(users);
                using var streamWriter = new StreamWriter(usersFilePath);
                streamWriter.Write(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving user data: {ex.Message}");
            }
        }

        static void PromptAndStoreSecurityQuestions(string name, Dictionary<string, List<Tuple<string, string>>> users)
        {
            Console.WriteLine("Would you like to store answers to security questions? (Y/N)");
            string storeSecurityQuestion = Console.ReadLine().ToUpper();

            if (storeSecurityQuestion == "Y")
            {
                List<string> securityQuestions = new List<string>()
                {
                    "In what city were you born?",
                    "What is the name of your favorite pet?",
                    "What is your mother's maiden name?",
                    "What high school did you attend?",
                    "What was the mascot of your high school?",
                    "What was the make of your first car?",
                    "What was your favorite toy as a child?",
                    "Where did you meet your spouse?",
                    "What is your favorite meal?",
                    "Who is your favorite actor/actress?"
                };

                List<Tuple<string, string>> answers = new List<Tuple<string, string>>();

                for (int i = 0; i < 3; i++)
                {
                    Console.WriteLine(securityQuestions.ElementAt(i));
                    string answer = Console.ReadLine();

                    Tuple<string, string> questionAnswer = new Tuple<string, string>(securityQuestions.ElementAt(i), answer);
                    answers.Add(questionAnswer);
                }

                users[name] = answers;
            }
        }
    }
}
