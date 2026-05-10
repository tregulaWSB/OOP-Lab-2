import time
import re

class Track:
  def __init__(self, title: str, artist: str, duration: str):
    self.title = title
    self.artist = artist
    if not re.match(r"^[0-5]?[0-9]:[0-5][0-9]$", duration):
      raise Exception("Błąd: Nieprawidłowa długość utworu.")
    self.duration = duration

  def compute_duration(self) -> float:
    parts = self.duration.split(":")
    minutes = int(parts[0])
    seconds = int(parts[1]) / 60.0
    return minutes + seconds

  def display_info(self) -> str:
    return f"'{self.title}' - {self.artist} ({self.duration})"
  
  def play(self):
    print(f"Odtwarzanie utworu: {self.display_info()}")
    time.sleep(self.compute_duration())

class Playlist:
  def __init__(self, name: str):
    self.name = name
    self.tracks = []

  def add_track(self, track: Track):
    self.tracks.append(track)

  def remove_track(self, track: Track):
    if track in self.tracks:
      self.tracks.remove(track)
    else:
      print(f"Brak utworu '{track.title}' na playliście.")

  def combine_duration(self) -> str:
    sum_decimal = sum(track.compute_duration() for track in self.tracks)
    hours = int(sum_decimal / 60)
    minutes = int(sum_decimal % 60)
    seconds = int((sum_decimal * 60) % 60)
    if hours > 0:
      return f"{hours} godz. {minutes} min"
    else:
      return f"{minutes} min {seconds} sek."
      
  def display_info(self):
    print(f"Playlista '{self.name}', liczba utworów: {len(self.tracks)}, całkowita długość: {self.combine_duration()}")

  def play(self):
    if not self.tracks:
      print("Nie można odtworzyć playlisty - brak dodanych utworów.")
      return
    print(f"Odtwarzanie playlisty '{self.name}'")
    for track in self.tracks:
      track.play()

  def rename(self, name: str):
    self.name = name

class User:
  def __init__(self, name: str, email: str):
    self.name = name
    self.email = email
    self.playlists = []

  def display_info(self):
    print(f"Użytkownik {self.name}, email: {self.email}, ilość playlist: {len(self.playlists)}")

  def get_all_playlists(self) -> list:
    all_playlists = [playlist.name for playlist in self.playlists]
    return all_playlists
    
  def get_playlist(self, name: str) -> Playlist:
    playlists = [p for p in self.playlists if p.name == name]
    if playlists:
      return playlists[0]
    else:
      raise Exception("Błąd: Brak playlisty o podanej nazwie.")

  def create_playlist(self, name: str) -> Playlist:
    if name in self.get_all_playlists():
      print("Playlista o podanej nazwie już istnieje, wybierz inną nazwę.")
      return
    playlist = Playlist(name)
    self.playlists.append(playlist)
    return playlist

  def remove_playlist(self, playlist: Playlist):
    if not playlist in self.playlists:
      print("Nie znaleziono playlisty użytkownika.")
      return
    self.playlists.remove(playlist)
    print(f"Usunięto playlistę '{playlist.name}'")

def main():
  track1 = Track("Here Comes the Sun", "The Beatles", "3:05")
  track2 = Track("Voices", "Cheap Trick", "4:23")
  track3 = Track("Always Forever", "Cults", "3:44")
  track4 = Track("Sultans of Swing", "Dire Straits", "5:48")

  user1 = User("jan", "jan.kowalski@email.pl")

  playlist1 = user1.create_playlist("Playlista 1")
  playlist1.add_track(track1)
  playlist1.add_track(track3)

  playlist2 = user1.create_playlist("Playlista 2")
  playlist2.add_track(track2)
  playlist2.add_track(track4)

  playlist1.display_info()
  playlist2.display_info()

  user1.display_info()
  user1.remove_playlist(playlist2)
  user1.display_info()
  
  playlist1.play()
  
if __name__ == "__main__":
  main()