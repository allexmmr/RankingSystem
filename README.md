# Ranking System
This is a simple ranking system developed in ASP.NET Core using Entity Framework Code First, design principle of inversion of control (IoC), full mobile compatibility and more.

#### Test
###### 1- Admin can create teams. The team has just a name.
###### 2- Admin can invite team members by adding their name and email.
###### 3- System will send an invitation email. (it doesn't need to really send an email the generated link which is going to be emailed is important)
###### 4- Admin can assign members to the teams. Each member can appear in more than one team.
###### 5- Admin will see invited members in pending and activated state.
###### 6- Member will receive the invite email with a link (as mentioned in step 3). Clicking on the link will change the state of the member to activate.

#### Bonus steps
###### 7- Members can see their teammates and give them a score from 1 to 5 but they cannot score themselves.
###### 8- Users can choose the leaderboard they want to see. There is one leaderboard for each different team.
###### 9- Leaderboard will show the members of a currently selected team in order of their rank.

#### Technical criteria
###### The design and the quality of the code, following the best practices and testability, are more important than the look and aesthetic aspect. The project should be compilable just by pulling the code and required libraries from nuget or npm. Using open source projects (available on nuget) is alright unless it's covering the test itself.
