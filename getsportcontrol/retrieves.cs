using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace getsportcontrol
{
    public class Event
    {
        public int id { get; set; }
        public string name { get; set; }
        public int groundType { get; set; }
        public string homeTeam { get; set; }
        public string awayTeam { get; set; }
        public string homePitcher { get; set; }
        public string awayPitcher { get; set; }
        public bool is80MinsGame { get; set; }
        public bool hasInplayMarkets { get; set; }
        public long startTime { get; set; }
        public string startTimeServer { get; set; }
        public string period { get; set; }
        public int homeRedCards { get; set; }
        public int awayRedCards { get; set; }
        public string score { get; set; }
        public int favoriteType { get; set; }
        public int eventPitcherId { get; set; }
        public int noOfOtherMarkets { get; set; }
        public List<ScoreInfo> scoreInfo { get; set; }
        public List<Market> markets { get; set; }
        public string cardScore { get; set; }
        public string cornerScore { get; set; }
    }

    public class League
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Event> events { get; set; }
    }

    public class Line
    {
        public List<MarketSelection> marketSelections { get; set; }
    }

    public class Market
    {
        public int id { get; set; }
        public string name { get; set; }
        public int marketTypeId { get; set; }
        public int marketGroupId { get; set; }
        public List<Selection> selections { get; set; }
        public List<Line> lines { get; set; }
    }

    public class MarketSelection
    {
        public int id { get; set; }
        public string name { get; set; }
        public string indicator { get; set; }
        public string handicap { get; set; }
        public string odds { get; set; }
        public string decimalOdds { get; set; }
    }

    public class retrieves
    {
        public int sportId { get; set; }
        public int binaryOddsFormatId { get; set; }
        public Schedule schedule { get; set; }
    }

    public class Schedule
    {
        public int id { get; set; }
        public List<League> leagues { get; set; }
    }

    public class ScoreInfo
    {
        public string bettingPeriod { get; set; }
        public bool isActive { get; set; }
        public int homeScore { get; set; }
        public int awayScore { get; set; }
    }

    public class Selection
    {
        public int id { get; set; }
        public string name { get; set; }
        public string indicator { get; set; }
        public string odds { get; set; }
    }
}

