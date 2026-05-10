using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lab2_cs
{
  public class Track
  {
    public string Title { get; }
    public string Artist { get; }
    public string Duration { get; }

    public Track(string title, string artist, string duration)
    {
      Title = title;
      Artist = artist;
      if (!Regex.IsMatch(duration, @"^[0-5]?[0-9]:[0-5][0-9]$"))
      {
        throw new ArgumentException("Błąd: Nieprawidłowa długość utworu.");
      }
      Duration = duration;
    }

    public double ComputeDuration()
    {
      string[] parts = Duration.Split(":");
      int minutes = int.Parse(parts[0]);
      double seconds = int.Parse(parts[1]) / 60.0;
      return minutes + seconds;
    }

    public string DisplayInfo()
    {
      return $"'{Title}' - {Artist} ({Duration})";
    }

    public void Play()
    {
      Console.WriteLine($"Odtwarzanie utworu: {DisplayInfo()}");
      int sleepTimeMs = (int)(ComputeDuration() * 1000);
      Thread.Sleep(sleepTimeMs);
    }
  }

  class Program
  {
    static void Main()
    {
      var track1 = new Track("Here Comes the Sun", "The Beatles", "3:05");
      var track2 = new Track("Voices", "Cheap Trick", "4:23");
      var track3 = new Track("Always Forever", "Cults", "3:44");
      var track4 = new Track("Sultans of Swing", "Dire Straits", "5:48");

      Console.WriteLine(track1.Duration);
      Console.WriteLine(track1.ComputeDuration());
      Console.WriteLine(track1.DisplayInfo());
      track1.Play();
    }
  }
}
