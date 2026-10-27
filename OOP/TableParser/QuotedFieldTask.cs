using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Text;

namespace TableParser;

[TestFixture]
public class QuotedFieldTaskTests
{
	[TestCase("''", 0, "", 2)]
	[TestCase("'a'", 0, "a", 3)]
	[TestCase("\"a 'b' 'c' d\"", 0, "a 'b' 'c' d", 13)]
	[TestCase("'\"1\" \"2\" \"3\"'", 0, "\"1\" \"2\" \"3\"", 13)]
	[TestCase("abc \"d g h", 4, "d g h", 6)]
	[TestCase("\"a \\\"c\\\"\"", 0, "a \"c\"", 9)]
	[TestCase("\"\\\\\" b", 0, "\\", 4)]
	[TestCase("\\\"a b\\\"", 1, "a b\"", 6)]
	public void Test(string line, int startIndex, string expectedValue, int expectedLength)
	{
		var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
		ClassicAssert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
	}

}

class QuotedFieldTask
{
	public static Token ReadQuotedField(string line, int startIndex)
	{
		char quote = line[startIndex];
		StringBuilder value = new StringBuilder();
		int index = startIndex + 1;
		while (index < line.Length)
		{
			if (line[index] == '\\')
			{
				value.Append(line[index + 1]);
				index += 2;
			}
			else if (line[index] == quote)
			{
				index++;
				break;
			}
			else
			{
				value.Append(line[index]);
				index++;
			}
		}
		return new Token(value.ToString(), startIndex, index - startIndex);
	}
}
