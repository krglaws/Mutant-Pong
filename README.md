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

## Sample Paddles
These are some example paddles as they are drawn by the Pillow image library in Python:

![alt text](https://github.com/krglaws/Mutant-Pong/blob/master/demo_paddles/image3.png)

![alt text](https://github.com/krglaws/Mutant-Pong/blob/master/demo_paddles/image4.png)

![alt text](https://github.com/krglaws/Mutant-Pong/blob/master/demo_paddles/image5.png)

Each paddle has 8 vertices which are picked at random in the first generation. Each paddle plays one game against the standard rectangular pong paddle. The best performing (and thus the most fit) paddles have a higher likelihood of being selected for breeding and passing on their "DNA" to the subsequent generation. This cycle repeats until you get bored!

## Unity Generated Hit Detection
Once loaded into Unity, the hit detection lines are generated (in green):

![alt text](https://github.com/krglaws/Mutant-Pong/blob/master/demo_paddles/image1.png)


## Dependencies

Unity 2017.3.1f1
- not tested with other versions but will probably still work

Python 2.7
- Pillow
- py2exe
