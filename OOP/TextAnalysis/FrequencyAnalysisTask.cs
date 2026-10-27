namespace TextAnalysis;

static class FrequencyAnalysisTask
{
    public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
    {


        var frequency= new Dictionary<string, Dictionary<string, int>>();
        
        foreach(List<string> sentence in text){

            for(int i =0; i +1 < sentence.Count; i++){
                AddNGramm(sentence[i], sentence[i+1], frequency);

                if (i + 2 >= sentence.Count){
                    continue;
                }

                string begin = sentence[i] + " " + sentence[i + 1];
                AddNGramm(begin, sentence[i + 2], frequency);
            }
        }
        
        var result = new Dictionary<string, string>();

        FindBestWord(result, frequency);

        return result;
    }

    public static void FindBestWord(Dictionary<string, string> result, Dictionary<string, Dictionary<string, int>> frequency){
        foreach (var start in frequency)
        {
            string bestWord = "";
            int maxCount = -1;

            foreach (var nextWord in start.Value)
            {
                if (nextWord.Value > maxCount ||
                    (nextWord.Value == maxCount &&
                     string.CompareOrdinal(nextWord.Key, bestWord) < 0))
                {
                    bestWord = nextWord.Key;
                    maxCount = nextWord.Value;
                }
            }

            result[start.Key] = bestWord;
        }
    }

    public static void AddNGramm(string begin, string nextWord, Dictionary<string, Dictionary<string, int>> frequency)
    {
        if (!frequency.ContainsKey(begin))
            frequency[begin] = new Dictionary<string, int>();

        if (!frequency[begin].ContainsKey(nextWord))
            frequency[begin][nextWord] = 0;

        frequency[begin][nextWord]++;
    }
}