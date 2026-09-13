namespace Pluralize;

public static class PluralizeTask
{
public static string PluralizeRubles(int count)
  {
      count = count % 100;

      if (count >= 11 && count <= 14)
          return "рублей";

      if (count % 10 == 1)
          return "рубль";

      if (count % 10 >= 2 && count % 10 <= 4)
          return "рубля";

      return "рублей";
  }

}