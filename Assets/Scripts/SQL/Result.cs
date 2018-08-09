using System.Linq;

[System.Serializable]
public class Result
{
    public int id { get; private set; }
    public int levelID { get; private set; }
    public int userID { get; private set; }
    public int codeLines { get; private set; }
    public int codeTime { get; private set; }

    private Result(int levelID, User user, int codeLines, int codeTime)
    {
        this.id = Repository.ins.results.Count;
        this.levelID = levelID;
        this.userID = user.id;
        this.codeLines = codeLines;
        this.codeTime = codeTime;

        Repository.ins.results.Add(this);
    }

    private static void EditResult(Result result, int codeLines, int codeTime)
    {
        if(codeLines < result.codeLines || codeTime < result.codeTime)
        {
            result.codeLines = codeLines;
            result.codeTime = codeTime;
        }

      //  Debug.LogWarning("Wynik nie został zmieniony, ponieważ wartości poprzedniego rozwiązania są takie same.");
    }

    public static void AddResult(int levelID, User user, int codeLines, int codeTime)
    {
        Result findResult = Repository.ins.results.FirstOrDefault(x=>x.levelID == levelID && x.userID == user.id);

        if (findResult != null) EditResult(findResult, codeLines, codeTime);
        else new Result(levelID, user, codeLines, codeTime);

    }
}
