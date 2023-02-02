# cahoot-practice
##### Replace the connection string in application.json, then you can play with this web application (Make sure you attach the mdf to sql server as a database firstly).

#### Rrequirement 1
- Created a ASP MVC application and used stackoverflow data as my db server.
- Used EF core to migrate database and designed an API to query info.
- Implemented a Blazor page with bootstrap.

localhost:5709/r1 is the url you can test this application. But because the data is huge it may needs some time to run.


#### Requirement 2:
Created some functions to use.
````sql

-------------- First to create  getVoteByDay2( ) ------------

create function getVoteByDay2(@d int, @type int)
returns table
as
return (
	SELECT COUNT(*) as count FROM votes
	where ((DATEPART(day, votes.CreationDate) % 7) + 1) = @d
	GROUP BY VoteTypeId
	HAVING VoteTypeId = @type
);

-------------- Create  getPostByDay( ) ------------

create function getPostByDay(@d int, @type int)
returns table
as
return (
	SELECT COUNT(*) as count FROM Posts
	where ((DATEPART(day, Posts.CreationDate) % 7) + 1) = @d
	GROUP BY PostTypeId
	HAVING PostTypeId = @type
);

-------------- Create  getSum4( ) ------------

create function getSum4(@d int, @qa int)
returns table
as
return (
SELECT @d AS day, @qa AS postTypeId ,c.count AS count, a.count AS upvote, b.count AS downvote, Convert(decimal(18,5),a.count/b.count) AS ratio
FROM getVoteByDay2(@d,2) AS a, getVoteByDay2(@d,3) AS b, getPostByDay(@d, @qa) AS c);

-------------- Run the query  ------------

SELECT * FROM (
SELECT * FROM getSum4(1, 1) UNION
SELECT * FROM getSum4(1, 2) UNION
SELECT * FROM getSum4(2, 1) UNION
SELECT * FROM getSum4(2, 2) UNION
SELECT * FROM getSum4(3, 1) UNION
SELECT * FROM getSum4(3, 2) UNION
SELECT * FROM getSum4(4, 1) UNION
SELECT * FROM getSum4(4, 2) UNION
SELECT * FROM getSum4(5, 1) UNION
SELECT * FROM getSum4(5, 2) UNION
SELECT * FROM getSum4(6, 1) UNION
SELECT * FROM getSum4(6, 2) UNION
SELECT * FROM getSum4(7, 1) UNION
SELECT * FROM getSum4(7, 2)
) AS a
ORDER BY a.ratio DESC;
````


#### Requirement 3:
```sql
WITH temp1 AS (SELECT a.weeknumber, a.questionCount, b.AnswerCount FROM(
SELECT COUNT(*) as questionCount, DATEDIFF(week, '2011-02-01', Posts.CreationDate) AS weeknumber FROM Posts
GROUP BY DATEDIFF(week, '2011-02-01', Posts.CreationDate) , PostTypeId
HAVING PostTypeId = 1) AS a
INNER JOIN(
	SELECT COUNT(*) as AnswerCount, DATEDIFF(week, '2011-02-01', Posts.CreationDate) AS weeknumber FROM Posts
	GROUP BY DATEDIFF(week, '2011-02-01', Posts.CreationDate) , PostTypeId
	HAVING PostTypeId = 2) AS b
ON a.weeknumber = b.weeknumber)

SELECT format(dateadd(wk, datediff(wk, 0, DATEADD(wk, d.weeknumber, '2011-02-01')), 0),'yyyy-MM-dd') as startDate, d.questionCount, d.AnswerCount, d.acceptedAnwser, e.userCount, e.voteCount FROM (
SELECT temp1.weeknumber, temp1.questionCount, temp1.AnswerCount, c.acceptedAnwser FROM temp1
INNER JOIN(
	SELECT COUNT(*) as acceptedAnwser, DATEDIFF(week, '2011-02-01', Posts.CreationDate) AS weeknumber FROM Posts
	WHERE AcceptedAnswerId IS NOT NULL
	GROUP BY DATEDIFF(week, '2011-02-01', Posts.CreationDate)) AS c
ON temp1.weeknumber = c.weeknumber
) AS d INNER JOIN (
	SELECT a.weeknumber, a.voteCount, b.userCount FROM(
	SELECT COUNT(*) as voteCount, DATEDIFF(week, '2011-02-01', Votes.CreationDate) AS weeknumber FROM Votes
	GROUP BY DATEDIFF(week, '2011-02-01', Votes.CreationDate)) AS a
	JOIN (
	SELECT COUNT(*) as userCount, DATEDIFF(week, '2011-02-01', Users.CreationDate) AS weeknumber FROM Users
	GROUP BY DATEDIFF(week, '2011-02-01', Users.CreationDate)
	) AS b
	ON a.weeknumber = b.weeknumber
) AS e
ON e.weeknumber = d.weeknumber
ORDER BY startDate DESC;
```
