import time

class Track:
  def __init__(self, title: str, artist: str, duration: str):
    self.title = title
    self.artist = artist
    self.duration = duration

  def compute_duration(self) -> float:
    parts = self.duration.split(":")
    minutes = int(parts[0])
    seconds = int(parts[1]) / 60.0
    return minutes + seconds

  def display_info(self) -> str:
    return f"'{self.title}' - {self.artist} ({self.duration})"
  
  def play(self):
    print(f"Playing song: {self.display_info()}")
    time.sleep(self.compute_duration())

def main():
  track1 = Track("Here Comes the Sun", "The Beatles", "3:05")
  print(f"Track 1 duration: {track1.duration}")
  print(f"Track 1 computed duration: {track1.compute_duration()}")
  print(f"Track 1 info: {track1.display_info()}")
  track1.play()

if __name__ == "__main__":
  main()