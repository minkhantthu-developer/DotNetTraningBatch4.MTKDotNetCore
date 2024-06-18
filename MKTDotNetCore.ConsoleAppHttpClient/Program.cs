// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
string jsonStr = File.ReadAllText("data.json");
Console.WriteLine(jsonStr);
Console.ReadKey();


public class MainDto
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}

