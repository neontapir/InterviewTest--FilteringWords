# Filtering Words

Write some C# code that filters a sequence of strings.  It should pass all six letter strings that could be formed by joining two smaller strings from the same sequence. 

## For example

Given the sequence: 

```
al, albums, aver, bar, barely, be, befoul, bums, by, cat, con, convex, ely, foul, here, hereby, jig, jigsaw, or, saw, tail, tailor, vex, we, weaver
```

The code should pass the following:

```
albums, barely, befoul, convex, hereby, jigsaw, tailor, weaver
```

Because these could be formed by joining two other strings from the sequence:

```
al + bums => albums
bar + ely => barely
be + foul => befoul
con + vex => convex
here + by => hereby
jig + saw => jigsaw
tail + or => tailor
we + aver => weaver
```

Use of TDD techniques is required focussing on the requirements.   Please comment your code with your name and thoughts about what is good about your solution, and what could be improved.  

Please submit your solution in a ZIP file by email to the address provided.  Do not include compiled objects or third party DLLs.
