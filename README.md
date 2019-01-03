# Mutant-Pong
A version of Pong made in Unity that evolves better paddles using a genetic algorithm. 


## Usage
1. Download
2. Modify DrawShape.py so that it saves PNGs to C:/Users/<your username>/path/to/pong/Assets/Resources/
3. Compile DrawShapes/DrawShape.py with py2exe
  ```
  /DrawShapes> python setup.py py2exe
  ```
3. Copy contents of dist folder into C:/DrawShapes/
4. Open project in Unity
5. Enjoy the show!

## Dependencies

Unity 2017.3.1f1
- not tested with other versions but will probably still work

Python 2.7
- Pillow
- py2exe
