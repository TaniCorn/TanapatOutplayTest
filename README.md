# TanapatOutplayTest
 Tanapat Somrid application remote test for Outplay Entertainment.

# Planning
I'll be outlining my intial plans in the readme before getting to work on the development. This is to plan for the time that I have, follow a standard for github, and be an easy reference for myself when thinking of how I want to complete this test.

## Work Log
Just to keep an accurate track of the time I spent on each task and see where I'm at and when, I'll be keeping a work log.
- 21/11/24 (1.5 hours) - Project setup, Github setup, ReadMe planning.
- 22/11/24 (2.5 hours) - Task 1, completed basic functionality. Theoretically works, however after putting in some simple test numbers, it does not appear to work.
- 22/11/24 (2 hours) Task 1, fixed issues and happy with the solution
- 23/11/24 (1 hour) Task 2, figuring out solution and planning
- 23/11/24 (1.5 hours) Task 2, implementing the board searching and putting placeholder functionality for the tree search
- 23/11/24 (1.5 hours) Task 2, thinking of how to best implement the tree search, and then implementing said tree

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

## Execution
This is a retrospect after all the tasks

- Task 1 (4.5 hours), I'm pretty happy with my final solution and my method of getting there. After a good initial plan of using max height to determine the initial condition. I was a bit too excited to use max x distance that it blindsided me for 20 minutes, until I realised it was the wrong solution. It took a bit of time to think about what I could use as my mind was avoiding *time*step, but realised that time is still a necessary factor and it popped into my head about finding the intersection point, I could use a linear movement equation. Using SUVAT I then was pretty happy with the theoretical solution, but as I expected there were bugs, but it didn't take too long to find out what was wrong, and it was just a matter of developing the solution to properly consider negative velocities.

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

## Initial ideas
Given the information, I think that this is primarily a mathematics problem. One where we're only meant to calculate xPosition when we reach the specified height, and only calculate it once. So it's not trying to predict every frame we are above specified height, just what position on the x axis we are at when we reach specified height. 
I would normally assume we need to take into account the time of the simulation to figure out at what time we reach h, however it doesn't appear to be that kind of question. Signing off tonight to come back to it another time.

