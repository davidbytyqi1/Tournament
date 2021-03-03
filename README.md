# Tournament
Problem: League schedule
 
X is hosting a CS GO league with 16 teams participating and has these rules:
1. The league will last exactly 15 weeks
2. Each team will face each other team exactly once
3. Each team will have a match every week
4. Every week will have exactly 8 matches
The first 5 weeks of the league have been set manually by the X team, so this algorithm
will schedule weeks 6 to 15.
Input format:

"1:16,2:15,3:14,4:13,5:12,6:11,7:10,8:9", //w1

"2:9,5:7,12:15,3:6,8:10,4:14,1:11,13:16", //w2

"1:3,7:12,14:15,4:10,8:16,9:13,2:6,5:11", //w3

"7:13,5:8,11:14,3:9,4:15,6:12,2:16,1:10", //w4

"7:16,3:15,13:14,5:10,9:11,8:12,4:6,1:2" //w5


Where 1,2,... 16 are team IDs, and “1:2” is a match between team 1 and 2. e use “,” to separate
between matches in the same week.
What is expected: The solution will print the schedule for upcoming weeks and matches to be
played fulfilling all the league rules.
Bonus points: If the solution will work for any league with n teams and same rules, for any k
weeks pre-scheduled where k > 0 and k < n - 1 will give an output of s lines of scheduled
weeks, where s = n - 1 - k.
