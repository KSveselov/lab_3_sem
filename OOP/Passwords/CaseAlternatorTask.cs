namespace Passwords;

public class CaseAlternatorTask
{
	//Тесты будут вызывать этот метод
	public static List<string> AlternateCharCases(string lowercaseWord)
	{
		var result = new List<string>();
		AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
		return result;
	}

	static void AlternateCharCases(char[] word, int startIndex, List<string> result)
	{
		if (startIndex == word.Length)
		{
			result.Add(new string(word));
			return;
		}

		AlternateCharCases(word, startIndex + 1, result);

		var startWord = word[startIndex];
		if (!char.IsLetter(startWord))
			return;

		var upper= char.ToUpper(startWord);
		if (upper == startWord)
			return;

		word[startIndex] = upper;
		AlternateCharCases(word, startIndex + 1, result);
		word[startIndex] = startWord;
	}
}