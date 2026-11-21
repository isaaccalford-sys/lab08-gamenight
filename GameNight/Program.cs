/* 
* Name: Isaac Alford
* Course: CSCI-1250, Section 201
* Assignment: Lab 08, Game Night
* Date: November 20, 2026
* Discription:
*/

// Constants
const string PLAYERS_FILE = "players.txt";
const string POINTS_FILE = "points.txt";
const string HISTORY_FILE = "history.txt";
const int MAX_POINTS = 20;

// Look for the data files next to Program.cs, however the program is started.
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

// variables
string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);

int leader = LeaderIndex(points);
int total = TotalPoints(points);

//output
System.Console.WriteLine();
System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");

//display names of players and scores
for(int i = 0; i < players.Length; i++)
    {
        System.Console.WriteLine(players[i].PadRight(11) + points[i]);
    }

System.Console.WriteLine("Leader: " + players[leader] + " with " + points[leader]);
System.Console.WriteLine("Season total: " + total);
System.Console.WriteLine();

//Methods 
static int LeaderIndex(string[] points)
{
  int leader = 0;
  int leaderposition = 0;

    for(int i = 0; i < points.Length; i++)
    {

        if(Convert.ToInt32(points[i]) > leader)
        {
            leader += Convert.ToInt32(points[i]);
            leaderposition  = i;
        }

    }
  return leaderposition;
}

static int TotalPoints(string[] points)
{
     int totalPoints = 0;

     for(int i = 0; i < points.Length; i++)
    {
     totalPoints  += Convert.ToInt32(points[i]);
    }
    return totalPoints;
}