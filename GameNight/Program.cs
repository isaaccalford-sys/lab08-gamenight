/* 
* Name: Isaac Alford
* Course: CSCI-1250, Section 201
* Assignment: Lab 08, Game Night
* Date: November 20, 2026
* Discription: Keeps a game night leaderboard in three text files. Shows the
* season so far, records tonight's points, saves the new totals,
* and adds tonight's result to a history that grows every week.

*/

// Constants
//the names of each player from the file.
const string PLAYERS_FILE = "players.txt";

//the currant points of each player in the file.
const string POINTS_FILE = "points.txt";

//the records of past victories of players in the file.
const string HISTORY_FILE = "history.txt";

//the maximum points a player can have.
const int MAX_POINTS = 20;

//finding the files
string projectFolder = Path.GetFullPath(
Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
Directory.SetCurrentDirectory(projectFolder);

// variables
string[] players = File.ReadAllLines(PLAYERS_FILE);
string[] points = File.ReadAllLines(POINTS_FILE);
string[] history = File.ReadAllLines(HISTORY_FILE);

//header
System.Console.WriteLine();
System.Console.WriteLine("=== GAME NIGHT LEADERBOARD ===");


//===part 3=== history log 
int nightsPlayed = history.Length;

if(nightsPlayed == 0)
{
    System.Console.WriteLine("No nights played yet.");
    System.Console.WriteLine("Tonight is Night 1.");
}

if(nightsPlayed >= 1)
{
 System.Console.WriteLine($"Nights played: {nightsPlayed}");

 //it is the simplest loop to use for this since I need every line in history run
 foreach(string line in history)
    {
          System.Console.WriteLine($"  {line}");
    }
        
 System.Console.WriteLine($"Tonight is Night {nightsPlayed + 1}.");
}


//===part 1===


int leader = LeaderIndex(points);
int total = TotalPoints(points);

//output
System.Console.WriteLine();
System.Console.WriteLine("PLAYER".PadRight(11) + "POINTS");


//So that I can use i to dictate position in players and points
for(int i = 0; i < players.Length; i++)
    {
        System.Console.WriteLine(players[i].PadRight(11) + points[i]);
    }

System.Console.WriteLine("Leader: " + players[leader] + " with " + points[leader]);
System.Console.WriteLine("Season total: " + total);
System.Console.WriteLine();

//===part 2===

string[] tonight = new string[points.Length];


//So that I can use i to dictate position in players and tonight
//getting and checking input form user about scores 
for(int i = 0; i < players.Length; i++)
{
    
 System.Console.Write("Points tonight for " + players[i] + ": ");
 tonight[i] = Console.ReadLine();


    //so it can check until the correct input is given by the user
    while(Convert.ToInt32(tonight[i]) > MAX_POINTS || Convert.ToInt32(tonight[i]) < 0)
    {
    System.Console.WriteLine("Points must be from 0 to 20.");
    System.Console.Write("Points tonight for " + players[i] + ": ");

    tonight[i] = Console.ReadLine();
    }

    int season = Convert.ToInt32(points[i]) + Convert.ToInt32(tonight[i]);
    points[i] = season.ToString();

}

File.WriteAllLines(POINTS_FILE, points);

//finding the leader
int winnerTonight = 0;
int highestScore = Convert.ToInt32(tonight[0]);

//So I can use i as the position
for( int i = 0; i < tonight.Length; i++)
{
     if (Convert.ToInt32(tonight[i]) > highestScore)
        {
              highestScore = Convert.ToInt32(tonight[i]);
              winnerTonight = i;
        }
}

//Part if part 3
//history being added to file
string[] entry = { HistoryEntry(nightsPlayed, players[winnerTonight], Convert.ToInt32(tonight[winnerTonight]))};
File.AppendAllLines(HISTORY_FILE, entry);

//output
System.Console.WriteLine("Tonight's winner: " + players[winnerTonight] + " with " + tonight[winnerTonight]);
System.Console.WriteLine();
System.Console.WriteLine("Saved points.txt.");
System.Console.WriteLine("Added to history.txt: "+ entry[entry.Length - 1]);
System.Console.WriteLine();



//Methods 
// Takes in a string array and hands back the position of of the highest number
static int LeaderIndex(string[] points)

{
  int leader = 0;
  int leaderposition = 0;

  //So I can use i as the position
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

// takes in a string array and hands back all of the numbers added together
static int TotalPoints(string[] points)
{
     int totalPoints = 0;
    //So I can use i as the position and make the conersions from string to int easier
     for(int i = 0; i < points.Length; i++)
    {
     totalPoints  += Convert.ToInt32(points[i]);
    }
    return totalPoints;
}

//takes in three data points and hands back a correctly formatted history entry
static string HistoryEntry(int night, string winner, int points)
{
string entry = $"Night {night + 1}: {winner} won with {points} points";
return entry;
}
