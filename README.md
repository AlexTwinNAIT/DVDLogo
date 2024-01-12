# DVD Logo Coding Adventure - Alex Twin

## What is this project?

Basically I heard that our first lesson in DMIT1514 reminded us of the DVD Logo screensaver thing. So I decided I'll just remake it in MonoGame

## Why did I do this?

bored.

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

![Explaining how it works with visual aid](../DVDLogo/content/CallmeDonTheWayIBeMSPainting.png)


## Resources/References

Here is where i'll list all my resources for this little project, maybe also some coding snippets following my reasons of understanding this.
- Commit names are references to Heaven Pierce Her tracks.
- How to add content and by extention image sprites to the project [here](https://docs.MonoGame.net/articles/getting_started/4_adding_content.html)
- [DVD Logo](https://freebiesupply.com/logos/dvd-logo/)