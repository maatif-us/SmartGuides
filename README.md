# SmartGuides

This is a console application that prompts users to answer security questions and stores the answers for future verification.

# Technologies Used
This application was written in C# using Visual Studio. It utilizes the Newtonsoft.Json package for JSON serialization and deserialization.

# Functionality
When the application is run, it prompts the user for their name. If the user's name is found in the users.json file (which stores user data), the application prompts the user to answer a security question. If the user answers the question correctly, they are congratulated and the application exits. If the user answers the question incorrectly, they are prompted with another security question until they either answer correctly or run out of questions.

If the user's name is not found in the users.json file, the application prompts the user if they would like to store answers to security questions. If the user answers yes, they are prompted with a list of security questions and asked to provide answers. The application then stores the answers in the users.json file for future verification.

# File Structure
- SecurityQuestions.cs: The main application code.
- users.json: The file that stores user data in JSON format.
- The application uses a JSON file to store user data. The file is located at users.json in the root directory of the project. The file is initially empty and will be created automatically when the application is run for the first time.

The JSON file is structured as follows:

```{
  "username": [
    {
      "question": "What is your favorite color?",
      "answer": "Blue"
    },
    {
      "question": "What is your pet's name?",
      "answer": "Fluffy"
    }
  ]
}
```

# How to Run
To run the application, open the project in Visual Studio and press the "Start" button. Alternatively, you can compile the project into an executable and run it from the command line or by double-clicking on the executable file. Note that the users.json file must be in the same directory as the executable for the application to function properly.