![20241122_090005](https://github.com/user-attachments/assets/7f019866-dd04-4755-a916-de5d7d99eb6d)

I was initially implementing the mathematical equation for the dot product but then remembered Unity had a function for finding the angle between two vectors and just used that.

## G Issue
There was 1 issue however, which is that I had assumed G would always be down. But looking at it, there's no assumptions made on G. In this particular game, my assumption would be that G should always be negative.
As I was just having a look at the problem if I continued down this line of thought, there was just too many things to consider, and a few too many assumptions that I would have to make. 
I'm going to assume that for the purposes of this test, that G SHOULD be negative, but won't necessarily be passed in as negative.

## Horizontal Displacement
After going down the line of the Horizontal range, I realised I was missing a crucial step. I don't know when the y will be intersected. So having a look again at the vertical displacement, I was thinking I need a time of some sort, which led me to the old SUVAT formulas. Re-arranging it, gives me t, I just use the same formula to find the distance travelled when at time t, which means I never needed the maximum horizontal distance.
Using the bounces as sort of reflective space, we can determine which way we are bouncing to using some simple maths, which helps us determine the final xDisplacement by making it relative to one of the walls, the 0 or the width.

![20241122_090013](https://github.com/user-attachments/assets/76ae5b31-d4c9-4fe8-9186-9d27e450f1bf)

## Bugfixing

After testing with 5 different simple scenarios for
- Can't reach height
- Move left no bounce
- Move right no bounce
- Move left bounce
- Move right bounce

I found that the numbers looked completely wrong. Going through with a breakpoint I found a few things
1. My re-arranged SUVAT formula sign was wrong
2. I didn't take into account negative x direction enough, which caused a negative bounce number
3. The reflection sign didn't take into account which wall it bounced on first

Solving the first one was easy enough to find, as time was negative.
However the 2nd and 3rd one tied in together. As I was trying to figure out how I can work with the numbers, my diagram below shows my thought process of using the overall travelled distance and subtracting it from each border.

![20241122_105436](https://github.com/user-attachments/assets/945c5de3-fb5c-4127-bf85-116ba8a3409c)

I made a few if statements to figure our which wall we bounced on, but along the way I realised I could use the negative bounce number to infer that information instead, I could then subsequent get rid of the if statements and different calculations to figure out which wall to add or subtract on.

Overall pretty happy with the solution, even happier that I managed to get it done within my time estimate. I'm not the happiest about how many comments there are, however I think that without them, coming back to this someone could get lost on what each step does. For very mathematical functions, I think this amount of commenting is appropriate.

# Task 2
Task 2 requires me to write an implementation for a function which returns the current best move for a match 3 board. The main issue I'm seeing here is how I'm interpreting this question
1. The implementation requires the most immediate best move, and has no regard for future moves.
2. The implementation requires the best move for the best gains, requiring it to look x events into the future.

In the rules, it does not say empty gems get replaced, but it is common for match 3 games for gems to fall to empty slots, so I can assume 1 of 3 things

1. a. It works like a standard match 3 game where gems fall and new random gems get placed above
2. b. Gems do not get replaced and are empty, and therefore there is a limited amount of moves
3. c. The assumption is that it's a clean board each time this function runs, and we do not care about about standard games


The reason why this is important is that it tells me what kind of algorithm to make.

If assumption 'c' is correct, then '1' is the correct path for implementation. 

If assumption 'b' is correct, then there is a best chain of moves for the entire game and '2' would be ideal

If assumption 'a' is correct, then there is a a best chain of moves, that may get overwritten with new gem information, so '2' is ideal, but '1' is fine

I would like to send an email just to clarify, but it's the weekend. Based on this rule '... best move for a given board is ths the one ethat will remove the most jewels.', regarding the given board. I believe that it means to treat the board as a new one each time this function is ran. So I will be going with teh assumption of c, and just implement a simple algorithm that has no regard for future moves.

The rules of Match 3:
- Pairs of jewels adjacent vertically and horizontally can be swapped.
- You can only swap jewels when this will result in a match being created.
- A match happens when there are 3 or more jewels of the same kind adjacent vertically or horizontally.
- All jewels involved in matches are set to JewelKind::Empty after each move.
- One point is given for each jewel that has been removed. The best move for a given board is thus the one that will remove the most jewels.
- The initial board state contains no matches; therefore, swapping jewels is the only way matches can be created.

## Initial Ideas
I'm immediately thinking of a tree search algorithm for this, but for now I'll just see if I can break down the problem.

I don't think I can really avoid having to check every single gem as a first loop, technically you can avoid it by not checking gems you will swap with for other gems, but I think that might just increase code complexity for little performance gain.
Then for every gem, we have to check each move it can make and if it's possible. Here we could immediately try to calculate the points for that move, or save that gem move as a valid one to come back to later.
The gem search has to take into account all connected gems to try and add them up to make the overall points. The hard part here is to make sure you don't add a previous gem. Like a loop.
Then compare which gem move has the most points.

I'll go with immediate calculations as it feels pointless to check is 3 connected, abandon it, then continue the search again later.

```
1. Go through all gems on the board
2. Go through all moves that a gem can take, for each gem on the board
3. Calculate the points gained from that move
4. Find the best move on the board for the gem

for (gem : gems)
 for (moves : gem)
  for (movedirection : moves)
   fake Move in direction
   check connected gem count in all directions except for 'movedirection'
   if (connectedgemcount > currentbestcount)
    currentbestcount = connectedgemcount
    save Move
   end if
  end loop
 end loop
end loop
```
## Board Info
One thing I didn't immediately consider was how to access the board info. I could access it each time with GetJewel, however I don't like how that may be very innefficient with many accesses.
So I thought a simple multi-dimensional array that gets filled initially would be great as I think it would make for better cache hits down the line. 

## Axis assumptions
Unfortunately it's not mentioned where the x and y axis is aligned. I'll be going with the assumption that x = 0 is left, and y = 0 is the top.

## Tree searching algorithm
Currently I'm contemplating between doing a breadth vs depth first search algorithm. However I don't think it makes too much of a difference, and personally I would prefer to do a breadth first search as that makes the most sense to me in this scenario for coding.

Next comes the question of what data structure I want to implement it with. I'm initially thinking a set to ensure no duplicates, this is an easy way to ensure we don't double search things.

```
Add initial node to queue
foreach queue node
 Search all other directions except for direction that we came through
 foreach neighbour
  if node neighbours matches desired gem
   try to add node neighbour to set
   if (adding node neighbour to set is successful)
    add node neighbour to queue //for searching
    add one point
   end if
  end if
 end neighbour loop
pop queue
end queue loop
```

# Task 3
