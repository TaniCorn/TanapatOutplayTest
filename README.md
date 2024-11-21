# TanapatOutplayTest
 Tanapat Somrid application remote test for Outplay Entertainment.

# Planning
I'll be outlining my intial plans in the readme before getting to work on the development. This is to plan for the time that I have, follow a standard for github, and be an easy reference for myself when thinking of how I want to complete this test.

## Work Log
Just to keep an accurate track of the time I spent on each task and see where I'm at and when, I'll be keeping a work log.
- 21/11/24 (2.5 hours) - Project setup, Github setup, ReadMe planning.

## Github Standard
I'll be completing the tasks one after the other, as such I'll be creating a branch for each task and merging it back into main when complete.
The commit standard will simply be prefixed with a keyword to understand the primary nature of the commit, these will be:
- fix- (A fix to any bugs found)
- wip- (A work in progress commit. Will be done due to having to sign off)
- dev- (A key point for the development of a task)
- task- (The finalisation of a task)

As outlined in the brief, I'll be trying to commit regularly.

## Coding Standard
As I've been following the UE5 coding standard for a while, there may be inconsistancies as I will try to follow a standard Unity, C# coding standard that I've often observed.

- Variables are camelCase
- Functions are PascalCase
- Suffix object type (xxxGO for GameObjects, xxxPrefab for Prefabs etc)
- Variables at the top, with functions following afterwards
- Consistant variable ordering across all scripts (Const, Static, public, protected, private)
- Consistant function ordering across all scripts (Unity functions, public, protected, private)

## Plan
I was sent the test on Wednesday 20th, so I have until 27th. I've started on Thursday 1900 with just the intial project setup and a bit of planning.
I will primarily have time on Friday, Saturday, and Tuesday, and I'll be trying to make time on Sunday. Which means I could probably get in 15 hours in, 20 at a stretch, 10 at a minimum.

Looking at the tasks, 
- Task 1 is the most familiar in terms of mathematics. So I'm hoping it won't take too long, I'll probably have a quick look at some of my old mathematics notebooks to refresh my memory.
  - (5 Hours) I'll hopefully only spend 1 hour on figuring out the solution to the problem, 1 hour on implementation, and I'll give myself 3 hours for testing and extra time.
- I've never tried a task like task 2, however my initial response is thinking of a sorting algorithm of some sort. From my understanding, I'm not being discouraged from using online resources, just to not rely on following a tutorial. In which case, I think it would be fair of me to have a look at some sorting algorithms if that is what I end up settling on for a solution.
  - (7 Hours) I think this could take me a while if my initial guess isn't correct. So I'm going to give myself at least 2 hours for figuring out the solution, 2 hours for implementation, and 3 hours for testing, and extra time.
- Task 3 seems the most simple to me, so I'm worried that I'm being a bit naive with it. I think the main thing is that it's easy to do, but hard to make it look nice and clean and structured. But depending on my time left, I may actually end up falling into that trap. Otherwise, the random spawning, the constant speed of moving between 3 designer specified points, collision, soudn effect and particle effect all seem pretty simple.
  - (5 hours) 1 hour on planning, 2 hours on implementation, 2 hours on testing, bugfixing, and extra time.

Given the above, it should take me 17 hours to complete the 3 tasks. So I may or may not be able to complete this test given how the week plays out.


# Task 1
Task 1 requires me to write an implementation for a reusable function with a given signature which will predict the position of a ball when it reaches the specified height h, for a 2D game about a ball bouncing within a box with no top and with an initial position and velocity, which is affected by gravity. If the ball hits the bottom, it won't bounce. The floor exists at X = 0.
Other considerations include no air resistance, no energy loss on collision, ball can be considered a point, no precision issues and everything can fit easily in a float. 

The function given is 
- bool TryCalculateXPositionAtHeight( float h, Vector2 p, Vector2 v, float G, float w, ref float xPosition)
I'm going to assume the following
- h - specified height
- p - point of ball
- v - initial velocity of ball
- G - gravitational acceleration
- w - width of box
- xPosition - the output position of where the ball will be on the next frame given, that the previous statements are true

Given the information, I think that this is primarily a mathematics problem. One where we're only meant to calculate xPosition when we reach the specified height, and only calculate it once. So it's not trying to predict every frame we are above specified height, just what position on the x axis we are at when we reach specified height. 
I would normally assume we need to take into account the time of the simulation to figure out at what time we reach h, however it doens't appear to be that kind of question. Signing off tonight to come back to it another time.

# Task 2


# Task 3
