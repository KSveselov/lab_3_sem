using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace TableParser;

[TestFixture]
public class FieldParserTaskTests
{
	public static void Test(string input, string[] expectedResult)
	{
		var actualResult = FieldsParserTask.ParseLine(input);
		ClassicAssert.AreEqual(expectedResult.Length, actualResult.Count);
		for (int i = 0; i < expectedResult.Length; ++i)
		{
			ClassicAssert.AreEqual(expectedResult[i], actualResult[i].Value);
		}
	}

	[TestCase("text", new[] { "text" })]
	[TestCase("hello world", new[] { "hello", "world" })]
	[TestCase("ab   cd", new[] { "ab", "cd" })]
	[TestCase("\"bcd ef\"", new[] { "bcd ef" })]
	[TestCase("'x y'", new[] { "x y" })]
	[TestCase("\"a 'b' 'c' d\"", new[] { "a 'b' 'c' d" })]
	[TestCase("'\"1\"'", new[] { "\"1\"" })]
	[TestCase("''", new[] { "" })]
	[TestCase("a\"b\"", new[] { "a", "b" })]
	[TestCase("\"a\"b", new[] { "a", "b" })]
	[TestCase("\"abc", new[] { "abc" })]
	[TestCase("  a  ", new[] { "a" })]
	[TestCase("   ", new string[0])]
	[TestCase("\"\\\\\"", new[] { "\\" })]
	[TestCase("\"\\\\a\"", new[] { "\\a" })]
	[TestCase("\"a\\\"b\"", new[] { "a\"b" })]
	[TestCase("\"a\\\"\"", new[] { "a\"" })]
	[TestCase("'a\\'b'", new[] { "a'b" })]
	[TestCase("\\\\", new[] { "\\\\" })]
	[TestCase("\\\"a", new[] { "\\", "a" })]
	[TestCase("\"a ", new[] { "a " })]

	public static void RunTests(string input, string[] expectedOutput)
	{
		Test(input, expectedOutput);
	}
}

public class FieldsParserTask
{
	// При решении этой задаче постарайтесь избежать создания методов, длиннее 10 строк.
	// Подумайте как можно использовать ReadQuotedField и Token в этой задаче.
	public static List<Token> ParseLine(string line)
	{
		List<Token> tokens = new List<Token>();
		int index = 0;
		while (index < line.Length)
		{
			if (line[index] == ' ') 
			{ 
				index++; 
				continue; 
			}
			Token token;
			if (line[index] == '"' || line[index] == '\'')
				token = QuotedFieldTask.ReadQuotedField(line, index);
			else
				token = ReadField(line, index);
			tokens.Add(token);
			index = token.GetIndexNextToToken();
		}
		return tokens;
	}
        
	private static Token ReadField(string line, int startIndex)
	{
		int index = startIndex;
		while (index < line.Length && line[index] != ' ' && line[index] != '"' && line[index] != '\'')
		{
			index++;
		}
		line = line.Substring(startIndex, index - startIndex);
		return new Token(line, startIndex, index - startIndex);
	}
}
