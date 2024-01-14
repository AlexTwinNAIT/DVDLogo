# DVD Logo Coding Adventure - Alex Twin

### !!! Slight epilespy warning: Sometimes when the logo hits the corner it bounces around 20 times per second, causing the logo to change color every time it bounces. !!!

## What is this project?

Basically I heard that our first lesson in DMIT1514 reminded us of the DVD Logo screensaver thing. So I decided I'll just remake it in MonoGame

## Why did I do this?

bored.

## Okay how do I download and run this?

Just check out [Releases](https://github.com/AlexTwinNAIT/DVDLogo/releases/tag/WidePosting) and download as a .zip file, extract and run DVDLogo.exe

## Discoveries/ Explanations

### 1. Basic collisions

So basically I figured out that I dont need to make a fancy collisiion detection system, why do that! we aren't in the stone age! we have math!

> *When working in 2D, MonoGame uses a coordinate system similar to the screen coordinates you've seen in your earlier classes. The origin of the coordinate system, is the upper-left corner of the game window's client area, and the X-axis increases to the right and the Y-axis increases downward.* - [CIS580, Intro to Monogame](https://textbooks.cs.ksu.edu/cis580/01-intro-to-monogame/03-the-game-window/index.html#:~:text=When%20working%20in%202D%2C%20MonoGame,the%20Y-axis%20increases%20downward.)

---
*the logo's position also starts at the upper left corner*, so thats where 0,0 is.

With that being said, we can check the position of the DVD logo. relative to the screen's width and height! if it exceeds the bounds then we know we've hit the edge!

```cs
if (_logoPosition.X > ScreenSize.X || _logoPosition.X < 0) // do the same with the y axis
{
    // do the thing
}
```

**Okay smart guy, what about the image itself, it's gonna exceed the bounds because the position is on the upper left corner of the logo!**

Oh but I thought of that! By subtracting the width and height of the logo from the screen width and height, you'll get the ACTUAL hitbox, and by extenstion the functionality will work for the DVD logo, and not just the position. It's the same logic, just adjusted for the DVD Logo.

```cs

if (_logoPosition.X > ScreenSize.X - _dvdLogo.Width || _logoPosition.X < 0 || _logoPosition.Y > ScreenSize.Y - _dvdLogo.Height || _logoPosition.Y < 0)
{
    // do the thing
}

```
---
Here is a visual representation, just imagine the blue box is the DVD logo, and the red dot is both 0,0 and the blue box's position in MonoGame. The black border is the actual hitbox derived from the if statement above this text. The red border is the actual screen size. *(I'm sorry my measurements didn't line up.)*

![Explaining how it works with visual aid](https://github.com/AlexTwinNAIT/DVDLogo/blob/main/content/CallmeDonTheWayIBeMSPainting.png)

---

### 2. Reflecting the tragectory of the DVD Logo

Okay so after reading this [StackOverflow](https://math.stackexchange.com/questions/13261/how-to-get-a-reflection-vector) post it gave me the formula for reflecting a vector.

- d Is the Logo's position 1 frame prior (just before impact)
- r is the reflected vector
- n is the normal is the wall we'd be hitting.
- theta is the degrees between the normal's direction, and the logo's pre-hit direction.

![Vector Math representation](https://i.stack.imgur.com/IQa15.png)

>   r=d-2(d*n)n

So fun fact, MonoGame has a reflect function built in called: `Vector2.Reflect()` So I dont need to hardcode it.

problem is that I don't actually have the normal values however after consolting a friend, I decided to  just hard code the normal values, being as we're inside a square and not an abstract shape it's not that difficult to hard code them as there's only four sides.

### So here is the normal vectors:

**as a visual aid:**

![Visual Aid 2](https://i.imgur.com/qTz55EJ.png)

*Remember that in MonoGame the X value increases the more right-wards you are in the screen. The Y Values increases the more downwards you go.*

**in code:**

```cs
public struct Normals
{
    public static readonly Vector2 top = new Vector2(0, -1); // faces down
    public static readonly Vector2 bottom = new Vector2(0, 1); // faces up
    public static readonly Vector2 left = new Vector2(1, 0); // faces right
    public static readonly Vector2 right = new Vector2(-1, 0); // faces left
}
```
**What do you mean? I don't understand what a normal vector is!**

A normal vector is a vector that represents an object's surface orientation!

Consult the visual aid that I drew in around 5 seconds. VVVV

![Visual Aid 3](https://i.imgur.com/3V0BU7L.png)

*(I'm sorry I'm not able to describe what a normal vector is, good news is for us game programmers is that we actually have a class where we learn math and physics... for games...)*


So now that we have the normal values we can properly reflect our logo's trajectory,  and now we're finished.

---



## Resources/References/Credits

Here is where i'll list all my resources for this little project, maybe also some coding snippets following my reasons of understanding this.
- Commit names are references to Heaven Pierce Her tracks.
- How to add content and by extention image sprites to the project [here](https://docs.MonoGame.net/articles/getting_started/4_adding_content.html)
- [DVD Logo](https://freebiesupply.com/logos/dvd-logo/)
- [CIS580, Intro to Monogame](https://textbooks.cs.ksu.edu/cis580/01-intro-to-monogame/03-the-game-window/index.html#:~:text=When%20working%20in%202D%2C%20MonoGame,the%20Y-axis%20increases%20downward.)
- Credits to Matthew (???) for telling me I'm bad at math.
- [DVD]