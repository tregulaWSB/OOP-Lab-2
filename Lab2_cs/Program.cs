using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

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

  public class Playlist
  {
    public string Name { get; private set; }
    private readonly List<Track> tracks = new List<Track>();

    public Playlist(string name)
    {
      Name = name;
    }

    public void AddTrack(Track track)
    {
      tracks.Add(track);
    }

    public void RemoveTrack(Track track)
    {
      if (tracks.Contains(track))
      {
        tracks.Remove(track);
      }
      else
      {
        Console.WriteLine($"Brak utworu '{track.Title}' na playliście.");
      }
    }

    public string CombineDuration()
    {
      double sumDecimal = tracks.Sum(track => track.ComputeDuration());
      int hours = (int)(sumDecimal / 60);
      int minutes = (int)(sumDecimal % 60);
      int seconds = (int)(sumDecimal * 60 % 60);
      if (hours > 0)
      {
        return $"{hours} godz. {minutes} min";
      }
      else
      {
        return $"{minutes} min {seconds} sek.";
      }
    }

    public void DisplayInfo()
    {
      Console.WriteLine($"Playlista '{Name}', liczba utworów: {tracks.Count}, całkowita długość: {CombineDuration()}");
    }

    public void Play()
    {
      if (tracks.Count == 0)
      {
        Console.WriteLine("Nie można odtworzyć playlisty - brak dodanych utworów.");
        return;
      }
      Console.WriteLine($"Odtwarzanie playlisty '{Name}'");
      foreach (var track in tracks)
      {
        track.Play();
      }
    }

    public void Rename(string name)
    {
      Name = name;
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

      var playlist1 = new Playlist("Playlista 1");
      playlist1.DisplayInfo();
      
      playlist1.AddTrack(track1);
      playlist1.AddTrack(track2);
      playlist1.AddTrack(track3);
      playlist1.AddTrack(track4);
      
      playlist1.DisplayInfo();
      
      playlist1.Play();
    }
  }
}
