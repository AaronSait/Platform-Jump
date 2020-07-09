public class Player_Data
{
    string name;
    public string get_Name()
    {
        return name;
    }

    int score;
    public int get_Score()
    {
        return score;
    }

    public Player_Data(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
